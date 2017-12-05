using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.SceneManagement;
using System;

public class LoadScene : MonoBehaviour
{
    public delegate void LoadSceneHandler(string sceneName);
    public event LoadSceneHandler onStartLoadScene;
    public event LoadSceneHandler onUnloadScene;

    float durationTime = 3f;

    private void Start()
    {
        SteamVR_Fade.Start(Color.black, 0);
        SteamVR_Fade.Start(Color.clear, durationTime);
    }

    public void FadeLoadScene(string sceneName)
    {
        SteamVR_Fade.Start(Color.black, durationTime);
        StartCoroutine(StartLoad(sceneName, LoadSceneMode.Single, durationTime));
    }

    public void AdditiveLoadScene(string sceneName)
    {
        SteamVR_Fade.Start(Color.black, durationTime);
        StartCoroutine(StartLoad(sceneName, LoadSceneMode.Additive, durationTime));
    }

    public void UnloadScene(string sceneName)
    {
        StartCoroutine(Unload(sceneName));
        if (onUnloadScene != null)
        {
            onUnloadScene(sceneName);
        }
    }

    IEnumerator StartLoad(string sceneName,LoadSceneMode mode,float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(Load(sceneName, mode));
        if (onStartLoadScene != null)
        {
            onStartLoadScene(sceneName);
        }
    }

    IEnumerator Load(string sceneName,LoadSceneMode loadMode)
    {
        yield return SceneManager.LoadSceneAsync(sceneName,loadMode);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }

    IEnumerator Unload(string sceneName)
    {
        yield return SceneManager.UnloadSceneAsync(sceneName);
        GC.Collect();
    }
}
