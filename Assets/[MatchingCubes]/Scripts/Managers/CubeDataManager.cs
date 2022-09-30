using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDataManager : Singleton<CubeDataManager>
{
    private Dictionary<CubeType, CubeData> _cubeDatasByType = new Dictionary<CubeType, CubeData>();
    public Dictionary<CubeType, CubeData> CubeDatasByType { get => _cubeDatasByType; private set => _cubeDatasByType = value; }

    [SerializeField] private List<CubeData> _cubeDatas = new List<CubeData>();
    public List<CubeData> CubeDatas => _cubeDatas;

    private void Awake()
    {
        SetCubeDataCollection();
    }

    public CubeData GetCubeData(CubeType cubeType) 
    {
        try
        {
            return CubeDatasByType[cubeType];
        }
        catch
        {
            return null;
        }
    }

    private void SetCubeDataCollection() 
    {
        foreach (var cubeData in CubeDatas)
        {
            if (CubeDatasByType.ContainsKey(cubeData.CubeType))
                continue;

            CubeDatasByType.Add(cubeData.CubeType, cubeData);
        }
    }
}
