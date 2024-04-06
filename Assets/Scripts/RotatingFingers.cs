using UnityEngine;

public class RotatingFingers : MonoBehaviour
{
    [SerializeField] private Transform _finger;
    [SerializeField] private float _speed;
    [SerializeField] private float _range;

    private float _moveTime;

    private void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        _moveTime += Time.deltaTime * _speed;

        float x = Mathf.Cos(_moveTime - Mathf.PI / 2f) * _range;
        float y = Mathf.Sin(_moveTime - Mathf.PI / 2f) * _range;

        _finger.position = new Vector2(x, y) + (Vector2)transform.position;
    }

    private void Rotate()
    {
        Vector2 direction = (transform.position - _finger.position).normalized;
        float z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        _finger.rotation = Quaternion.Euler(0f, 0f, z);
    }
}
