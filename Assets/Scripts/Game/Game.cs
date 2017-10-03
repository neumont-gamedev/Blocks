using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : FiniteStateMachine
{
    [SerializeField] Blocks m_blocks = null;
    [SerializeField] Balls m_balls = null;
    [SerializeField] Score m_score = null;
    [SerializeField] PointsController m_points = null;
    [SerializeField] Transform m_ballStart = null;

    float m_timer = 0.0f;
    float m_time = 0.0f;

    public enum eState
    {
        INITIALIZE,
        TITLE,
        START,
        GAME,
        GAMEOVER
    }

    private static Game s_instance = null;
    public static Game instance { get { return s_instance; } }

    void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;
        }
        else if (s_instance != this)
        {
            Destroy(gameObject);
        }

        InitializeStateMachine<eState>(eState.INITIALIZE, true);
        AddTransitionsToState(eState.INITIALIZE, new System.Enum[] { eState.TITLE });
        AddTransitionsToState(eState.TITLE, new System.Enum[] { eState.START });
        AddTransitionsToState(eState.START, new System.Enum[] { eState.GAME });
        AddTransitionsToState(eState.GAME, new System.Enum[] { eState.GAMEOVER });
        AddTransitionsToState(eState.GAMEOVER, new System.Enum[] { eState.TITLE });
    }

    void EnterINITIALIZE(System.Enum previous)
    {
        SetState(eState.START, true);
    }

    void EnterSTART(System.Enum previous)
    {
        m_blocks.CreateBlocks("");
        m_timer = 2.0f;
    }

    void UpdateSTART()
    {
        m_timer = m_timer - Time.deltaTime;
        if (m_timer <= 0.0f)
        {
            float angle = Random.Range(-45.0f, 45.0f);
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector3 direction = rotation * Vector3.down;

            m_balls.CreateBall(m_ballStart.position, direction);
            SetState(eState.GAME, true);
        }
    }

    public void AddPoints(Vector3 position, int value)
    {
        m_points.CreatePoints(position, value);
        m_score.AddPoints(value);
    }
}
