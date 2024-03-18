using UnityEngine;

[RequireComponent(typeof(MonsterBehavior), typeof(MonsterDragging), typeof(MonsterMerging))]
[RequireComponent(typeof(MonsterMovement))]
public class Monster : MonoBehaviour
{
    public static readonly int MaxTypeNumber = 2;

    [SerializeField] private int _typeNumber;
    public int TypeNumber { get { return _typeNumber; } }

    public MonsterBehavior Behavior { get; private set; }
    public MonsterDragging Dragging { get; private set; }
    public MonsterMerging Merging { get; private set; }
    public MonsterMovement Movement { get; private set; }

    private void Awake()
    {
        Behavior = GetComponent<MonsterBehavior>();
        Dragging = GetComponent<MonsterDragging>();
        Merging = GetComponent<MonsterMerging>();
        Movement = GetComponent<MonsterMovement>();
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
