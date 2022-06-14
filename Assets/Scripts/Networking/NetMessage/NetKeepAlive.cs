using Unity.Networking.Transport;

//For this class I've followed a tutorial from "Epitome" on YouTube
//Link: https://www.youtube.com/watch?v=lPoiTw0qjtc&list=PLmcbjnHce7SeAUFouc3X9zqXxiPbCz8Zp&index=11

public class NetKeepAlive : NetMessage
{
    public NetKeepAlive() // <-- Making the box
    {


        Code = OpCode.KEEP_ALIVE;
    }
    public NetKeepAlive(DataStreamReader reader) // <-- receiving the box
    {
        Code = OpCode.KEEP_ALIVE;
        Deserialize(reader);
    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
    }

    public override void Deserialize(DataStreamReader reader)
    {
        //No reader.readbyte here because when we get to deserializing we already opened the box
        //thus we don't have to deserialize
    }

    public override void ReceivedOnClient()
    {
        NetUtility.C_KEEP_ALIVE?.Invoke(this);
    }

    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.S_KEEP_ALIVE?.Invoke(this, cnn);
    }
}
