using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM.State
{
    public class GMStateBase
    {
        private GMStateProcess _curProcess = GMStateProcess.Init;
        public GMStateProcess CurProcess => _curProcess;

        public virtual void InitStep()
        {
            GMSceneManager.Instance.OnLoadCompleteScene += OnLoadCompleteScene;
            _curProcess = GMStateProcess.Scene;
        }
        public virtual void SceneStep()
        {
            _curProcess = GMStateProcess.UI;
        }
        public virtual void UIStep()
        {
            _curProcess = GMStateProcess.Player;
        }
        public virtual void PlayerStep()
        {
            _curProcess = GMStateProcess.End;
        }
        public virtual void OnLoadCompleteScene()
        {
            GMSceneManager.Instance.OnLoadCompleteScene -= OnLoadCompleteScene;
        }
    }
}