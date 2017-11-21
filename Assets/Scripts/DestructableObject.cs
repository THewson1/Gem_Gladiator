using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour {

    [Tooltip("Add objects in the order that they will be destroyed")]
    public List<GameObject> m_parts;
    public List<string> m_tagsThatDestroyThis;
    public float m_breakAwayForce;
    public float m_partLifetime;

    // Use this for initialization
    void Start () {
		for (int i = 0; i < m_parts.Count; i ++)
        {
            m_parts[i].AddComponent<Rigidbody>().isKinematic = true;
        }
	}
    
    private void OnTriggerEnter(Collider other)
    {
        foreach (string tag in m_tagsThatDestroyThis)
        {
            if (other.gameObject.CompareTag(tag))
            {
                DestroyAllParts();
            }
        }
    }
    
    private void OnCollisionEnter(Collision other) {
        foreach (string tag in m_tagsThatDestroyThis)
        {
            if (other.gameObject.CompareTag(tag))
            {
                DestroyTopPart();
                BounceBoulderAway(other.gameObject);
            }
        }
    }
    
    void DestroyAllParts()
    {
        for (int i = 0; i < m_parts.Count; i++)
        {
            Rigidbody rb = m_parts[i].GetComponent<Rigidbody>();
            rb.isKinematic = false;
            Transform popDirection = m_parts[i].transform;
            popDirection.Rotate(new Vector3(0, Random.Range(0, 360), 0));
            rb.AddForce(popDirection.forward + Vector3.up * m_breakAwayForce, ForceMode.Impulse);
            rb.AddTorque(popDirection.forward * m_breakAwayForce, ForceMode.Impulse);
            m_parts[i].AddComponent<DestroyAfterTime>().m_lifeTime = m_partLifetime;
            m_parts[i].transform.parent = null;
        }
        for (int i = 0; i < m_parts.Count; i++)
        {
            m_parts.RemoveAt(0);
        }
        Destroy(GetComponent<BoxCollider>());
        Invoke("DestroySelf", m_partLifetime);
    }
    
    void DestroyTopPart() {
        Rigidbody rb = m_parts[0].GetComponent<Rigidbody>();
        rb.isKinematic = false;
        Transform popDirection = m_parts[0].transform;
        popDirection.Rotate(new Vector3(0, Random.Range(0, 360), 0));
        rb.AddForce( popDirection.forward + Vector3.up * m_breakAwayForce, ForceMode.Impulse);
        rb.AddTorque(popDirection.forward * m_breakAwayForce, ForceMode.Impulse);
        m_parts[0].AddComponent<DestroyAfterTime>().m_lifeTime = m_partLifetime;
        m_parts.RemoveAt(0);



        if (m_parts.Count == 0)
        {
            Destroy(GetComponent<BoxCollider>());
            Invoke("DestroySelf", m_partLifetime);
        }
    }

    void BounceBoulderAway(GameObject other)
    {
        Rigidbody otherRB = other.GetComponent<Rigidbody>();
        float boulderVelocity = otherRB.velocity.magnitude;
        Vector3 newDirection = other.transform.position - transform.position;
        otherRB.velocity = newDirection * boulderVelocity;
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

}
