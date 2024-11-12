using GM.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMStateManager : GMManagerBase<GMStateManager>
{
    GMStateBase _curState;

    public override IEnumerator GMAwake()
    {
        _curState = new GMLobbyState();

        StartCoroutine(ChangeStateCo());

        return base.GMAwake();
    }
    public void ChangeState(GMGameState gameState)
    {
        switch(gameState)
        {
            case GMGameState.Lobby:
                {
                    _curState = new GMLobbyState();
                    ChangeStateCo();
                }
                break;
        }
    }
    private IEnumerator ChangeStateCo()
    {
        if (_curState == null)
            yield break;

        _curState.InitStep();
        yield return new WaitUntil(() => _curState.CurProcess == GMStateProcess.Scene);

        _curState.SceneStep();
        yield return new WaitUntil(() => _curState.CurProcess == GMStateProcess.UI);

        _curState.UIStep();
        yield return new WaitUntil(() => _curState.CurProcess == GMStateProcess.Player);

        _curState.PlayerStep();
        yield return new WaitUntil(() => _curState.CurProcess == GMStateProcess.End);
    }
}
