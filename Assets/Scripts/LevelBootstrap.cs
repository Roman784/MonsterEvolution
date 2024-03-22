using System.Collections;
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
        Wallet.Instance.Init(DataContext.Instance.GameData.CoinCount);
    }
}
