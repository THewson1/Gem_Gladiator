using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupGameObject : MonoBehaviour {

    public AudioSource m_pickupSoundEffect;

    private void Start()
    {
        m_pickupSoundEffect = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (m_pickupSoundEffect)
                m_pickupSoundEffect.Play();
            OnPickup(other.gameObject);
            Destroy(this.gameObject);
        }
    }

    public abstract void OnPickup(GameObject player);
}
