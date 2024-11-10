using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM.State
{
    public class GMStateBase : MonoBehaviour
    {
        private GMStateProcess _curProcess = GMStateProcess.Init;
        public GMStateProcess CurProcess => _curProcess;

        public virtual IEnumerator InitCo()
        {
            yield return null;
            _curProcess = GMStateProcess.Scene;
        }
        public virtual IEnumerator SceneCo()
        {
            yield return null;
            _curProcess = GMStateProcess.UI;
        }
        public virtual IEnumerator UICo()
        {
            yield return null;
            _curProcess = GMStateProcess.Player;
        }
        public virtual IEnumerator PlayerCo()
        {
            yield return null;
            _curProcess = GMStateProcess.End;
        }
    }
}