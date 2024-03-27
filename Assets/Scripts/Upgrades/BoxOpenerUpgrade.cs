using System;
using System.Collections.Generic;
using UnityEngine;

public class BoxOpenerUpgrade : Upgrade
{
    private static BoxOpenerUpgrade _instance;
    public static BoxOpenerUpgrade Instance
    {
        get
        {
            if (_instance == null)
                _instance = new BoxOpenerUpgrade();
            return _instance;
        }
    }

    private BoxOpenerUpgrade()
    {
        _currentLevel = 0;
        InitLevels();
    }

    private void InitLevels()
    {
        _levels = new Dictionary<int, Action>();

        _levels[1] = Level1;
        _levels[2] = Level2;
        _levels[3] = Level3;
        _levels[4] = Level4;
        _levels[5] = Level5;
    }

    protected override void Save()
    {
        DataContext.Instance.SetBoxOpenerLevel(_currentLevel);
    }

    private void Level1()
    {
        BoxOpener.Instance.Enable();
    }

    private void Level2()
    {
        float cooldown = BoxOpener.Instance.InitialCooldown - 5f;
        BoxOpener.Instance.SetCooldown(cooldown);
    }

    private void Level3()
    {
        float cooldown = BoxOpener.Instance.InitialCooldown - 10f;
        BoxOpener.Instance.SetCooldown(cooldown);
    }

    private void Level4()
    {
        float cooldown = BoxOpener.Instance.InitialCooldown - 15f;
        BoxOpener.Instance.SetCooldown(cooldown);
    }

    private void Level5()
    {
        BoxOpener.Instance.SetCountAtTime(2);
    }
}
