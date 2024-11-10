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

        StartCoroutine(_curState.InitCo());
        yield return new WaitUntil(() => _curState.CurProcess == GMStateProcess.Scene);

        StartCoroutine(_curState.SceneCo());
        yield return new WaitUntil(() => _curState.CurProcess == GMStateProcess.UI);

        StartCoroutine(_curState.UICo());
        yield return new WaitUntil(() => _curState.CurProcess == GMStateProcess.Player);

        StartCoroutine(_curState.PlayerCo());
        yield return new WaitUntil(() => _curState.CurProcess == GMStateProcess.End);
    }
}
