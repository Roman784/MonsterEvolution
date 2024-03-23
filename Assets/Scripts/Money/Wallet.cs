using System.Numerics;
using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    public static Wallet Instance { get; private set; }

    private BigInteger _coins;
    public BigInteger Coins { get { return _coins; } }

    private int _CPS; // Coins per secon.
    public int CPS { get { return _CPS; } }
    private CooldownTimer _revenueTimer;

    public UnityEvent CoinsChanged = new UnityEvent();
    public UnityEvent CPSChanged = new UnityEvent();

    private void Awake()
    {
        Instance = Singleton.Get<Wallet>();

        MonsterRegistry.Instance.OnChanged.AddListener(CalculateCPS);

        _revenueTimer = new CooldownTimer(1, CollectRevenue);
    }

    public void Init(BigInteger coins)
    {
        _coins = coins;

        CalculateCPS();
        UpdateRenderers();
    }

    private void Update()
    {
        _revenueTimer.Update();
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

        if (previousValue != _CPS)
            CPSChanged.Invoke();
    }    

    public void IncreaseCoinCount(int value)
    {
        _coins += value;

        CoinsChanged.Invoke();

        DataContext.Instance.SetCoinCount(Coins);
    }

    public void ReduceCoinCount(int value)
    {
        _coins -= value;

        if (_coins < 0)
            _coins = 0;

        CoinsChanged.Invoke();

        DataContext.Instance.SetCoinCount(Coins);
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
