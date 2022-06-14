using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

//For this class and inheriting classes I've followed a tutorial from "Epitome" on YouTube
//Link: https://www.youtube.com/watch?v=lPoiTw0qjtc&list=PLmcbjnHce7SeAUFouc3X9zqXxiPbCz8Zp&index=11


public class NetMessage
{
    public OpCode Code { set; get; }

    public virtual void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
    }
    public virtual void Deserialize(DataStreamReader reader)
    {

    }
    public virtual void ReceivedOnClient()
    {

    }
    public virtual void ReceivedOnServer(NetworkConnection cnn)
    {

    }
}
