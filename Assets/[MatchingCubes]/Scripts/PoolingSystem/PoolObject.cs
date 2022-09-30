using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public string PoolID { get; private set; }

    private Vector3 _defaultScale;

    private void Awake()
    {
        _defaultScale = transform.localScale;
    }

    public void Initialize(string poolID) 
    {
        PoolID = poolID;
    }

    public void OnSendBackToPool() 
    {
        transform.localScale = _defaultScale;
    }
}
