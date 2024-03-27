using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    private string _title;
    private string _description;
    private int _price;
    private Sprite _icon;

    private List<ShopItemInfo> _upgrades;
    private int _upgradeLevel;
    private Upgrade _upgrade;

    [SerializeField] private Button _buyButton;
    [SerializeField] private GameObject _coinShortageDesignation;

    [Space]

    [SerializeField] private TMP_Text _titleRenderer;
    [SerializeField] private TMP_Text _descriptionRenderer;
    [SerializeField] private TMP_Text _priceRenderer;
    [SerializeField] private Image _iconRenderer;

    public void Init(List<ShopItemInfo> upgrades, int upgradeLevel, Upgrade upgrade)
    {
        _upgrades = upgrades;
        _upgradeLevel = upgradeLevel;
        _upgrade = upgrade;

        SetPrice();

        _buyButton.onClick.AddListener(Buy);
        Wallet.Instance.CoinsChanged.AddListener(UpdateCoinShortageDesignation);

        UpdateRenderers();
        UpdateCoinShortageDesignation();
    }

    public void Buy()
    {
        SetPrice();

        if (Wallet.Instance.Coins < _price || _upgradeLevel >= _upgrades.Count) return;

        _upgrade.LevelUp();
        _upgradeLevel = _upgrade.CurrentLevel;

        Wallet.Instance.ReduceCoinCount(_price);

        UpdateRenderers();
        UpdateCoinShortageDesignation();
    }

    private void SetPrice()
    {
        if (_upgradeLevel < 0 || _upgradeLevel >= _upgrades.Count) return;

        _price = _upgrades[_upgradeLevel].Price;
    }

    private void UpdateRenderers()
    {
        if (_upgradeLevel >= _upgrades.Count)
        {
            _titleRenderer.text = _upgrades[_upgrades.Count - 1].Title;
            _descriptionRenderer.text = "";
            _priceRenderer.text = "max";
            _iconRenderer.sprite = _upgrades[_upgrades.Count - 1].Icon;

            _price = 0;

            return;
        }

        if (_upgradeLevel < 0) return;

        _titleRenderer.text = _upgrades[_upgradeLevel].Title;
        _descriptionRenderer.text = _upgrades[_upgradeLevel].Description;
        _priceRenderer.text = CoinsRenderer.GetFormattedValue(_upgrades[_upgradeLevel].Price).ToString();
        _iconRenderer.sprite = _upgrades[_upgradeLevel].Icon;
    }

    private void UpdateCoinShortageDesignation()
    {
        SetPrice();
        _coinShortageDesignation.SetActive(Wallet.Instance.Coins < _price);
    }
}
