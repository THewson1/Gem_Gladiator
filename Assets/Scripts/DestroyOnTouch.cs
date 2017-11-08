using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTouch : MonoBehaviour {

    public string m_tagOfDestroyer;
    public float m_delay;

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
        Destroy(gameObject);
    }
}
