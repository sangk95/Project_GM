using GM.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM.State
{
    public class GMStateStage01 : GMStateBase
    {
        public override void InitStep()
        {
            base.InitStep();
        }
        public override void SceneStep()
        {
            GMSceneManager.Instance.LoadSceneAsync_Single(GMScene.Stage01);
        }
        public override void OnLoadCompleteScene()
        {
            base.OnLoadCompleteScene();
            base.SceneStep();
        }
        public override void UIStep()
        {
            // UI ·Îµå
        }
        public override void PlayerStep()
        {
            base.PlayerStep();
        }
    }
}