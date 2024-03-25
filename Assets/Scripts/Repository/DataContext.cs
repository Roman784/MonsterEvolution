using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
    private GameData _defaultGameData = new GameData()
    {
        CoinCount = "0",
        Monsters = new List<MonsterData>(),
        MonsterSpawner = new MonsterSpawnerData()
        {
            TypeNumber = 1,
            Cooldown = 15f,
            TimeReductionStep = 0.1f
        },
        MergeMagnet = new MergeMagnetData()
        {
            IsOpen = false,
            Cooldown = 20f
        },
        BoxOpener = new BoxOpenerData()
        {
            IsOpen = false,
            Cooldown = 15f
        }
    };

    private IDataSerializer _serializer;

    private DataContext()
    {
        _serializer = new JsonSerializer(Application.dataPath, "gameData");
    }

    public void Load()
    {
        GameData = _serializer.Load();

        if (GameData == null)
            DefaultData();
    }

    private void Save()
    {
        _serializer.Save(GameData);
    }

    private void DefaultData()
    {
        GameData = new GameData()
        {
            CoinCount = _defaultGameData.CoinCount,
            Monsters = new List<MonsterData>(_defaultGameData.Monsters)
        };
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

    public void SetCoinCount(BigInteger value)
    {
        GameData.CoinCount = value.ToString();
        
        Save();
    }

    public void SetMonsterSpawnerData(MonsterSpawnerData newData)
    {
        GameData.MonsterSpawner = newData;

        Save();
    }

    public void SetMergeMagnedData(MergeMagnetData newData)
    {
        GameData.MergeMagnet = newData;

        Save();
    }

    public void SetBoxOpenerData(BoxOpenerData newData)
    {
        GameData.BoxOpener = newData;

        Save();
    }
}
