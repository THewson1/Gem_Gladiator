using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheild : PowerupOrDebuff {

    private GameObject m_sheild = null;

    public float m_explosionForce = 1000;
    public float m_explosionRadius = 4;

    private PlayerDeathLogic m_playerDeathLogic;

    public override void Initialize()
    {
        //spawn sheild on player
        foreach (GameObject child in FindChildren(transform))
        {
            if (child.CompareTag("Shield"))
                m_sheild = child;
        }
        if (m_sheild != null)
            m_sheild.SetActive(true);

        m_playerDeathLogic = GetComponent<PlayerDeathLogic>();
        m_playerDeathLogic.m_invincible = true;
    }

    List<GameObject> FindChildren(Transform parent)
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in parent)
        {
            children.Add(child.gameObject);
            foreach (GameObject child2 in FindChildren(child))
                children.Add(child2);
        }
        return children;
    }

    public override void Uninitialize()
    {
        //clean up
        if (m_sheild != null)
            m_sheild.SetActive(false);

        m_playerDeathLogic.m_invincible = false;
        Destroy(this);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Boulder"))
        {
            GameObject[]  listOfGameObjects = FindObjectsOfType<GameObject>();

            for (int i = 0; i < listOfGameObjects.Length; i ++)
            {
                Rigidbody rb = listOfGameObjects[i].GetComponent<Rigidbody>();
                if (rb && !listOfGameObjects[i].CompareTag("Player"))
                {
                    rb.velocity = Vector3.zero;
                    rb.AddExplosionForce(m_explosionForce, transform.position + Vector3.up, m_explosionRadius * rb.mass);
                    if (listOfGameObjects[i].CompareTag("Boulder"))
                    {
                        Vector3 directionToMove = (other.transform.position - transform.position).normalized;
                        rb.AddForce(directionToMove * m_explosionForce, ForceMode.Impulse);
                    }
                }
            }
            m_audioSource.Play();
            Invoke("Uninitialize", 1);
        }
    }
}
