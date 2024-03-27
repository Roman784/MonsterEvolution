using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade
{
    protected int _currentLevel;
    public int CurrentLevel { get { return _currentLevel; } }
    protected Dictionary<int, Action> _levels = new Dictionary<int, Action>();

    public void LevelUp()
    {
        if (_currentLevel >= _levels.Count) return;

        LevelUp(_currentLevel + 1);

        Save();
    }

    public void LevelUp(int level)
    {
        for (int i = 1; i <= level; i++)
        {
            if (!_levels.ContainsKey(i) || i <= _currentLevel) continue;

            _levels[i].Invoke();
            _currentLevel = i;
        }
    }

    protected abstract void Save();
}
