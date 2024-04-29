using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : PanelMenu
{
    public static UpgradeMenu Instance { get; private set; }

    [SerializeField] private int _shopItemCount;
    [SerializeField] private ShopItem _shopItemPrefab;
    [SerializeField] private Transform _shopItemsGrid;

    [Space]

    [SerializeField] private List<ShopItemInfo> _mergeMagnetUpgrades = new List<ShopItemInfo>();
    [SerializeField] private List<ShopItemInfo> _boxOpenerUpgrades = new List<ShopItemInfo>();
    [SerializeField] private List<ShopItemInfo> _boxSpawnerUpgrades = new List<ShopItemInfo>();
    [SerializeField] private List<ShopItemInfo> _monsterBoxUpgrades = new List<ShopItemInfo>();
    [SerializeField] private List<ShopItemInfo> _CPSMultiplierUpgrades = new List<ShopItemInfo>();

    [Space]

    [SerializeField] private AdPanel _adPanel;

    private void Awake()
    {
        Instance = Singleton.Get<UpgradeMenu>();
    }

    public void Init()
    {
        GameData gameData = DataContext.Instance.GameData;

        InitShopItem(gameData.MonsterSpawner.BoxSpawnerLevel, _boxSpawnerUpgrades, BoxSpawnerUpgrade.Instance);
        InitShopItem(gameData.MonsterSpawner.MonsterBoxLevel, _monsterBoxUpgrades, MonsterBoxUpgrade.Instance);
        InitShopItem(gameData.MergeMagnet.Level, _mergeMagnetUpgrades, MergeMagnetUpgrade.Instance);
        InitShopItem(gameData.BoxOpener.Level, _boxOpenerUpgrades, BoxOpenerUpgrade.Instance);
        InitShopItem(gameData.Wallet.CPSLevel, _CPSMultiplierUpgrades, CPSUpgrade.Instance);

        Instantiate(new GameObject("Pacifier").AddComponent<RectTransform>(), _shopItemsGrid);

        YandexReceiver.Rewarded.AddListener(GetAdReward);
    }

    private void InitShopItem(int upgradeLevel, List<ShopItemInfo> upgrades, Upgrade upgrade)
    {
        ShopItem shopItem = Instantiate(_shopItemPrefab, _shopItemsGrid);
        shopItem.Init(upgrades, upgradeLevel, upgrade);
    }

    public new void Open()
    {
        base.Open();

        YandexSender.Instance.ShowFullscreenAdv();
    }

    public void ShowAd()
    {
        //YandexSender.Instance.ShowRewardedVideo();
        _adPanel.Open();
    }

    private void GetAdReward()
    {
        Wallet.Instance.IncreaseCoinCount(2000);
    }
}
