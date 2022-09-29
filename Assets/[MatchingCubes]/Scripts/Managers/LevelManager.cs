using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public int CurrentLevelIndex => SceneManager.GetActiveScene().buildIndex;
    public int NextLevelIndex 
    { 
        get
        {
            int index = CurrentLevelIndex + 1;
            int maxIndex = SceneManager.sceneCountInBuildSettings - 1;
            index = Mathf.Min(index, maxIndex);
            return index;
        } 
    }
    
    public bool IsLevelStarted { get; private set; }
    public bool IsLoading { get; private set; }

    [HideInInspector]
    public UnityEvent OnLevelStarted = new UnityEvent();   

    private void OnEnable()
    {
        InputManager.Instance.OnTouched.AddListener(StartLevel);
        EventManager.OnSceneLoaded.AddListener(ResetValues);
    }

    private void OnDisable()
    {     
        InputManager.Instance.OnTouched.RemoveListener(StartLevel);
        EventManager.OnSceneLoaded.RemoveListener(ResetValues);
    }

    public void RestartLevel() 
    {        
        LoadScene(CurrentLevelIndex);
    }   

    public void NextLevel() 
    {
        LoadScene(NextLevelIndex);
    }

    private void StartLevel() 
    {
        if (IsLevelStarted)
            return;

        IsLevelStarted = true;
        OnLevelStarted.Invoke();
    }

    private void LoadScene(int buildIndex) 
    {
        if (IsLoading)
            return;

        StartCoroutine(LoadSceneCoroutine(buildIndex));
    }

    private IEnumerator LoadSceneCoroutine(int buildIndex)
    {
        IsLoading = true;
        EventManager.OnSceneLoading.Invoke();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(buildIndex);   
        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        IsLoading = false;
        EventManager.OnSceneLoaded.Invoke();
    }

    private void ResetValues() 
    {
        IsLevelStarted = false;
    }
}
