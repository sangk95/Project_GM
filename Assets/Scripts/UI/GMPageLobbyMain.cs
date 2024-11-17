using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM.UI
{
    public class GMPageLobbyMain : GMUIController
    {
        private static string _strClick_StartGame = "Click StartGame";

        public GMButton _btnStartGame;

        public override void UIInit()
        {
            base.UIInit();
            if(_btnStartGame != null)
            {
                _btnStartGame.SetText("GameStart");
                _btnStartGame.AddUICallBack(GMUIState.Click, _strClick_StartGame.GetHashCode());
                _btnStartGame.CallBack += UICallBack;
            }
        }

        protected override void UICallBack(GMUIState state, int hashCode, object obj = null)
        {
            base.UICallBack(state, hashCode, obj);

            switch(state)
            {
                case GMUIState.Click:
                    {
                        if (hashCode == _strClick_StartGame.GetHashCode())
                        {
                            GMStateManager.Instance.ChangeState(GMGameState.Stage01);
                            GMUIManager.Instance.CloseUIController(this);
                        }
                    }
                    break;
            }
        }
    }
}