using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class ByteSerializer
{
    public static byte[] Serialize(object obj)
    {
        byte[] data;

        using (var stream = new MemoryStream()) 
        {
            (new BinaryFormatter()).Serialize(stream, obj);
            stream.Flush();
 
            data = stream.ToArray();
        }

        return data;
    }

    public static TType Deserialize<TType>(byte[] data)
    {
        TType obj;

        using (var stream = new MemoryStream(data))
        {
            obj = (TType)(new BinaryFormatter()).Deserialize(stream);
        }

        return obj;
    }
}