using UnityEngine;

public class CorralArea : MonoBehaviour
{
    public static CorralArea Instance {  get; private set; }

    [SerializeField] private Vector2 _borderIndent;

    Vector2 _lowerLeftCorner;
    Vector2 _upperRightCorner;

    private Camera _camera;

    private void Awake()
    {
        Instance = Singleton.Get<CorralArea>();

        _camera = Camera.main;
    }
    
    private void LocateCornerPositions()
    {
        _lowerLeftCorner = (Vector2)_camera.ViewportToWorldPoint(new Vector2(0, 0)) + _borderIndent;
        _upperRightCorner = (Vector2)_camera.ViewportToWorldPoint(new Vector2(1, 1)) - _borderIndent;
    }

    public Vector2 GetRandomPosition()
    {
        LocateCornerPositions();

        float x = Random.Range(_lowerLeftCorner.x, _upperRightCorner.x);
        float y = Random.Range(_lowerLeftCorner.y, _upperRightCorner.y);

        return new Vector2(x, y);
    }

    public bool IsWithin(Vector2 position)
    {
        LocateCornerPositions();

        return position.x > _lowerLeftCorner.x && 
               position.x < _upperRightCorner.x &&
               position.y > _lowerLeftCorner.y && 
               position.y < _upperRightCorner.y;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Camera camera = Camera.main;

        Vector2 _lowerLeftCorner = (Vector2)camera.ViewportToWorldPoint(new Vector2(0, 0)) + _borderIndent;
        Vector2 _upperRightCorner = (Vector2)camera.ViewportToWorldPoint(new Vector2(1, 1)) - _borderIndent;

        Vector2 center = (_lowerLeftCorner + _upperRightCorner) / 2f;
        Vector2 size = _upperRightCorner - _lowerLeftCorner;

        Gizmos.DrawWireCube(center, size);
    }
}
