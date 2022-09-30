using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private float _coin;
    public float Coin 
    {
        get 
        {
            return _coin;
        }

        set 
        {
            _coin = value;
            PlayerPrefs.SetFloat(PlayerPrefsKeys.Coin, _coin);
        }
    }

    private void Awake()
    {
        LoadData();
    }  

    private void LoadData() 
    {
        Coin = PlayerPrefs.GetFloat(PlayerPrefsKeys.Coin, 0);
    }    
}
