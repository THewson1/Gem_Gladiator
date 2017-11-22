using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundWhenTouchingGround : MonoBehaviour {

    public float m_velocityRequired;
    public string m_requiredTag = "NULL";
    public AudioSource m_soundEffect;
    public float m_range = 1;

    // Update is called once per frame
    void FixedUpdate () {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.distance <= m_range)
            {
                if (m_requiredTag != "NULL")
                {
                    if (hit.transform.CompareTag(m_requiredTag))
                    {
                        PlaySound();
                    }
                }
                else
                {
                    PlaySound();
                }
            }
        }
	}

    void PlaySound()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb.velocity.magnitude > m_velocityRequired)
        {
            if (m_soundEffect)
                m_soundEffect.Play();
        }
    }
}
