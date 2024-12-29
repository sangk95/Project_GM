using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable InconsistentNaming

public enum GMUIState
{
    None,
    Normal,
    Hover,
    Click,
    Disable
}

public enum GMUIAddress
{
    None,
    GMPage_LobbyMain,
    GMPage_InGame,
}

public enum GMStateProcess
{
    Init,
    Scene,
    UI,
    Player,
    ETC,
    End,
}
public enum GMGameState
{
    None,
    State_Lobby,
    State_Main,
}
public enum GMScene
{
    None,
    StandBy,
    Lobby,
    Stage01,
}
public enum GMPlayerState
{
    None,
    Default,
}
public enum GMPlayerAnim
{
    None,
    Player_Idle,
    Player_Run,
    Player_Dash,
}
