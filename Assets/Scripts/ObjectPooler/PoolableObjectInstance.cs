using UnityEngine;

public class PoolableObjectInstance : MonoBehaviour
{
    public PoolableObject pool;

    public void ReturnToPool()
    {
        pool.ReturnObject(gameObject);
    }


}
