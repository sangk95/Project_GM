using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppMain : MonoBehaviour
{
    List<GMManagerBase> _listManager = new List<GMManagerBase>();
    GMInputManager _inputManager;
    GMSceneManager _sceneManager;
    GMUIManager _uiManager;
    GMStateManager _stateManager;

    GMCharacterManager _characterManager;
    GMCameraManager _cameraManager;

    private void Awake()
    {
        _inputManager = GetComponent<GMInputManager>();
        _sceneManager = GetComponent<GMSceneManager>();
        _uiManager = GetComponent<GMUIManager>();
        _stateManager = GetComponent<GMStateManager>();
        _characterManager = GetComponent<GMCharacterManager>();
        _cameraManager = GetComponent<GMCameraManager>();

        _listManager.Add(_inputManager);
        _listManager.Add(_sceneManager);
        _listManager.Add(_uiManager);
        _listManager.Add(_stateManager);
        _listManager.Add(_characterManager);
        _listManager.Add(_cameraManager);
    }
    private void Start()
    {
        StartCoroutine(ManagerAwakeCo());
    }
    private IEnumerator ManagerAwakeCo()
    {
        for(int i=0; i<_listManager.Count; ++i)
        {
            if(_listManager[i] == null)
                continue;

            yield return StartCoroutine(_listManager[i].GMAwake());
        }

        StartCoroutine(ManagerStartCo());
    }
    private IEnumerator ManagerStartCo()
    {
        for(int i=0; i<_listManager.Count; ++i)
        {
            if(_listManager[i] == null)
                continue;

            yield return StartCoroutine(_listManager[i].GMStart());
        }

    }
}
