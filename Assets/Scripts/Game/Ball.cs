using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : FiniteStateMachine
{
    [SerializeField][Range(0.0f, 10.0f)] float m_speed = 5.0f;
    [SerializeField][Range(0.0f, 5.0f)] float m_radius = 1.0f;

    public enum eState
    {
        INACTIVE,
        ENTER,
        ACTIVE,
        HIT
    }

    Ray m_ray = new Ray(Vector3.zero, Vector3.zero);

    public Vector3 direction { get; set; }
    public float timeScale { get; set; }
    
    void Awake()
    {
        InitializeStateMachine<eState>(eState.INACTIVE, true);
        AddTransitionsToState(eState.INACTIVE, new System.Enum[] { eState.ACTIVE, eState.ENTER });
        AddTransitionsToState(eState.ENTER, new System.Enum[] { eState.ACTIVE });
        AddTransitionsToState(eState.ACTIVE, new System.Enum[] { eState.HIT, eState.ENTER });
        AddTransitionsToState(eState.HIT, new System.Enum[] { eState.INACTIVE });

        direction = new Vector3(0.5f, -0.5f, 0.0f);
        direction.Normalize();
        timeScale = 1.0f;
    }

	void Start()
    {
		//
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

    private void UpdateACTIVE()
    {
        float distance = m_speed * Time.deltaTime;
        Vector3 velocity = (direction * distance);

        m_ray.origin = transform.position;
        m_ray.direction = direction;
        RaycastHit rayHit;

        if (Physics.Raycast(m_ray, out rayHit))
        {
            if (rayHit.distance < distance + m_radius)
            {
                direction = Vector3.Reflect(direction, rayHit.normal);
                transform.position = rayHit.point + (direction * (distance - rayHit.distance));
            }
            else
            {
                transform.position = transform.position + velocity;
            }
        }
        else
        {
            transform.position = transform.position + velocity;
        }
    }
}
