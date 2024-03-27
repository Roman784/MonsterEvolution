using System;
using System.Collections.Generic;

public class MergeMagnetUpgrade : Upgrade
{
    private static MergeMagnetUpgrade _instance;
    public static MergeMagnetUpgrade Instance
    {
        get
        {
            if (_instance == null)
                _instance = new MergeMagnetUpgrade();
            return _instance;
        }
    }

    private MergeMagnetUpgrade()
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
        DataContext.Instance.SetMergeMagnedLevel(_currentLevel);
    }

    private void Level1()
    {
        MergeMagnet.Instance.Enable();
    }

    private void Level2() 
    {
        float cooldown = MergeMagnet.Instance.InitialCooldown - 5f;
        MergeMagnet.Instance.SetCooldown(cooldown);
    }

    private void Level3()
    {
        float cooldown = MergeMagnet.Instance.InitialCooldown - 10f;
        MergeMagnet.Instance.SetCooldown(cooldown);
    }

    private void Level4()
    {
        float cooldown = MergeMagnet.Instance.InitialCooldown - 15f;
        MergeMagnet.Instance.SetCooldown(cooldown);
    }

    private void Level5()
    {
        MergeMagnet.Instance.SetCoupleCountAtTime(2);
    }
}
