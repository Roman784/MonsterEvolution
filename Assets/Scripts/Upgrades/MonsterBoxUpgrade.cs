using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBoxUpgrade
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

    public int CurrentLevel { get; private set; }
    private Dictionary<int, Action> _levels;

    private MonsterBoxUpgrade()
    {
        CurrentLevel = 0;
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

    public void LevelUp(int level)
    {
        for (int i = 1; i <= level; i++)
        {
            if (!_levels.ContainsKey(i) || i <= CurrentLevel) continue;

            _levels[i].Invoke();
            CurrentLevel = i;
        }
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
