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

    public GameData GameData { get; private set; } = null;
    private GameData _defaultGameData = new GameData()
    {
        MaxMonsterLevel = 0,
        Wallet = new WalletData()
        {
            CoinCount = "0",
            CPSLevel = 0,
            InitialCPSMultiplier = 1f
        },
        Monsters = new List<MonsterData>(),
        MonsterSpawner = new MonsterSpawnerData()
        {
            BoxSpawnerLevel = 0,
            MonsterBoxLevel = 0,
            InitialTypeNumber = 1,
            InitialCooldown = 15f,
            InitialTimeReductionStep = 0.1f
        },
        MergeMagnet = new MergeMagnetData()
        {
            Level = 0,
            InitialCooldown = 20f,
            InitialCoupleCountAtTime = 1
        },
        BoxOpener = new BoxOpenerData()
        {
            Level = 0,
            InitialCooldown = 20f,
            InitialCountAtTime = 1
        },
        SoundVolume = 1f
    };

    private IDataSerializer _serializer;

    private DataContext()
    {
        // _serializer = new JsonSerializer(Application.dataPath, "gameData");
        _serializer = new YandexSerializer();
    }

    public void Load()
    {
        GameData = _serializer.Load();
    }

    private void Save()
    {
        _serializer.Save(GameData);
    }

    public void SetGameData(GameData data)
    {
        GameData = data;

        if (GameData == null)
            DefaultData();
    }

    private void DefaultData()
    {
        GameData = _defaultGameData;

        Debug.Log("Default data");

        Save();
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
        GameData.Wallet.CoinCount = value.ToString();
        
        Save();
    }

    public void SetCPSLevel(int level)
    {
        GameData.Wallet.CPSLevel = level;

        Save();
    }

    public void SetBoxSpawnerLevel(int level)
    {
        GameData.MonsterSpawner.BoxSpawnerLevel = level;

        Save();
    }

    public void SetMonsterBoxLevel(int level)
    {
        GameData.MonsterSpawner.MonsterBoxLevel = level;

        Save();
    }

    public void SetMergeMagnedLevel(int level)
    {
        GameData.MergeMagnet.Level = level;

        Save();
    }

    public void SetBoxOpenerLevel(int level)
    {
        GameData.BoxOpener.Level = level;

        Save();
    }

    public void SetMaxMonsterLevel(int level)
    {
        GameData.MaxMonsterLevel = level;

        Save();
    }

    public void SetSoundVolume(float volume)
    {
        GameData.SoundVolume = volume;

        Save();
    }
}
