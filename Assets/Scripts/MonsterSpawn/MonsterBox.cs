using UnityEngine;

public class MonsterBox : MonoBehaviour
{
    private int _typeNumber;

    [SerializeField] private Transform _spawnPoint;

    public void Init(int typeNumber, Vector2 position)
    {
        _typeNumber = typeNumber;

        transform.position = position;
    }

    private void OnMouseUp()
    {
        Open();
    }

    public void Open()
    {
        MonsterSpawner.Instance.Spawn(_typeNumber, _spawnPoint.position);

        Destroy();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
