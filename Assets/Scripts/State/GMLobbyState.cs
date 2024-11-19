using GM.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM.State
{
    public class GMLobbyState : GMStateBase
    {
        public override void InitStep()
        {
            base.InitStep();
        }
        public override void SceneStep()
        {
            GMSceneManager.Instance.LoadSceneAsync_Single(GMScene.Lobby);
        }
        public override void OnLoadCompleteScene()
        {
            base.OnLoadCompleteScene();
            base.SceneStep();
        }
        public override void UIStep()
        {
            // 로비 UI 로드
            GMUIManager.Instance.LoadUIController(GMUIAddress.GMPage_LobbyMain, AsyncLoadUI);
        }
        public void AsyncLoadUI(GMUIController uiController)
        {
            if (uiController == null || (uiController is GMPageLobbyMain) == false)
                return;
            base.UIStep();
        }
        public override void PlayerStep()
        {
            base.PlayerStep();
        }
    }
}