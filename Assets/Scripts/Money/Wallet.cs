using System.Numerics;
using UnityEngine;
using UnityEngine.Events;

public class Wallet
{
    private static Wallet _instance;
    public static Wallet Instance
    {
        get
        {
            if (_instance == null)
                _instance = new Wallet();
            return _instance;
        }
    }

    private BigInteger _coins;
    public BigInteger Coins { get { return _coins; } }

    private int _CPS; // Coins per secon.
    public int CPS { get { return _CPS; } }
    private CooldownTimer _revenueTimer;

    private float _CPSMultiplier;
    public float InitialCPSMultiplier { get; private set; }

    public UnityEvent CoinsChanged = new UnityEvent();
    public UnityEvent CPSChanged = new UnityEvent();

    private Wallet()
    {
    }

    public void Init(BigInteger coins, float initialCPSMultiplier)
    {
        _coins = coins;
        InitialCPSMultiplier = initialCPSMultiplier;
        _CPSMultiplier = initialCPSMultiplier;

        _revenueTimer = new GameObject("RevenueTimer", typeof(CooldownTimer)).GetComponent<CooldownTimer>();
        _revenueTimer.Init(1, CollectRevenue);

        CalculateCPS();
        UpdateRenderers();

        MonsterRegistry.Instance.OnChanged.AddListener(CalculateCPS);
    }

    private void CollectRevenue()
    {
        IncreaseCoinCount(_CPS);
    }

    public void CalculateCPS()
    {
        int previousValue = _CPS;

        _CPS = 0;
        foreach (Monster monster in MonsterRegistry.Instance.Get())
        {
            _CPS += monster.Revenue;
        }

        _CPS = Mathf.FloorToInt(_CPS * _CPSMultiplier);

        if (previousValue != _CPS)
            CPSChanged.Invoke();
    }    

    public void IncreaseCoinCount(int value)
    {
        _coins += value;

        CoinsChanged.Invoke();

        UpdateRenderers();

        DataContext.Instance.SetCoinCount(Coins);
    }

    public void ReduceCoinCount(int value)
    {
        _coins -= value;

        if (_coins < 0)
            _coins = 0;

        CoinsChanged.Invoke();

        UpdateRenderers();

        DataContext.Instance.SetCoinCount(Coins);
    }

    public void SetCPSMultiplier(float value)
    {
        _CPSMultiplier = value;
        CalculateCPS();
    }

    private void UpdateRenderers()
    {
        CoinsRenderer[] renderers = GameObject.FindObjectsOfType<CoinsRenderer>();

        foreach (CoinsRenderer renderer in renderers)
        {
            renderer.UpdateAll();
        }
    }
}
