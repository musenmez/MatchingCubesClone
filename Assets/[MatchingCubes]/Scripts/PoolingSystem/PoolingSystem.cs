using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingSystem : Singleton<PoolingSystem>
{
    private Dictionary<string, Stack<GameObject>> _poolStacksByID = new Dictionary<string, Stack<GameObject>>();
    public Dictionary<string, Stack<GameObject>> PoolStacksByID { get => _poolStacksByID; private set => _poolStacksByID = value; }

    private Dictionary<string, Pool> _poolsByID = new Dictionary<string, Pool>();
    public Dictionary<string, Pool> PoolsByID { get => _poolsByID; private set => _poolsByID = value; }

    [SerializeField] private List<Pool> _pools = new List<Pool>();

    private void Awake()
    {
        SetPoolCollection();
        SetInitialPoolStacks();
    }

    public GameObject InstantiateFromPool(string poolID, Vector3 position, Quaternion rotation) 
    {
        if (!PoolStacksByID.ContainsKey(poolID))
        {
            Debug.LogError("Pool with ID " + poolID + "dosen't exist.");
            return null;
        }

        GameObject poolObject;
       
        if (PoolStacksByID[poolID].Count <= 0) 
        {
            poolObject = GetExtraObject(poolID);
        }
        else
        {
            poolObject = PoolStacksByID[poolID].Pop();
            if (poolObject == null)
                poolObject = GetExtraObject(poolID);
        }

        if (poolObject == null)
            return null;

        poolObject.transform.SetPositionAndRotation(position, rotation);
        poolObject.SetActive(true);

        return poolObject;
    }

    public void DestroyPoolObject(GameObject obj) 
    {
        if (!obj.TryGetComponent(out PoolObject poolObject))
            return;

        if (!PoolStacksByID.ContainsKey(poolObject.PoolID))
            return;

        obj.SetActive(false);
        obj.transform.SetParent(transform);
        poolObject.OnSendBackToPool();

        PoolStacksByID[poolObject.PoolID].Push(obj);
    }

    private void SetInitialPoolStacks() 
    {
        foreach (var pool in _pools)
        {
            Stack<GameObject> poolStack = new Stack<GameObject>();
            for (int i = 0; i < pool.InitialSize; i++)
            {
                GameObject poolObject = CreatePoolObject(pool.PoolID, pool.Prefab);
                poolStack.Push(poolObject);
            }

            if (!PoolStacksByID.ContainsKey(pool.PoolID))
            {
                PoolStacksByID.Add(pool.PoolID, poolStack);
            }
        }
    }

    private void SetPoolCollection() 
    {
        foreach (var pool in _pools)
        {
            if (!PoolsByID.ContainsKey(pool.PoolID))
            {
                PoolsByID.Add(pool.PoolID, pool);
            }
        }
    }

    private GameObject GetExtraObject(string poolID) 
    {
        if (!PoolsByID.ContainsKey(poolID))
            return null;       
    
        return CreatePoolObject(poolID, PoolsByID[poolID].Prefab);
    }

    private GameObject CreatePoolObject(string poolID, GameObject prefab) 
    {
        GameObject poolObject = Instantiate(prefab);            

        if (!poolObject.TryGetComponent(out PoolObject _))
        {
            poolObject.AddComponent<PoolObject>().Initialize(poolID);
        }

        poolObject.transform.SetParent(transform);
        poolObject.SetActive(false);
        return poolObject;
    }
}
