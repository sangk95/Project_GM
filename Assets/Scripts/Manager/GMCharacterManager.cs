using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GMCharacterManager : GMManagerBase<GMCharacterManager>
{
    public Action OnCompleteCreateCharacter;

    private GMPlayerScript _myScript;
    public void CreateMyCharacter()
    {
        Addressables.LoadAssetAsync<GameObject>("GMPlayer").Completed += (handle) =>
        {
            if (handle.Result != null && handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                GameObject gameObject = Instantiate(handle.Result);
                _myScript = gameObject.GetComponent<GMPlayerScript>();
                if(_myScript != null)
                {
                    _myScript.SetCharacter();
                }

                gameObject.SetActive(true);
                
                if (OnCompleteCreateCharacter != null)
                    OnCompleteCreateCharacter.Invoke();
            }
        };
    }
    public GMPlayerScript GetMyCharacter()
    {
        if (_myScript != null)
            return _myScript;
        return null;
    }
}
