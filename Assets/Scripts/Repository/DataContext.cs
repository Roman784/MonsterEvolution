using System.Collections.Generic;
using System.Linq;
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
        _serializer = new JsonSerializer(Application.dataPath, "gameData");

        Load();

        if (GameData == null)
            DefaultData();
    }

    private void Load()
    {
        GameData = _serializer.Load();
    }

    private void Save()
    {
        _serializer.Save(GameData);
    }

    private void DefaultData()
    {
        GameData = new GameData();
        GameData.Monsters = new List<MonsterData>();
    }

    public void AddMonster(int typeNumber)
    {
        MonsterData monster = new MonsterData()
        {
            TypeNumber = typeNumber
        };

        GameData.Monsters.Add(monster);

        Save();
    }

    public void RemoveÑoupleMonster(int typeNumber) 
    {
        MonsterData monster1 = GameData.Monsters.FirstOrDefault(m => m.TypeNumber == typeNumber);
        MonsterData monster2 = GameData.Monsters.FirstOrDefault(m => m.TypeNumber == typeNumber && m != monster1);

        if (monster1 == null || monster2 == null)
            return;

        GameData.Monsters.Remove(monster1);
        GameData.Monsters.Remove(monster2);

        Save();
    }
}
