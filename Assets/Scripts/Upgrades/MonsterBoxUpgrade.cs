using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBoxUpgrade : Upgrade
{
    private static MonsterBoxUpgrade _instance;
    public static MonsterBoxUpgrade Instance
    {
        get
        {
            if (_instance == null)
                _instance = new MonsterBoxUpgrade();
            return _instance;
        }
    }

    private MonsterBoxUpgrade()
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
        DataContext.Instance.SetMonsterBoxLevel(_currentLevel);
    }

    private void Level1()
    {
        int number = MonsterBoxSpawner.Instance.InitialTypeNumber + 1;
        MonsterBoxSpawner.Instance.SetTypeNumber(number);
    }

    private void Level2()
    {
        int number = MonsterBoxSpawner.Instance.InitialTypeNumber + 2;
        MonsterBoxSpawner.Instance.SetTypeNumber(number);
    }

    private void Level3()
    {
        int number = MonsterBoxSpawner.Instance.InitialTypeNumber + 3;
        MonsterBoxSpawner.Instance.SetTypeNumber(number);
    }

    private void Level4()
    {
        int number = MonsterBoxSpawner.Instance.InitialTypeNumber + 4;
        MonsterBoxSpawner.Instance.SetTypeNumber(number);
    }

    private void Level5()
    {
        int number = MonsterBoxSpawner.Instance.InitialTypeNumber + 5;
        MonsterBoxSpawner.Instance.SetTypeNumber(number);
    }
}
