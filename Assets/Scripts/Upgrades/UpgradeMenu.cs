using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    public static UpgradeMenu Instance { get; private set; }

    [SerializeField] private int _shopItemCount;
    [SerializeField] private ShopItem _shopItemPrefab;
    [SerializeField] private Transform _shopItemsGrid;

    [Space]

    [SerializeField] private List<ShopItemInfo> _mergeMagnetUpgrades = new List<ShopItemInfo>();

    private void Awake()
    {
        Instance = Singleton.Get<UpgradeMenu>();
    }

    public void Init()
    {
        InitMergeMagnet();
    }

    private void InitMergeMagnet()
    {
        int upgradeLevel = DataContext.Instance.GameData.MergeMagnet.Level;

        ShopItem shopItem = Instantiate(_shopItemPrefab, _shopItemsGrid);
        shopItem.Init(_mergeMagnetUpgrades, upgradeLevel, MergeMagnetUpgrade.Instance);
    }

    public void Open()
    {

    }

    public void Close()
    {

    }
}
