using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : FiniteStateMachine
{
    [SerializeField] [Range(0.1f, 5.0f)] float m_enterTime = 1.0f;
    [SerializeField] [Range(0.1f, 1.0f)] float m_hitTime = 0.2f;

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

    Collider m_collider = null;
    Blocks m_owner = null;

    eType m_type;
    float m_timer;
    Vector3 m_positionStart;
    Vector3 m_position;
    Vector3 m_scaleStart;

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
        m_collider = GetComponent<Collider>();
    }

    public void Create(Vector3 position, eType type, Blocks blocks)
    {
        m_type = type;

        m_position = position;
        m_positionStart.x = m_position.x;
        m_positionStart.y = m_position.y + 10.0f;
        m_positionStart.z = m_position.z;
        transform.position = m_positionStart;

        m_scaleStart = transform.localScale;

        m_owner = blocks;

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
        //float interp = Interpolation.BounceOut(1.0f - (m_timer / m_enterTime));
        float interp = 1.0f - (m_timer / m_enterTime);
        transform.position = Vector3.LerpUnclamped(m_positionStart, m_position, interp);
        
        if (m_timer == 0.0f)
        {
            SetState(eState.ACTIVE);
        }
    }

    private void EnterHIT(Enum previousState)
    {
        m_collider.enabled = false;
        m_owner.RemoveBlock(this);

        m_timer = m_hitTime;
        StartCoroutine(HitCoroutine());
    }

    private void UpdateHIT()
    {
        //
    }

    IEnumerator HitCoroutine()
    {
        GetComponent<AudioSource>().Play();
        while (m_timer > 0.0f)
        {
            m_timer = m_timer - Time.deltaTime;
            m_timer = Mathf.Max(m_timer, 0.0f);
            //float interp = Interpolation.BackIn(1.0f - (m_timer / m_hitTime));
            float interp = 1.0f - (m_timer / m_hitTime);
            transform.localScale = Vector3.LerpUnclamped(m_scaleStart, Vector3.zero, interp);
                        
            yield return null;
        }
        
        SetState(eState.INACTIVE);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            SetState(eState.HIT);
        }
    }
}
