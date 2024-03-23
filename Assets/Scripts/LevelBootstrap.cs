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

        MonsterSpawner.Instance.SpawnSavedMonsters();
        Wallet.Instance.Init(BigInteger.Parse(DataContext.Instance.GameData.CoinCount));
    }
}
