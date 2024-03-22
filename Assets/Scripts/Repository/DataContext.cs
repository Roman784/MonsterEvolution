using UnityEngine;

public class DataContext
{
    private static DataContext _instance;
    public static DataContext Instance
    {
        get
        {
            if (_instance == null)
                _instance = new DataContext();
            return _instance;
        }
    }

    public GameData GameData { get; private set; }

    private IDataSerializer _serializer;

    private DataContext()
    {
        _serializer = new JsonSerializer(Application.persistentDataPath, "gameData");

        Load();
    }

    private void Load()
    {
        GameData = _serializer.Load();
    }

    private void Save()
    {
        _serializer.Save(GameData);
    }
}
