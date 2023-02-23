using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
public class Storage
{
    private string _filePath;
    private BinaryFormatter _formatter;

    public Storage()
    {
        var directory = Application.persistentDataPath + "/saves";
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        _filePath = directory + "/GameSave.save";
        _formatter = new BinaryFormatter();
    }

    public object Load(object saveDefaultData)
    {
        if (!File.Exists(_filePath))
        {
            if (saveDefaultData != null)
            {
                Save(saveDefaultData);
            }
            return saveDefaultData;
        }

        var file = File.Open(_filePath, FileMode.Open);
        var savedData = _formatter.Deserialize(file);
        file.Close();
        return savedData;
    }

    public void Save(object saveData)
    {
        var file = File.Create(_filePath);
        _formatter.Serialize(file, saveData);
        file.Close();
    }
}
