using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM.UI
{
    public class GMPageInGame : GMUIController
    {
        private static string _strClick_WantedPoster = "Click WantedPoster";
        private static string _strOpen_WantedPoster = "Open WantedPoster";
        private static string _strClose_WantedPoster = "Close WantedPoster";

        [Header("Buttons")]
        public GMButton _btnWantedPoster;

        [Header("DoTween")]
        public DOTweenAnimation _tweenWantedPoster;

        private bool _isWantedPosterOpen;

        public override void UIInit()
        {
            base.UIInit();
            if(_btnWantedPoster != null)
            {
                _btnWantedPoster.AddUICallBack(GMUIState.Click, _strClick_WantedPoster.GetHashCode());
                _btnWantedPoster.CallBack += UICallBack;
            }


        }

        protected override void UICallBack(GMUIState state, int hashCode, object obj = null)
        {
            base.UICallBack(state, hashCode, obj);

            switch(state)
            {
                case GMUIState.Click:
                    {
                        if (hashCode == _strClick_WantedPoster.GetHashCode())
                        {
                            if(_tweenWantedPoster != null)
                            {
                                if(_isWantedPosterOpen)
                                    _tweenWantedPoster.DORestartAllById(_strClose_WantedPoster);
                                else
                                    _tweenWantedPoster.DORestartAllById(_strOpen_WantedPoster);
                            }
                        }
                    }
                    break;
            }
        }
    }
}