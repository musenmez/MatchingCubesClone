using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{    
    public float Coin { get; set; }

    private void Awake()
    {
        LoadData();
    }

    private void SaveData() 
    {
        PlayerPrefs.SetFloat(PlayerPrefsKeys.Coin, Coin);
    }

    private void LoadData() 
    {
        Coin = PlayerPrefs.GetFloat(PlayerPrefsKeys.Coin, 0);
    }

    private void OnApplicationPause(bool pause)
    {
        SaveData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
