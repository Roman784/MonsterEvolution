using UnityEngine;

public class MonsterBehaviorWalking : MonsterBehavior
{
    private Vector2 _position;

    public MonsterBehaviorWalking(Monster monster) : base(monster)
    {
    }

    public override void Enter()
    {
        IsFinished = false;

        _monster.Movement.SetSpeed(SpeedTypes.Walk);
        _monster.Animation.SetIntensity(SpeedTypes.Walk);

        _position = CorralArea.Instance.GetRandomPosition();
    }

    public override void Update()
    {
        _monster.Movement.MoveTo(_position);

        if ((Vector2)_monster.transform.position == _position)
        {
            IsFinished = true;
        }
    }
}
