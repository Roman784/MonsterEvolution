using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private List<T> _objects = new List<T>();
    private T _prefab;

    public ObjectPool(T prefab, int objectCount)
    {
        _prefab = prefab;

        for (int i = 0; i < objectCount; i++)
        {
            Create();
        }
    }

    public T Get(bool isActive = true)
    {
        T vacantObject = _objects.FirstOrDefault(x => !x.isActiveAndEnabled);
        if (vacantObject == null) vacantObject = Create();

        vacantObject.gameObject.SetActive(isActive);

        return vacantObject;
    }

    public T Get(Vector3 position, Quaternion rotation, bool isActive = true)
    {
        T vacantObject = Get(isActive);

        vacantObject.transform.position = position;
        vacantObject.transform.rotation = rotation;

        return vacantObject;
    }

    public void Release(T releasedObject)
    {
        releasedObject.gameObject.SetActive(false);
    }

    private T Create()
    {
        T newObject = GameObject.Instantiate(_prefab);
        newObject.gameObject.SetActive(false);
        _objects.Add(newObject);

        return newObject;
    }
}
