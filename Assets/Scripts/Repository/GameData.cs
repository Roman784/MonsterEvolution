using System.Collections.Generic;
using System.Numerics;

[System.Serializable]
public class GameData
{
    public int MaxMonsterLevel;
    public WalletData Wallet;
    public List<MonsterData> Monsters = new List<MonsterData>();
    public MonsterSpawnerData MonsterSpawner;
    public MergeMagnetData MergeMagnet;
    public BoxOpenerData BoxOpener;
    public float SoundVolume;
}
