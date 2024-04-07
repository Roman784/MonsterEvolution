using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;

    private float _speed;

    public void MoveTo(Vector2 position)
    {
        transform.position = Vector2.MoveTowards(transform.position, position, _speed * Time.deltaTime);

        Rotate(position);
    }

    private void Rotate(Vector2 position)
    {
        if (position.x < transform.position.x) 
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (position.x >  transform.position.x) 
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    public void SetSpeed(SpeedTypes speedType)
    {
        if (speedType == SpeedTypes.Walk) _speed = _walkSpeed;
        else if (speedType == SpeedTypes.Run) _speed = _runSpeed;
    }
}
