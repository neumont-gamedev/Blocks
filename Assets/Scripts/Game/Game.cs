using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : FiniteStateMachine
{
    [SerializeField] Blocks m_blocks = null;
    [SerializeField] Ball m_ball = null;

    public enum eState
    {
        INITIALIZE,
        TITLE,
        START,
        GAME,
        GAMEOVER
    }

    void Awake()
    {
        InitializeStateMachine<eState>(eState.INITIALIZE, true);
        AddTransitionsToState(eState.INITIALIZE, new System.Enum[] { eState.TITLE });
        AddTransitionsToState(eState.TITLE, new System.Enum[] { eState.START });
        AddTransitionsToState(eState.START, new System.Enum[] { eState.GAME });
        AddTransitionsToState(eState.GAME, new System.Enum[] { eState.GAMEOVER });
        AddTransitionsToState(eState.GAMEOVER, new System.Enum[] { eState.TITLE });
    }

    void EnterINITIALIZE(Enum previous)
    {
        SetState(eState.START, true);
	}

    void EnterSTART(Enum previous)
    {
        m_blocks.CreateBlocks("");
    }

    void UpdateSTART()
    {
        
    }
}
