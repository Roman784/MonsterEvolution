using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner Instance {  get; private set; }

    [SerializeField] private List<Monster> _monsterPrefabs = new List<Monster>();
    [SerializeField] private MonsterBox _monsterBoxPrefab;

    private void Awake()
    {
        Instance = Singleton.Get<MonsterSpawner>();
    }

    public void Spawn(int typeNumber, Vector2 position, bool needSave=true)
    {
        Monster prefab = GetMonsterPrefab(typeNumber);

        if (prefab == null) return;

        Monster spawnedMonster = Instantiate(prefab);
        spawnedMonster.Init(position);

        MonsterRegistry.Instance.Add(spawnedMonster);

        if (needSave)
        {
            DataContext.Instance.AddMonster(typeNumber);
        }
    }

    public void SpawnBox(int typeNumber, Vector2 position)
    {
        MonsterBox spawnedBox = Instantiate(_monsterBoxPrefab);
        spawnedBox.Init(typeNumber, position);
    }

    private Monster GetMonsterPrefab(int typeNumber)
    {
        return _monsterPrefabs.FirstOrDefault(p => p.TypeNumber == typeNumber);
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

    [ContextMenu("Spawn test monster")]
    private void SpawnTestMonster()
    {
        Vector2 position = CorralArea.Instance.GetRandomPosition();
        Spawn(1, position);
    }

    [ContextMenu("Spawn test box")]
    private void SpawnTestBox()
    {
        Vector2 position = CorralArea.Instance.GetRandomPosition();
        SpawnBox(1, position);
    }
}
