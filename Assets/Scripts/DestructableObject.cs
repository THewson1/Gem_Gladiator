using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour {

    [Tooltip("Add objects in the order that they will be destroyed")]
    public List<GameObject> m_parts = null;
    public List<string> m_tagsThatDestroyThis;
    public float m_breakAwayForce;
    public float m_partLifetime;

    private AudioSource m_audioSource;

    // Use this for initialization
    void Start () {
        // find the audiosource that is on this object
        m_audioSource = GetComponent<AudioSource>();
        // add rigidbodys to all the parts and freeze them for now
        if (m_parts != null)
        {
            for (int i = 0; i < m_parts.Count; i++)
            {
                m_parts[i].AddComponent<Rigidbody>().isKinematic = true;
            }
        }
	}
    
    /// <summary>
    /// Objects with trigger colliders will be instantly destroyed
    /// </summary>
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
    
    /// <summary>
    /// Objects with regular colliders will pop the top part off
    /// </summary>
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
        // play the audiosource on this object if it exists
        if (m_audioSource)
            m_audioSource.Play();
        // go through all the parts and pop them off with the corrrect force
        for (int i = 0; i < m_parts.Count; i++)
        {
            Rigidbody rb = m_parts[i].GetComponent<Rigidbody>();
            rb.isKinematic = false;
            Transform popDirection = m_parts[i].transform;
            popDirection.Rotate(new Vector3(0, Random.Range(0, 360), 0));
            rb.AddForce(popDirection.forward + Vector3.up * m_breakAwayForce, ForceMode.Impulse);
            rb.AddTorque(popDirection.forward * m_breakAwayForce, ForceMode.Impulse);
            m_parts[i].AddComponent<DestroyAfterTime>().m_lifeTime = m_partLifetime;
            // change the layer to correctly manage collisions
            m_parts[i].layer = LayerMask.NameToLayer("Ghost");
            m_parts[i].transform.parent = null;
        }
        // remove all the parts from this object's list of parts
        for (int i = 0; i < m_parts.Count; i++)
        {
            m_parts.RemoveAt(0);
        }
        Destroy(GetComponent<BoxCollider>());
        Invoke("DestroySelf", m_partLifetime);
    }
    
    void DestroyTopPart() {
        // play the audiosource on this object if it exists
        if (m_audioSource)
            m_audioSource.Play();
        // pop off the top element of the parts array with the correct force
        Rigidbody rb = m_parts[0].GetComponent<Rigidbody>();
        rb.isKinematic = false;
        Transform popDirection = m_parts[0].transform;
        popDirection.Rotate(new Vector3(0, Random.Range(0, 360), 0));
        rb.AddForce( popDirection.forward + Vector3.up * m_breakAwayForce, ForceMode.Impulse);
        rb.AddTorque(popDirection.forward * m_breakAwayForce, ForceMode.Impulse);
        m_parts[0].AddComponent<DestroyAfterTime>().m_lifeTime = m_partLifetime;
        // change the layer to correctly manage collisions
        m_parts[0].layer = LayerMask.NameToLayer("Ghost");
        m_parts.RemoveAt(0);

        if (m_parts.Count == 0)
        {
            Destroy(GetComponent<BoxCollider>());
            Invoke("DestroySelf", m_partLifetime);
        }
    }

    // bounce the boulder away from this object
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
