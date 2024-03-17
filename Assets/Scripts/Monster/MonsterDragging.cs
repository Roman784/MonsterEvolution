using UnityEngine;
using UnityEngine.Events;

public class MonsterDragging : MonoBehaviour
{
    private bool _isLifted;
    private Vector2 _offsetLiftPosition; // Смещение относительно точки подъёма.

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

    // Поднимает монстра.
    private void Lift()
    {
        _isLifted = true;

        _offsetLiftPosition = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    // Опускает монстра.
    private void Lower()
    {
        _isLifted = false;

        Lowered.Invoke();
    }

    // Перетаскивание монстра.
    private void Drag()
    {
        if (!_isLifted) return;

        Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 position = mousePosition - _offsetLiftPosition;

        transform.position = position;
    }
}
