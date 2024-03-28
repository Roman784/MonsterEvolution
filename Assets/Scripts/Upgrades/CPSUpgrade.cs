using System;
using System.Collections.Generic;

public class CPSUpgrade : Upgrade
{
    private static CPSUpgrade _instance;
    public static CPSUpgrade Instance
    {
        get
        {
            if (_instance == null)
                _instance = new CPSUpgrade();
            return _instance;
        }
    }

    private CPSUpgrade()
    {
        _currentLevel = 0;
        InitLevels();
    }

    private void InitLevels()
    {
        _levels[1] = Level1;
        _levels[2] = Level2;
        _levels[3] = Level3;
        _levels[4] = Level4;
        _levels[5] = Level5;
    }

    protected override void Save()
    {
        DataContext.Instance.SetCPSLevel(_currentLevel);
    }

    private void Level1()
    {
        float multiplier = Wallet.Instance.InitialCPSMultiplier + 0.05f;
        Wallet.Instance.SetCPSMultiplier(multiplier);
    }

    private void Level2()
    {
        float multiplier = Wallet.Instance.InitialCPSMultiplier + 0.1f;
        Wallet.Instance.SetCPSMultiplier(multiplier);
    }

    private void Level3()
    {
        float multiplier = Wallet.Instance.InitialCPSMultiplier + 0.15f;
        Wallet.Instance.SetCPSMultiplier(multiplier);
    }

    private void Level4()
    {
        float multiplier = Wallet.Instance.InitialCPSMultiplier + 0.2f;
        Wallet.Instance.SetCPSMultiplier(multiplier);
    }

    private void Level5()
    {
        float multiplier = Wallet.Instance.InitialCPSMultiplier + 0.25f;
        Wallet.Instance.SetCPSMultiplier(multiplier);
    }
}
