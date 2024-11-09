using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GM.UI
{
    public class GMUIController : GMUIBase
    {
        [NonSerialized]
        public GMUIAddress m_address;
        protected bool m_canCallBack;
        public override void UIPreInit()
        {
            base.UIPreInit();
            m_address = GMUIAddress.None;
        }
        public override void UIInit()
        {
            base.UIInit();
        }
        public override void UIEnter()
        {
            base.UIEnter();

            m_canCallBack = true;
        }
        public override void UIExit()
        {
            base.UIExit();
        }
        protected override void UIEnd()
        {
            base.UIEnd();
        }

        protected override void UICallBack(GMUIState state, int hashCode, object obj = null)
        {
            base.UICallBack(state, hashCode, obj);
            if (m_canCallBack == false)
                return;
        }

        public void SetControllerAddress(GMUIAddress address)
        {
            if (m_address != GMUIAddress.None)
                return;

            m_address = address;
        }
    }
}