using System.Collections;
using System.Numerics;
using UnityEngine;

public class LevelBootstrap : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(BootUp());
    }

    private IEnumerator BootUp()
    {
        DataContext.Instance.Load();

        while (DataContext.Instance.GameData == null) { yield return null; }

        GameData gameData = DataContext.Instance.GameData;
        MonsterSpawnerData monsterSpawnerData = gameData.MonsterSpawner;

        MonsterSpawner.Instance.SpawnSavedMonsters();
        MonsterBoxSpawner.Instance.Init(monsterSpawnerData.TypeNumber, monsterSpawnerData.Cooldown, monsterSpawnerData.TimeReductionStep);
        Wallet.Instance.Init(BigInteger.Parse(gameData.CoinCount));
    }
}
