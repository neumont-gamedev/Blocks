using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : FiniteStateMachine
{
    [SerializeField] [Range(0.1f, 5.0f)] float m_enterTime;

    public enum eState
    {
        INACTIVE,
        ENTER,
        ACTIVE,
        HIT
    }

    public enum eType
    {
        STANDARD
    }

    eType m_type;
    float m_timer;
    Vector3 m_positionStart;
    Vector3 m_position;

    void Awake()
    {
        InitializeStateMachine<eState>(eState.INACTIVE, true);
        AddTransitionsToState(eState.INACTIVE, new System.Enum[] { eState.ACTIVE, eState.ENTER });
        AddTransitionsToState(eState.ENTER, new System.Enum[] { eState.ACTIVE });
        AddTransitionsToState(eState.ACTIVE, new System.Enum[] { eState.HIT, eState.ENTER });
        AddTransitionsToState(eState.HIT, new System.Enum[] { eState.INACTIVE });
    }

    void Start()
    {
		
	}

    public void Create(Vector3 position, eType type)
    {
        m_type = type;

        m_position = position;
        m_positionStart.x = m_position.x;
        m_positionStart.y = m_position.y + 10.0f;
        m_positionStart.z = m_position.z;
        
        transform.position = m_positionStart;

        SetState(eState.ENTER);
    }

    private void EnterENTER(Enum previousState)
    {
        transform.position = m_positionStart;
        m_timer = m_enterTime;
    }

    private void UpdateENTER()
    {
        m_timer = m_timer - Time.deltaTime;
        m_timer = Mathf.Max(m_timer, 0.0f);
        Vector3 position = Vector3.Lerp(m_positionStart, m_position, 1.0f - (m_timer / m_enterTime));
        transform.position = position;

        if (m_timer == 0.0f)
        {
            SetState(eState.ACTIVE);
        }
    }

}
