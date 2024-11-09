using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppMain : MonoBehaviour
{
    List<GMManagerBase> _listManager = new List<GMManagerBase>();
    GMInputManager _inputManager;

    private void Awake()
    {
        _inputManager = FindObjectOfType<GMInputManager>();
        _listManager.Add(_inputManager);
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
