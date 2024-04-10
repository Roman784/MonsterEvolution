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

        Localization.Instance.Init(Langs.En);

        GameData gameData = DataContext.Instance.GameData;
        WalletData walletData = gameData.Wallet;
        MonsterSpawnerData monsterSpawnerData = gameData.MonsterSpawner;
        MergeMagnetData mergeMagnetData = gameData.MergeMagnet;
        BoxOpenerData boxOpenerData = gameData.BoxOpener;

        MonsterSpawner.Instance.SpawnSavedMonsters();
        MonsterBoxSpawner.Instance.Init(monsterSpawnerData.InitialTypeNumber, monsterSpawnerData.InitialCooldown, monsterSpawnerData.InitialTimeReductionStep);

        Wallet.Instance.Init(BigInteger.Parse(walletData.CoinCount), walletData.InitialCPSMultiplier);
        MergeMagnet.Instance.Init(mergeMagnetData.InitialCooldown, mergeMagnetData.InitialCoupleCountAtTime);
        BoxOpener.Instance.Init(boxOpenerData.InitialCooldown, boxOpenerData.InitialCountAtTime);

        MergeMagnetUpgrade.Instance.LevelUp(mergeMagnetData.Level);
        BoxOpenerUpgrade.Instance.LevelUp(boxOpenerData.Level);
        BoxSpawnerUpgrade.Instance.LevelUp(monsterSpawnerData.BoxSpawnerLevel);
        MonsterBoxUpgrade.Instance.LevelUp(monsterSpawnerData.MonsterBoxLevel);
        CPSUpgrade.Instance.LevelUp(walletData.CPSLevel);

        UpgradeMenu.Instance.Init();

        Sound.Instance.Init(gameData.SoundVolume);
        MusicPlayer.Instance.Init(gameData.SoundVolume);
    }
}
