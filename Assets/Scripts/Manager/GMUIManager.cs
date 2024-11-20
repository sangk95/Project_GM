using GM.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GMUIManager : GMManagerBase<GMUIManager>
{
    private const float WAITTIME = 3.0f;

    private WaitForSeconds m_waitTime;
    private Dictionary<GMUIAddress, GameObject> m_dicUIController;
    private Dictionary<GMUIAddress, DateTime> m_dicCloseUITime; // has delay

    public GameObject _rootCanvas;
    public GMUIPool _uiPool;

    public override IEnumerator GMAwake()
    {
        m_dicUIController = new Dictionary<GMUIAddress, GameObject>();
        m_dicCloseUITime = new Dictionary<GMUIAddress, DateTime>();

        m_waitTime = new WaitForSeconds(WAITTIME);

        if(_uiPool)
            _uiPool.LoadComplete += LoadCompleteController;

        StartCoroutine(CheckCloseUI());

        return base.GMAwake();
    }
    private IEnumerator CheckCloseUI()
    {
        while(true)
        {
            if(m_dicCloseUITime.Count > 0)
            {
                foreach(var data in m_dicCloseUITime)
                {
                    if((DateTime.Now - data.Value).Milliseconds > WAITTIME)
                    {
                        _uiPool.DestroyUI(data.Key);
                        m_dicCloseUITime.Remove(data.Key);

                        break;
                    }
                }
            }

            yield return m_waitTime;
        }
    }
    public void LoadUIController(GMUIAddress address, Action<GMUIController> callBack = null)
    {
        if (m_dicUIController.ContainsKey(address) == true)
        {
            Debug.LogWarning("Already Loaded UI Address ==== " + address);
            return;
        }
        _uiPool.GetUI(address, callBack);
    }

    private void LoadCompleteController(GMUIAddress address, GameObject obj, Action<GMUIController> callBack)
    {
        if (obj == null)
            return;
        obj.transform.SetParent(_rootCanvas.transform);

        if(m_dicCloseUITime.ContainsKey(address) == true)
        {
            m_dicCloseUITime.Remove(address);
        }

        GMUIController uiController = obj.GetComponent<GMUIController>();
        if (uiController == null)
            return;

        if (m_dicUIController.ContainsKey(address) == true)
            m_dicUIController[address] = obj;
        else
            m_dicUIController.Add(address, obj);

        RectTransform rect = obj.GetComponent<RectTransform>();
        Vector2 vector = Vector2.zero;
        rect.offsetMax = vector;
        rect.offsetMin = vector;
        obj.SetActive(true);

        if (uiController.IsInit() == false)
            uiController.UIInit();

        uiController.UIEnter();

        uiController.SetControllerAddress(address);

        callBack?.Invoke(uiController);
    }

    public void CloseUIController(GMUIController controller)
    {
        if (m_dicUIController.TryGetValue(controller.m_address, out GameObject obj) == true)
        {
            controller.UIExit();

            m_dicCloseUITime[controller.m_address] = DateTime.Now;

            _uiPool.ReturnUI(controller.m_address, obj);

            m_dicUIController.Remove(controller.m_address);
        }
        else
        {
            Debug.LogError("UIController is not using :" + controller.m_address);
            return;
        }
    }
    public void CloseUIController(GMUIAddress address)
    {
        if (m_dicUIController.TryGetValue(address, out GameObject obj) == true)
        {
            obj.GetComponent<GMUIController>().UIExit();

            m_dicCloseUITime[address] = DateTime.Now;

            _uiPool.ReturnUI(address, obj);

            m_dicUIController.Remove(address);
        }
        else
        {
            Debug.LogError("UIController is not using :" + address);
            return;
        }
    }
    public GMUIController GetUIController(GMUIAddress address)
    {
        if (m_dicUIController.TryGetValue(address, out GameObject obj) == true)
        {
            GMUIController uiController = obj.GetComponent<GMUIController>();
            if (uiController != null)
                return uiController;
        }

        return null;
    }
}