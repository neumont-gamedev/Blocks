using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public enum eType
    {
        STANDARD
    }

    Rigidbody m_rigidbody = null;
    AudioSource m_audioSource = null;
    Balls m_owner = null;

    eType m_type = eType.STANDARD;
        
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
        m_rigidbody = GetComponent<Rigidbody>();
        m_audioSource = GetComponent<AudioSource>();
    }

    public void Initialize(Vector3 position, Vector3 direction, eType type, Balls owner)
    {
        transform.position = position;
        if (m_rigidbody == null)
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }
        m_rigidbody.AddForce(direction * (m_speed * 100.0f));
        m_type = type;
        m_owner = owner;
                
        SetState(eState.ENTER);
    }

    private void UpdateINACTIVE()
    {
        SetState(eState.ACTIVE);
    }

    private void OnCollisionEnter(Collision collision)
    {
        m_audioSource.Play();

        // randomize bounce
        // check if the ball is moving straight up or straight across, if so set a random offset angle to the velocity
        if (Mathf.Abs(Vector3.Dot(Vector3.up, m_rigidbody.velocity.normalized)) > 0.9f || Mathf.Abs(Vector3.Dot(Vector3.right, m_rigidbody.velocity.normalized)) > 0.9f)
        {
            float angle = Random.Range(10.0f, 20.0f) * Mathf.Sign(Random.Range(-1.0f, 1.0f));
            Quaternion qr = Quaternion.AngleAxis(angle, Vector3.forward);
            m_rigidbody.velocity = qr * m_rigidbody.velocity;
        }
    }
}
