using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeIncome : MonoBehaviour
{
    private Cube _cube;
    private Cube Cube => _cube == null ? _cube = GetComponentInParent<Cube>() : _cube;

    private const float INCOME = 10;

    private void OnEnable()
    {
        Cube.OnDestroyed.AddListener(CheckIncome);
    }

    private void OnDisable()
    {
        Cube.OnDestroyed.RemoveListener(CheckIncome);
    }

    private void CheckIncome() 
    {
        if (!Cube.IsMatched)
            return;

        CreateCoin();
    }

    private void CreateCoin() 
    {
        CoinPanel.Instance.CreateCoin(transform.position, INCOME);
    }
}
