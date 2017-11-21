using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundWhenTouchingGround : MonoBehaviour {

    public float m_velocityRequired;
    public string m_requiredTag = null;
    public AudioSource m_runningSoundEffect;
    public float m_range = 1;

    // Update is called once per frame
    void FixedUpdate () {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down * m_range, out hit))
        {
            if (m_requiredTag != null)
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

    void PlaySound()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb.velocity.magnitude > m_velocityRequired)
        {
            if (m_runningSoundEffect)
                m_runningSoundEffect.Play();
        }
    }
}
