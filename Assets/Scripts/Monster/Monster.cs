using UnityEngine;

[RequireComponent(typeof(MonsterDragging), typeof(MonsterMerging))]
public class Monster : MonoBehaviour
{
    public static readonly int MaxTypeNumber = 2;

    [SerializeField] private int _typeNumber;
    public int TypeNumber { get { return _typeNumber; } }

    public MonsterDragging Dragging {  get; private set; }
    public MonsterMerging Merging { get; private set; }

    private void Awake()
    {
        Dragging = GetComponent<MonsterDragging>();
        Merging = GetComponent<MonsterMerging>();
    }

    public void Init(Vector2 position)
    {
        transform.position = position;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
