using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public int CoinCount;
    public List<MonsterData> Monsters = new List<MonsterData>();
}
