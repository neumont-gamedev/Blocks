using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 2.0f)] float m_speed = 1.0f;

    enum eState
    {
        INACTIVE,
        ACTIVE
    }

    eState state { get; set; }

    void Awake()
    {
        state = eState.INACTIVE;
    }

	void Start()
    {
		
	}
	
	void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 position = transform.position;

        float dx = Mathf.Min(Mathf.Abs(target.x - position.x), m_speed) * Mathf.Sign(target.x - position.x);
        position.x = position.x + dx;
        transform.position = position;
    }
}
