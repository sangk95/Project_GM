using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GM.UI
{
    public class GMUIBase : UIBehaviour
    {
        protected bool m_isInit;
        public bool IsInit() => m_isInit;

        protected override void Awake()
        {
            base.Awake();
            m_isInit = false;

            UIPreInit();
        }
        protected override void OnDestroy() 
        {
            UIEnd();
        }
        public virtual void UIPreInit()
        {

        }
        /// <summary>
        /// For binding (setText, setActive, etc)
        /// </summary>
        public virtual void UIInit()
        {
            m_isInit = true;
        }
        /// <summary>
        /// For default setting
        /// </summary>
        public virtual void UIEnter()
        {

        }
        /// <summary>
        /// For unbinding
        /// </summary>
        public virtual void UIExit()
        {

        }
        /// <summary>
        /// For destroy
        /// </summary>
        protected virtual void UIEnd()
        {

        }

        protected virtual void UICallBack(GMUIState state, int hashCode, object obj = null)
        {
        }
    }
}