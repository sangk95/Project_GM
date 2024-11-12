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
    public void UnLoadSceneAsync(GMScene sceneType)
    {
        if(_dicSceneInstance.ContainsKey(sceneType) == false)
        {
            Debug.LogError($"UnLoadSceneAsync - {sceneType} doesn't exist");
            return;
        }
        Addressables.UnloadSceneAsync(_dicSceneInstance[sceneType]);
    }
    private void LoadCompleteScene(AsyncOperationHandle<SceneInstance> handle)
    {
        if (handle.Status != AsyncOperationStatus.Succeeded)
            return;

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
}
