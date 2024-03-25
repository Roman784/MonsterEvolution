using UnityEngine;

public class MonsterBox : MonoBehaviour
{
    private int _typeNumber;

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
        MonsterSpawner.Instance.Spawn(_typeNumber, transform.position);

        Destroy();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
