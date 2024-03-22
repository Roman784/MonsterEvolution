using UnityEngine;

public class MonsterBehaviorReentry : MonsterBehavior
{
    private Vector2 _position;

    public MonsterBehaviorReentry(Monster monster) : base(monster)
    {
    }

    public override void Enter()
    {
        IsFinished = false;

        _monster.Movement.SetSpeed(SpeedTypes.Run);
        _position = CorralArea.Instance.GetRandomPosition();
    }

    public override void Update()
    {
        _monster.Movement.MoveTo(_position);

        if (!CorralArea.Instance.IsWithin(_position))
        {
            _position = CorralArea.Instance.GetRandomPosition();
        }

        if ((Vector2)_monster.transform.position == _position)
        {
            IsFinished = true;
        }
    }
}
