using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupGameObject : MonoBehaviour {

    AudioSource m_audioSource;

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (m_audioSource)
                m_audioSource.Play();
            OnPickup(other.gameObject);
            Destroy(this.gameObject);
        }
    }

    public abstract void OnPickup(GameObject player);
}
