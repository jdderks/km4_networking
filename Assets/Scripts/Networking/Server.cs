using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Networking.Transport;
using UnityEngine;

//For this class I've followed a tutorial from "Epitome" on YouTube
//Link: https://www.youtube.com/watch?v=lPoiTw0qjtc&list=PLmcbjnHce7SeAUFouc3X9zqXxiPbCz8Zp&index=11

public class Server : MonoBehaviour
{
    #region Singleton implementation
    public static Server Instance { set; get; }
    private void Awake()
    {
        Instance = this;
    }
    #endregion


    public NetworkDriver driver;
    private NativeList<NetworkConnection> connections;

    private bool isActive = false;

    private const float keepAliveTickRate = 20.0f; //If the connection drops it will dc after 20 seconds
    private float lastKeepAliveTimestamp;

    public Action connectionDroppedAction;

    public void Initialize(ushort port)
    {
        driver = NetworkDriver.Create();
        NetworkEndPoint endpoint = NetworkEndPoint.AnyIpv4;

        endpoint.Port = port;

        if (driver.Bind(endpoint) != 0)
        {
            Debug.Log("Unable to bind on port " + port);
            return;
        } 
        else
        {
            driver.Listen();
            Debug.Log("Currently listening to port:" + port);
        }

        connections = new NativeList<NetworkConnection>(2, Allocator.Persistent);
        isActive = true;
    }
    public void Shutdown()
    {
        if (isActive)
        {
            driver.Dispose();
            connections.Dispose();
            isActive = false;
        }
    }
    private void OnDestroy()
    {
        Shutdown();
    }

    public void Update()
    {
        if (!isActive)
        {
            return;
        }


        KeepAlive();
        driver.ScheduleUpdate().Complete();

        CleanupConnections();
        AcceptNewConnections();
        UpdateMessagePump();
    }

    //Every 20 seconds we broadcast a keep alive message to every client
    private void KeepAlive()
    {
        if (Time.time - lastKeepAliveTimestamp > keepAliveTickRate)
        {
            lastKeepAliveTimestamp = Time.time;
            Broadcast(new NetKeepAlive());
        }
    }

    private void CleanupConnections()
    {
        for (int i = 0; i < connections.Length; i++)
        {
            if (!connections[i].IsCreated)
            {
                connections.RemoveAtSwapBack(i);
                i--;
            }
        }
    }
    private void AcceptNewConnections()
    {
        NetworkConnection c;
        while ((c = driver.Accept()) != default(NetworkConnection))
        {
            connections.Add(c);
        }
    }
    private void UpdateMessagePump()
    {
        DataStreamReader stream;
        for (int i = 0; i < connections.Length; i++)
        {
            NetworkEvent.Type cmd;
            while ((cmd = driver.PopEventForConnection(connections[i], out stream)) !=  NetworkEvent.Type.Empty)
            {
                if (cmd == NetworkEvent.Type.Data)
                {
                    NetUtility.OnData(stream, connections[i], this);
                } 
                else if (cmd == NetworkEvent.Type.Disconnect)
                {
                    Debug.Log("Client disconnected from server");
                    connections[i] = default(NetworkConnection);
                    connectionDroppedAction?.Invoke();
                    Shutdown(); //In 2 player games if the other disconnects you could shut down, this doesn't usually happen in games with more people
                }
            }

        }
    }
    
    //Server specific code
    public void SendToClient(NetworkConnection connection, NetMessage msg)
    {
        DataStreamWriter writer;                    //Create a new box
        driver.BeginSend(connection, out writer);   //Put adress on box
        msg.Serialize(ref writer);                  //Package up the box
        driver.EndSend(writer);
    }

    /// <summary>
    /// Broadcasts a message to all connected clients
    /// </summary>
    /// <param name="msg"></param>
    public void Broadcast(NetMessage msg)
    {
        for (int i = 0; i < connections.Length; i++)
        {
            if (connections[i].IsCreated)
            {
                //Debug.Log($"Sending {msg.Code} to : {connections[i].InternalId}");
                SendToClient(connections[i],msg);
            }
        }
    }
}