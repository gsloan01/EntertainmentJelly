using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public UnityEvent preloadSceneEvent;
    public UnityEvent postloadSceneEvent;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnLoadScene(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

    IEnumerator LoadScene(string sceneName)
    {
        preloadSceneEvent?.Invoke();
        SceneManager.LoadScene(sceneName);

        yield return null;
    }

    public void OnReloadCurrentScene()
    {
        OnLoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        postloadSceneEvent?.Invoke();
    }

    public void Log(string message)
    {
        Debug.Log($"{SceneManager.GetActiveScene().name}: {message}");
    }
}
