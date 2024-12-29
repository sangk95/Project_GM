using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM.UI
{
    public class GMPageLobbyMain : GMUIController
    {
        private static string _strClick_StartGame = "Click StartGame";
        private static string _strClick_Option = "Click Option";

        [Header("Buttons")]
        public GMButton _btnStartGame;
        public GMButton _btnOption;

        public override void UIInit()
        {
            base.UIInit();
            if(_btnStartGame != null)
            {
                _btnStartGame.SetText("GameStart");
                _btnStartGame.AddUICallBack(GMUIState.Click, _strClick_StartGame.GetHashCode());
                _btnStartGame.CallBack += UICallBack;
            }
            if(_btnOption != null)
            {
                _btnOption.SetText("Option");
                _btnOption.AddUICallBack(GMUIState.Click, _strClick_Option.GetHashCode());
                _btnOption.CallBack += UICallBack;
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
                            GMStateManager.Instance.ChangeState(GMGameState.State_Main);
                            GMUIManager.Instance.CloseUIController(this);
                        }
                        else if(hashCode == _strClick_Option.GetHashCode())
                        {
                            // Load Popup Option UI
                        }
                    }
                    break;
            }
        }
    }
}