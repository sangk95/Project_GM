using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GM.UI
{
    public class GMUIPool : MonoBehaviour
    {
        Dictionary<GMUIAddress, GameObject> m_dicUIObjectPool = new Dictionary<GMUIAddress, GameObject>();

        public Action<GMUIAddress, GameObject, Action<GMUIController>> LoadComplete;

        public void GetUI(GMUIAddress address, Action<GMUIController> callBack)
        {
            if(m_dicUIObjectPool.TryGetValue(address, out GameObject uiObject) == false)
            {
                Addressables.LoadAssetAsync<GameObject>(address.ToString()).Completed += (handle) =>
                {
                    if (handle.Result == null)
                        return;

                    GameObject uiObj = Instantiate(handle.Result);
                    LoadComplete?.Invoke(address, uiObj, callBack);
                };
            }
            else
            {
                LoadComplete?.Invoke(address, uiObject, callBack);
             
                m_dicUIObjectPool.Remove(address);
            }
        }

        public void ReturnUI(GMUIAddress address, GameObject obj)
        {
            if(m_dicUIObjectPool.ContainsKey(address) == true)
            {
                m_dicUIObjectPool[address] = obj;
            }
            else
            {
                m_dicUIObjectPool.Add(address, obj);
            }
            obj.SetActive(false);
        }

        public void DestroyUI(GMUIAddress address)
        {
            if (m_dicUIObjectPool.TryGetValue(address, out GameObject obj) == false)
                return;
            else
            {
                DestroyImmediate(obj);
                m_dicUIObjectPool.Remove(address);
            }
        }
    }
}