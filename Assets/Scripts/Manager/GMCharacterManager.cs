using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GMCharacterManager : GMManagerBase<GMCharacterManager>
{
    public Action OnCompleteCreateCharacter;

    private GMPlayerScript _myScript;
    private Dictionary<GMPlayerAnim, int> _dicAnimHash;
    public override IEnumerator GMAwake()
    {
        _dicAnimHash = new Dictionary<GMPlayerAnim, int>();
        
        CachingAnimHash();
        
        return base.GMAwake();
    }

    private void CachingAnimHash()
    {
        foreach (GMPlayerAnim anim in Enum.GetValues(typeof(GMPlayerAnim)))
        {
            _dicAnimHash.Add(anim, Animator.StringToHash(anim.ToString()));
        }
    }

    public int GetAnimHash(GMPlayerAnim anim)
    {
        if(_dicAnimHash != null && _dicAnimHash.TryGetValue(anim, out int hash))
            return hash;
        return 0;
    }
    public void CreateMyCharacter()
    {
        Addressables.LoadAssetAsync<GameObject>("GMPlayer").Completed += (handle) =>
        {
            if (handle.Result != null && handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                GameObject resultObj = Instantiate(handle.Result);
                _myScript = resultObj.GetComponent<GMPlayerScript>();
                if(_myScript != null)
                {
                    _myScript.SetCharacter();
                }

                resultObj.SetActive(true);
                
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
