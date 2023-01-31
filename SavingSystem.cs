using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SavingSystem <Type> : MonoBehaviour where Type : new()
{
    public static void Save(string p_path, Type p_file)
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = File.Create(p_path);
            bf.Serialize(stream, p_file);
            stream.Close();
        }
        catch(FileNotFoundException ex)
        {
            Debug.LogError(ex + "File Not Found!!!");
        }
    }
    
    public static Type Load(string p_path)
    {
        Type p_file = new Type();
        try
        {
            if (File.Exists(p_path))
            {
                FileStream stream = File.Open(p_path, FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                p_file = (Type)bf.Deserialize(stream);
                stream.Close();
            }
        }
        catch (FileNotFoundException ex)
        {
            Debug.LogError(ex + "File Not Found!!!");
        }

        return (Type)p_file;
    }
}