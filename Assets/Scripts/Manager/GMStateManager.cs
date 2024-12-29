using GM.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMStateManager : GMManagerBase<GMStateManager>
{
    GMStateBase _curState;

    // ReSharper disable Unity.PerformanceAnalysis
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
            case GMGameState.State_Lobby:
                {
                    _curState = new GMLobbyState();
                }
                break;
            case GMGameState.State_Main:
                {
                    _curState = new GMMainState();
                }
                break;

            default:
                Debug.LogError("Wrong GameState - " + gameState);
                return;
        }
        StartCoroutine(ChangeStateCo());
    }
    private IEnumerator ChangeStateCo()
    {
        if (_curState == null)
            yield break;

        _curState.Init();
        yield return new WaitUntil(() => _curState.CurProcess == GMStateProcess.Scene);

        _curState.LoadScene();
        yield return new WaitUntil(() => _curState.CurProcess == GMStateProcess.UI);

        _curState.LoadUI();
        yield return new WaitUntil(() => _curState.CurProcess == GMStateProcess.Player);

        _curState.LoadCharacter();
        yield return new WaitUntil(() => _curState.CurProcess == GMStateProcess.ETC);
        
        _curState.LoadETC();
        yield return new WaitUntil(() => _curState.CurProcess == GMStateProcess.End);
    }
}
