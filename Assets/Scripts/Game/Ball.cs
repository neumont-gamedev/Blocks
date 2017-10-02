using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : FiniteStateMachine
{
    [SerializeField][Range(0.0f, 10.0f)] float m_speed = 5.0f;

    public enum eState
    {
        INACTIVE,
        ENTER,
        ACTIVE,
        HIT
    }

    Rigidbody m_rigidbody = null;
    Ray m_ray = new Ray(Vector3.zero, Vector3.zero);

    public Vector3 direction { get; set; }
    
    void Awake()
    {
        InitializeStateMachine<eState>(eState.INACTIVE, true);
        AddTransitionsToState(eState.INACTIVE, new System.Enum[] { eState.ACTIVE, eState.ENTER });
        AddTransitionsToState(eState.ENTER, new System.Enum[] { eState.ACTIVE });
        AddTransitionsToState(eState.ACTIVE, new System.Enum[] { eState.HIT, eState.ENTER });
        AddTransitionsToState(eState.HIT, new System.Enum[] { eState.INACTIVE });

        direction = new Vector3(0.5f, -0.5f, 0.0f);
        direction.Normalize();
    }

	void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.AddForce(direction * (m_speed * 100.0f));
    }

    public void Create(Vector3 position, Vector3 direction)
    {
        transform.position = position;
        this.direction = direction;
        
        SetState(eState.ENTER);
    }

    private void UpdateINACTIVE()
    {
        SetState(eState.ACTIVE);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            print("ball hit");
        }
    }
}
