using GM.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM.State
{
    public class GMLobbyState : GMStateBase
    {
        public override void Init()
        {
            base.Init();
        }
        public override void LoadScene()
        {
            GMSceneManager.Instance.LoadSceneAsync_Single(GMScene.Lobby);
        }

        protected override void OnLoadCompleteScene()
        {
            base.OnLoadCompleteScene();
            base.LoadScene();
        }
        public override void LoadUI()
        {
            GMUIManager.Instance.LoadUIController(GMUIAddress.GMPage_LobbyMain, AsyncLoadUI);
        }
        public void AsyncLoadUI(GMUIController uiController)
        {
            if (uiController == null || (uiController is GMPageLobbyMain) == false)
                return;
            base.LoadUI();
        }
        public override void LoadCharacter()
        {
            base.LoadCharacter();
        }
    }
}