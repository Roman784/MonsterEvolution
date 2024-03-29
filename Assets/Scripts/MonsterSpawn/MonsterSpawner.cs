using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner Instance {  get; private set; }

    [SerializeField] private List<Monster> _monsterPrefabs = new List<Monster>();

    private int maxTypeNumber;

    private void Awake()
    {
        Instance = Singleton.Get<MonsterSpawner>();

        maxTypeNumber = GetMaxTypeNumber();
    }

    public void Spawn(int typeNumber, Vector2 position, bool needSave=true)
    {
        Monster prefab = GetMonsterPrefab(typeNumber);

        if (prefab == null) return;

        Monster spawnedMonster = Instantiate(prefab);
        spawnedMonster.Init(maxTypeNumber, position);

        MonsterRegistry.Instance.Add(spawnedMonster);

        if (typeNumber > DataContext.Instance.GameData.MaxMonsterLevel)
        {
            DataContext.Instance.SetMaxMonsterLevel(typeNumber);
        }

        if (needSave)
        {
            DataContext.Instance.AddMonster(typeNumber);
        }
    }

    public void SpawnSavedMonsters()
    {
        List<MonsterData> monstersData = DataContext.Instance.GameData.Monsters;

        foreach(MonsterData monsterData in monstersData)
        {
            Vector2 position = CorralArea.Instance.GetRandomPosition();
            Spawn(monsterData.TypeNumber, position, false);
        }
    }

    private Monster GetMonsterPrefab(int typeNumber)
    {
        return _monsterPrefabs.FirstOrDefault(m => m.TypeNumber == typeNumber);
    }

    private int GetMaxTypeNumber()
    {
        return _monsterPrefabs.OrderByDescending(m => m.TypeNumber).First().TypeNumber;
    }

    [ContextMenu("Spawn test monster")]
    private void SpawnTestMonster()
    {
        Vector2 position = CorralArea.Instance.GetRandomPosition();
        Spawn(1, position);
    }
}
