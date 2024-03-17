using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner Instance {  get; private set; }

    public List<Monster> _monsterPrefabs = new List<Monster>();

    private void Awake()
    {
        Instance = Singleton.Get<MonsterSpawner>();
    }

    public void Spawn(int typeNumber, Vector2 position)
    {
        Monster prefab = GetMonsterPrefab(typeNumber);

        if (prefab == null) return;

        Monster spawnedMonster = Instantiate(prefab);
        spawnedMonster.Init(position);
    }

    private Monster GetMonsterPrefab(int typeNumber)
    {
        return _monsterPrefabs.FirstOrDefault(p => p.TypeNumber == typeNumber);
    }
}
