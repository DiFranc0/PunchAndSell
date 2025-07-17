using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] PoolableObject[] pools;
    [SerializeField] Transform activePersonsContainer;

    private void Awake()
    {
        foreach (var pool in pools)
        {
            pool.Initialize(transform, activePersonsContainer);
        }
    }

}

