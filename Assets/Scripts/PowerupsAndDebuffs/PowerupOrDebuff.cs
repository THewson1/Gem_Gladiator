using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerupOrDebuff : MonoBehaviour {

    [Tooltip("leave zero if no lifetime is required")]
    [SerializeField] public float m_lifetime;

	// Use this for initialization
	void Start () {
        if (gameObject.CompareTag("Player"))
        {
            if (m_lifetime != 0)
                StartCoroutine(DestroyAfterTime(m_lifetime));
            Initialize();
        }
	}

    public abstract void Initialize();
    public abstract void Uninitialize();

    IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Uninitialize();
        Destroy(this);
    }
}
