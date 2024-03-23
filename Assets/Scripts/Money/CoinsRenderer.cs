using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TMPro;
using UnityEngine;

public class CoinsRenderer : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsRenderer;
    [SerializeField] private TMP_Text _CPSRenderer;

    // For values of one million and more.
    private Dictionary<BigInteger, string> _thresholdsAndSuffixes = new Dictionary<BigInteger, string>();

    private void Awake()
    {
        InitThreahholds();
    }

    private void Start()
    {
        Wallet.Instance.CoinsChanged.AddListener(UpdateCoinsRenderer);
        Wallet.Instance.CPSChanged.AddListener(UpdateCPSRenderer);
    }

    private void InitThreahholds()
    {
        _thresholdsAndSuffixes[1000000] = "Mi";
        _thresholdsAndSuffixes[1000000000] = "Bi";
        _thresholdsAndSuffixes[1000000000000] = "Tr";
        _thresholdsAndSuffixes[1000000000000000] = "Qu";

        _thresholdsAndSuffixes = _thresholdsAndSuffixes.OrderByDescending(t => t.Key)
                                                       .ToDictionary(t => t.Key, s => s.Value);
    }

    public void UpdateAll()
    {
        UpdateCoinsRenderer();
        UpdateCPSRenderer();
    }

    private void UpdateCoinsRenderer()
    {
        if (_coinsRenderer == null) return;

        BigInteger coins = Wallet.Instance.Coins;
        _coinsRenderer.text = GetFormattedValue(coins);
    }

    private void UpdateCPSRenderer()
    {
        if (_CPSRenderer == null) return;

        int CPS = Wallet.Instance.CPS;
        _CPSRenderer.text = GetFormattedValue(CPS) + " coin/sec";
    }

    private string GetFormattedValue(BigInteger value)
    {
        // For values of one million and more.
        foreach (var item in _thresholdsAndSuffixes)
        {
            if (value >= item.Key)
            {
                return (value / item.Key).ToString() + "." + value.ToString()[1] + " " + item.Value;
            }
        }

        if (value >= 1000)
        {
            return (value / 1000).ToString() + "," + (value % 1000).ToString("D3");
        }

        return value.ToString();
    }
}