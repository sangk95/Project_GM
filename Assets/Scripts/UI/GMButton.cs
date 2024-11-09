using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using TMPro;
using UnityEngine.UI;

namespace GM.UI
{
    public class GMButton : GMUIBase, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        private Dictionary<GMUIState, int> m_dicHashCode = new Dictionary<GMUIState, int>();
        private Dictionary<GMUIState, System.Object> m_dicObject = new Dictionary<GMUIState, object>();
        private GMUIState m_curState;
        private bool m_isDisabled;
        private bool m_isSelected;
        private static DateTime m_lastClick = DateTime.MinValue;
        private const float CLICK_DELAY = 200;

        private Dictionary<GMUIState, GameObject> m_dicStateObject = new Dictionary<GMUIState, GameObject>();

        public Action<GMUIState, int, object> CallBack;
        public List<TextMeshProUGUI> m_listText = new List<TextMeshProUGUI>();
        public GameObject m_objNormal;
        public GameObject m_objHover;
        public GameObject m_objClick;
        public GameObject m_objDisable;
        public GameObject m_objSelect;

        public override void UIPreInit()
        {
            m_lastClick = DateTime.Now;
            m_dicHashCode = new Dictionary<GMUIState, int>();
            m_dicObject = new Dictionary<GMUIState, object>();
            m_dicStateObject = new Dictionary<GMUIState, GameObject>();
            m_curState = GMUIState.None;
            m_isDisabled = false;
            m_isSelected = false;

            if (m_objNormal)
            {
                m_objNormal.SetActive(true);
                m_dicStateObject.Add(GMUIState.Normal, m_objNormal);
            }
            if (m_objHover)
            {
                m_objHover.SetActive(false);
                m_dicStateObject.Add(GMUIState.Hover, m_objHover);
            }
            if (m_objClick)
            {
                m_objClick.SetActive(false);
                m_dicStateObject.Add(GMUIState.Click, m_objClick);
            }
            if (m_objDisable)
            {
                m_objDisable.SetActive(false);
                m_dicStateObject.Add(GMUIState.Disable, m_objDisable);
            }
            if (m_objSelect)
            {
                m_objSelect.SetActive(false);
            }
            if (m_listText != null)
            {
                for (int i = 0; i < m_listText.Count; ++i)
                    m_listText[i].gameObject.SetActive(false);
            }
        }

        protected override void UIEnd()
        {
            CallBack = null;
        }

        protected override void OnEnable()
        {
            SetState(GMUIState.Normal);
        }


        public void OnPointerExit(PointerEventData eventData)
        {
            if (m_isDisabled == true || m_isSelected == true)
                return;

            SetState(GMUIState.Normal);
            CheckSendCallBack();
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (m_isDisabled == true)
                return;
            if (m_curState == GMUIState.Click)
                return;

            SetState(GMUIState.Hover);
            CheckSendCallBack();
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            if (m_isDisabled == true || m_isSelected == true)
                return;
            if (m_curState == GMUIState.Click)
                return;

            SetState(GMUIState.Click);
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (m_isDisabled == true || m_isSelected == true)
                return;

            CheckSendCallBack();


            SetState(GMUIState.Hover);
        }
        private void CheckSendCallBack()
        {
            if (m_dicHashCode.TryGetValue(m_curState, out int hashCode) == true)
            {
                if ((DateTime.Now - m_lastClick).Milliseconds > CLICK_DELAY)
                {
                    if (m_dicObject.TryGetValue(m_curState, out object _obj) == true)
                        CallBack?.Invoke(m_curState, hashCode, _obj);
                    else
                        CallBack?.Invoke(m_curState, hashCode, null);
                    m_lastClick = DateTime.Now;
                }
                else
                {
                    Debug.LogWarning("Click delayed" + (DateTime.Now - m_lastClick).Milliseconds);
                }
            }
        }

        public void AddUICallBack(GMUIState state, int hashCode, object obj = null)
        {
            if (m_dicHashCode.ContainsKey(state) == true)
                m_dicHashCode[state] = hashCode;
            else
                m_dicHashCode.Add(state, hashCode);

            if (m_dicObject.ContainsKey(state) == true)
                m_dicObject[state] = obj;
            else
                m_dicObject.Add(state, obj);
        }
        public void SetDisable(bool flag)
        {
            m_isDisabled = flag;
        }

        private void SetState(GMUIState state)
        {
            foreach (var obj in m_dicStateObject)
            {
                if (obj.Key == GMUIState.None || obj.Value == null)
                    continue;

                if (obj.Key == state)
                    obj.Value.SetActive(true);
                else
                    obj.Value.SetActive(false);
            }

            m_curState = state;
        }

        public void SetText(string text)
        {
            if (m_listText != null && m_listText.Count > 0)
            {
                for (int i = 0; i < m_listText.Count; ++i)
                {
                    m_listText[i].gameObject.SetActive(true);
                    m_listText[i].SetText(text);
                }
            }
        }
    }
}