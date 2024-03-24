using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Monster))]
public class MonsterDragging : MonoBehaviour
{
    private bool _isLifted;
    public bool IsLifted { get { return _isLifted; } }
    private Vector2 _offsetLiftPosition; // �������� ������������ ����� �������.

    public UnityEvent Lifted { get; private set; } = new UnityEvent();
    public UnityEvent Lowered { get; private set; } = new UnityEvent();

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Drag();
    }

    private void OnMouseDown()
    {
        Lift();
    }

    private void OnMouseUp()
    {
        Lower();
    }

    // ��������� �������.
    private void Lift()
    {
        _isLifted = true;

        _offsetLiftPosition = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        Lifted.Invoke();
    }

    // �������� �������.
    private void Lower()
    {
        _isLifted = false;

        Lowered.Invoke();
    }

    // �������������� �������.
    private void Drag()
    {
        if (!_isLifted) return;

        Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 position = mousePosition - _offsetLiftPosition;

        transform.position = position;
    }
}
