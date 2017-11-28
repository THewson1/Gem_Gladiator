using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BounceInternaly : MonoBehaviour {

    private GameObject m_platform;
    public List<GameObject> m_objectsToBounceTowards;
    private float m_radius;
    public float m_bounceForce;
    public bool m_useObjectsVelocity;

    private Rigidbody m_rb;

    // Use this for initialization
    void Start()
    {
        m_platform = GameObject.FindGameObjectWithTag("ArenaHitBox");
        m_objectsToBounceTowards.Add(GameObject.FindGameObjectWithTag("ArenaHitBox"));
        m_rb = GetComponent<Rigidbody>();
        m_radius = m_platform.transform.localScale.x / 2 - m_platform.transform.localScale.x / 13.6f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 directionFromCenter = (transform.position - m_platform.transform.position);
        float distanceFromCenter = directionFromCenter.magnitude;

        if (distanceFromCenter > m_radius)
        {
            float currentVelocity = m_rb.velocity.magnitude;
            m_rb.velocity = Vector3.zero;
            Vector3 averagePosition = new Vector3();

            foreach (GameObject GameObject in m_objectsToBounceTowards)
            {
                averagePosition += GameObject.transform.position;
            }

            averagePosition = new Vector3(averagePosition.x / m_objectsToBounceTowards.Count, averagePosition.y / m_objectsToBounceTowards.Count, averagePosition.z / m_objectsToBounceTowards.Count);

            Vector3 reflectionVector = (averagePosition - transform.position).normalized;

            if (m_useObjectsVelocity)
                m_rb.AddForce(reflectionVector * (currentVelocity * m_bounceForce));
            else
                m_rb.AddForce(reflectionVector * m_bounceForce);
        }

        //fix for glitching out of arena1
        if (distanceFromCenter > m_radius + m_radius / 10)
        {
            transform.position = directionFromCenter.normalized * m_radius;
        }
    }
}
