using UnityEngine;

public class Centering : MonoBehaviour
{
    [SerializeField] private RectTransform _elem1;
    [SerializeField] private RectTransform _elem2;
    [SerializeField] private float _offset;

    private Vector3 _previousPosition;

    private void Start()
    {
        //UpdatePosition();
    }

    public void UpdatePosition()
    {
        /*float x = ((_elem1.rect.width + _elem2.rect.width) / 2f - _elem1.rect.width) * -1f;

        if (Mathf.Abs(_previousPosition.x - x) > _offset)
        {
            _previousPosition = transform.localPosition;
            transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
        }*/
    }
}
