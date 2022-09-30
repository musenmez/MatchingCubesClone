using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraBrain : MonoBehaviour
{
    public static CameraBrain Instance;

    private CinemachineBrain _brain;
    public CinemachineBrain CinemachineBrain => _brain == null ? _brain = GetComponent<CinemachineBrain>() : _brain;

    private void Awake()
    {
        Instance = this;
    }
}
