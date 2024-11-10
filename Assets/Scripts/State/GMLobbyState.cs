using GM.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM.State
{
    public class GMLobbyState : GMStateBase
    {
        public override IEnumerator InitCo()
        {


            return base.InitCo();
        }
        public override IEnumerator SceneCo()
        {
            // 로비 씬 로드

            return base.SceneCo();
        }
        public override IEnumerator UICo()
        {
            // 로비 UI 로드
            GMUIManager.Instance.LoadUIController(GMUIAddress.GMPage_LobbyMain, AsyncLoadUI);

            yield return null;
        }
        private void AsyncLoadUI(GMUIController uiController)
        {
            if (uiController != null)
                base.UICo();
        }
        public override IEnumerator PlayerCo()
        {
            return base.PlayerCo();
        }
    }
}