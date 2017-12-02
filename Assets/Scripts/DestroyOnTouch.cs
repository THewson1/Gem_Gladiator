using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTouch : MonoBehaviour {

    public string m_tagOfDestroyer;
    public float m_delay;

    private AudioSource m_audioSource;

    private void Start()
    {
        // find the audiosource that is on this object
        m_audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(m_tagOfDestroyer))
        {
            StartCoroutine(DestroyGameObject());
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag(m_tagOfDestroyer))
        {
            StartCoroutine(DestroyGameObject());
        }
    }

    IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(m_delay);
        // create an audio player that lives long enough after this object to play the sound
        GameObject audioSource = Instantiate(new GameObject(gameObject.name + " sound"));
        audioSource.AddComponent<DestroyAfterTime>().m_lifeTime = 5;
        audioSource.AddComponent<AudioSource>().clip = m_audioSource.clip;
        AudioSource aus = audioSource.GetComponent<AudioSource>();
        aus.volume = m_audioSource.volume;
        aus.Play();
        // destroy this gameobject (the audiosource gameobject stays around long enough to play it's sound)
        Destroy(gameObject);
    }
}
