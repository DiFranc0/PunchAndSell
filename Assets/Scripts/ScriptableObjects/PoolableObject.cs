using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PoolableObject", menuName = "Object Pooling/Poolable Object")]
public class PoolableObject : ScriptableObject
{
    public GameObject[] prefabs;
    public int initialPoolSize = 10;

    [System.NonSerialized] private Queue<GameObject> objectPool = new Queue<GameObject>();
    [System.NonSerialized] private Transform parentTransform;
    [System.NonSerialized] public Transform parentPersons; // Parent for spawned persons

    public void Initialize(Transform parent, Transform personsParent)
    {
        parentTransform = parent;
        parentPersons = personsParent;

        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewObject();
        }
    }

    private GameObject CreateNewObject()
    {
        GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Length-1)], parentTransform);
        obj.SetActive(false);
        obj.GetComponent<PoolableObjectInstance>().pool = this;
        objectPool.Enqueue(obj);
        return obj;
    }

    /*public GameObject GetObject(Vector3 position, Quaternion rotation)
    {
        if (objectPool.Count == 0)
        {
            CreateNewObject();
        }

        GameObject obj = objectPool.Dequeue();
        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);
        return obj;
    }*/

    public GameObject GetObject(Transform pos)
    {
        if (objectPool.Count == 0)
        {
            CreateNewObject();
        }

        GameObject obj = objectPool.Dequeue();

        foreach(var col in obj.GetComponentsInChildren<Collider>())
        {
            col.enabled = true;
        }

        obj.transform.SetParent(parentPersons);
        obj.transform.SetPositionAndRotation(pos.position, pos.rotation);
        obj.GetComponent<WaypointSetter>()?.SetCurrentWaypoint(pos);
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.transform.SetParent(parentTransform);
        obj.SetActive(false);
        
        objectPool.Enqueue(obj);
    }
}
