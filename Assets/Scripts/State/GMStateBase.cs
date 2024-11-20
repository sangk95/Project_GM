using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM.State
{
    public class GMStateBase
    {
        private GMStateProcess _curProcess = GMStateProcess.Init;
        public GMStateProcess CurProcess => _curProcess;

        public virtual void Init()
        {
            GMSceneManager.Instance.OnLoadCompleteScene += OnLoadCompleteScene;
            _curProcess = GMStateProcess.Scene;
        }
        public virtual void LoadScene()
        {
            _curProcess = GMStateProcess.UI;
        }
        public virtual void LoadUI()
        {
            _curProcess = GMStateProcess.Player;
        }
        public virtual void LoadCharacter()
        {
            _curProcess = GMStateProcess.ETC;
        }

        public virtual void LoadETC()
        {
            _curProcess = GMStateProcess.End;
        }

        protected virtual void OnLoadCompleteScene()
        {
            GMSceneManager.Instance.OnLoadCompleteScene -= OnLoadCompleteScene;
        }
    }
}