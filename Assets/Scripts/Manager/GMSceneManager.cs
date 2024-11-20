using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class GMSceneManager : GMManagerBase<GMSceneManager>
{
    private Dictionary<GMScene, SceneInstance> _dicSceneInstance;
    private Queue<GMScene> _queueLateChangeScene;
    private GMScene _curLoadScene;
    private bool _isLoadProcess;

    public Action OnLoadCompleteScene;
    public Action OnUnLoadCompleteScene;

    public override IEnumerator GMAwake()
    {
        _dicSceneInstance = new Dictionary<GMScene, SceneInstance>();
        _queueLateChangeScene = new Queue<GMScene>();
        _curLoadScene = GMScene.None;
        _isLoadProcess = false;
        return base.GMAwake();
    }
    public void LoadSceneAsync(GMScene sceneType)
    {
        if(_isLoadProcess == true)
        {
            _queueLateChangeScene.Enqueue(sceneType);
            return;
        }
        if (_dicSceneInstance.ContainsKey(_curLoadScene) == true)
            return;

        _curLoadScene = sceneType;
        _isLoadProcess = true;

        string sceneName = sceneType.ToString();
        Addressables.LoadSceneAsync(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive).Completed += LoadCompleteScene;
    }
    public void LoadSceneAsync_Single(GMScene sceneType)
    {
        if(_isLoadProcess == true)
        {
            _queueLateChangeScene.Enqueue(sceneType);
            return;
        }
        if (_dicSceneInstance.ContainsKey(_curLoadScene) == true)
            return;

        _curLoadScene = sceneType;
        _isLoadProcess = true;

        string sceneName = sceneType.ToString();
        Addressables.LoadSceneAsync(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Single).Completed += LoadCompleteScene_Single;
    }
    public void UnLoadSceneAsync(GMScene sceneType)
    {
        if(_dicSceneInstance.ContainsKey(sceneType) == false)
        {
            Debug.LogError($"UnLoadSceneAsync - {sceneType} doesn't exist");
            return;
        }
        Addressables.UnloadSceneAsync(_dicSceneInstance[sceneType]).Completed += UnLoadCompleteScene;
    }
    private void LoadCompleteScene(AsyncOperationHandle<SceneInstance> handle)
    {
        if (handle.Status != AsyncOperationStatus.Succeeded ||
            handle.Result.Scene.IsValid() == false)
            return;
        GameObject[] rootObjects = handle.Result.Scene.GetRootGameObjects();
        if(rootObjects != null)
        {
            for(int i=0; i< rootObjects.Length; ++i)
            {
                Camera camera = rootObjects[i].GetComponent<Camera>();
                if (camera != null)
                {
                    camera.enabled = false;
                    rootObjects[i].SetActive(false);
                }
            }
        }

        _dicSceneInstance.Add(_curLoadScene, handle.Result);
        
        _curLoadScene = GMScene.None;
        _isLoadProcess = false;

        if (_queueLateChangeScene.Count > 0)
        {
            LoadSceneAsync(_queueLateChangeScene.Dequeue());
        }
        else
        {
            if (OnLoadCompleteScene != null)
                OnLoadCompleteScene.Invoke();
        }
    }
    private void LoadCompleteScene_Single(AsyncOperationHandle<SceneInstance> handle)
    {
        if (handle.Status != AsyncOperationStatus.Succeeded ||
            handle.Result.Scene.IsValid() == false)
            return;
        GameObject[] rootObjects = handle.Result.Scene.GetRootGameObjects();
        if (rootObjects != null)
        {
            for (int i = 0; i < rootObjects.Length; ++i)
            {
                Camera camera = rootObjects[i].GetComponent<Camera>();
                if (camera != null)
                {
                    camera.enabled = false;
                    rootObjects[i].SetActive(false);
                }
            }
        }

        _dicSceneInstance.Clear();

        _dicSceneInstance.Add(_curLoadScene, handle.Result);
        
        _curLoadScene = GMScene.None;
        _isLoadProcess = false;

        if (_queueLateChangeScene.Count > 0)
        {
            LoadSceneAsync(_queueLateChangeScene.Dequeue());
        }
        else
        {
            if (OnLoadCompleteScene != null)
                OnLoadCompleteScene.Invoke();
        }
    }
    private void UnLoadCompleteScene(AsyncOperationHandle<SceneInstance> handle)
    {
        if (handle.Status != AsyncOperationStatus.Succeeded)
            return;

        if (_dicSceneInstance.ContainsValue(handle.Result) == false)
            return;
        foreach(var data in _dicSceneInstance)
        {
            if (data.Value.Scene != handle.Result.Scene)
                continue;
            _dicSceneInstance.Remove(data.Key);
            break;
        }
        if (OnUnLoadCompleteScene != null)
            OnUnLoadCompleteScene.Invoke();
    }
}
