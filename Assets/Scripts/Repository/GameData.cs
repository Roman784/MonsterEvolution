using System.Collections.Generic;
using System.Numerics;

[System.Serializable]
public class GameData
{
    public string CoinCount;
    public List<MonsterData> Monsters = new List<MonsterData>();
    public MonsterSpawnerData MonsterSpawner;
}
