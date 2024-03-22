using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    public static Wallet Instance { get; private set; }

    private int _coinCount;
    public int CoinCount { get { return _coinCount; } }

    private int _CPS; // Coins per secon.
    public int CPS { get { return _CPS; } }
    private CooldownTimer _revenueTimer;

    public UnityEvent CoinCountChanged = new UnityEvent();
    public UnityEvent CPSChanged = new UnityEvent();

    private void Awake()
    {
        Instance = Singleton.Get<Wallet>();

        MonsterRegistry.Instance.OnChanged.AddListener(CalculateCPS);

        _revenueTimer = new CooldownTimer(1, CollectRevenue);
    }

    public void Init(int coinCount)
    {
        _coinCount = coinCount;

        CalculateCPS();
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
        _coinCount += value;

        CoinCountChanged.Invoke();

        DataContext.Instance.SetCoinCount(CoinCount);
    }

    public void ReduceCoinCount(int value)
    {
        _coinCount -= value;

        if (_coinCount < 0)
            _coinCount = 0;

        CoinCountChanged.Invoke();

        DataContext.Instance.SetCoinCount(CoinCount);
    }
}
