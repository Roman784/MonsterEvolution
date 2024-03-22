using System.IO;
using UnityEngine;

public class JsonSerializer : IDataSerializer
{
    private string _savePath;
    private string _saveFileName;

    private string _fullPath;

    public JsonSerializer(string savePath, string saveFileName) 
    {
        _savePath = savePath;
        _saveFileName = saveFileName;

        _fullPath = Path.Combine(_savePath, _saveFileName + ".json");
    }

    public GameData Load()
    {
        GameData data = null;

        if (!File.Exists(_fullPath))
        {
            Debug.Log("File not exist");

            return data;
        }
        
        try
        {
            string json = File.ReadAllText(_fullPath);
            data = JsonUtility.FromJson<GameData>(json);

            Debug.Log("Load data complete");
        }
        catch { Debug.Log("Load data error"); }

        return data;
    }

    public void Save(GameData data)
    {
        try
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(_fullPath, json);

            Debug.Log("Save data complete");
        }
        catch { Debug.Log("Save data error"); }
    }
}
