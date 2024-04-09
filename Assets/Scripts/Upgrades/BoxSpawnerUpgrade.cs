using System;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawnerUpgrade : Upgrade
{
    private static BoxSpawnerUpgrade _instance;
    public static BoxSpawnerUpgrade Instance
    {
        get
        {
            if (_instance == null)
                _instance = new BoxSpawnerUpgrade();
            return _instance;
        }
    }

    private BoxSpawnerUpgrade()
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
        DataContext.Instance.SetBoxSpawnerLevel(_currentLevel);
    }

    private void Level1()
    {
        float cooldown = MonsterBoxSpawner.Instance.InitialCooldown - 8f;
        MonsterBoxSpawner.Instance.SetCooldown(cooldown);
    }

    private void Level2()
    {
        float step = MonsterBoxSpawner.Instance.InitialReductionStep + 0.15f;
        MonsterBoxSpawner.Instance.SetTimeReductionStep(step);
    }

    private void Level3()
    {
        float cooldown = MonsterBoxSpawner.Instance.InitialCooldown - 10f;
        MonsterBoxSpawner.Instance.SetCooldown(cooldown);
    }

    private void Level4()
    {
        float step = MonsterBoxSpawner.Instance.InitialReductionStep + 0.2f;
        MonsterBoxSpawner.Instance.SetTimeReductionStep(step);
    }

    private void Level5()
    {
        float cooldown = MonsterBoxSpawner.Instance.InitialCooldown - 12f;
        MonsterBoxSpawner.Instance.SetCooldown(cooldown);
    }
}
