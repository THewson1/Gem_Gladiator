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
            GameObject soundEffect = Instantiate(new GameObject());
            AudioSource audioSource = soundEffect.AddComponent<AudioSource>();
            audioSource.clip = m_pickupSoundEffect.clip;
            audioSource.volume = m_pickupSoundEffect.volume;
            soundEffect.AddComponent<DestroyAfterTime>().m_lifeTime = 5;
            audioSource.Play();
            OnPickup(other.gameObject);
            Destroy(this.gameObject);
        }
    }

    public abstract void OnPickup(GameObject player);
}
