using UnityEngine;

public class CorralArea : MonoBehaviour
{
    public static CorralArea Instance {  get; private set; }

    [SerializeField] private Vector2 _borderIndent;

    private Camera _camera;

    private void Awake()
    {
        Instance = Singleton.Get<CorralArea>();
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    public Vector2 GetRandomPosition()
    {
        Vector2 lowerLeftCorner = (Vector2)_camera.ViewportToWorldPoint(new Vector2(0, 0)) + _borderIndent;
        Vector2 upperRightCorner = (Vector2)_camera.ViewportToWorldPoint(new Vector2(1, 1)) - _borderIndent;

        float x = Random.Range(lowerLeftCorner.x, upperRightCorner.x);
        float y = Random.Range(lowerLeftCorner.y, upperRightCorner.y);

        return new Vector2(x, y);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Camera camera = Camera.main;

        Vector2 lowerLeftCorner = (Vector2)camera.ViewportToWorldPoint(new Vector2(0, 0)) + _borderIndent;
        Vector2 upperRightCorner = (Vector2)camera.ViewportToWorldPoint(new Vector2(1, 1)) - _borderIndent;

        Vector2 center = (lowerLeftCorner + upperRightCorner) / 2f;
        Vector2 size = upperRightCorner - lowerLeftCorner;

        Gizmos.DrawWireCube(center, size);
    }
}
