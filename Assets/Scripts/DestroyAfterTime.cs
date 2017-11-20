using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {

    public float m_lifeTime = 1;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(m_lifeTime);
        if (gameObject != null)
            Destroy(gameObject);
    }
}