using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MaintainVelocity : MonoBehaviour {

    [Range(0, 5)] public float m_desiredVelocity;
    private Rigidbody m_rb;

	// Use this for initialization
	void Start () {
        m_rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (m_rb.velocity.magnitude < m_desiredVelocity)
        {
            Vector3 forceToadd = m_rb.velocity.normalized * m_desiredVelocity;
            m_rb.velocity = m_rb.velocity + forceToadd;
        }
	}
}
