using UnityEngine;

public class MonsterBehaviorLifting : MonsterBehavior
{
    public MonsterBehaviorLifting(Monster monster) : base(monster)
    {
        monster.Dragging.Lowered.AddListener(Finish);
    }

    public override void Enter()
    {
        IsFinished = false;

        _monster.Animation.SetIntensity(SpeedTypes.Idle);
    }

    public override void Update()
    {
    }

    private void Finish()
    {
        IsFinished = true;
    }
}
