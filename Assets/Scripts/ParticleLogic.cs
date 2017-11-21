using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR || UNITY_EDITOR_WIN
using UnityEditor;
#endif
using UnityEngine;

public class ParticleLogic : MonoBehaviour {

    public GameObject m_particlePrefab;
    private GameObject m_particle;
    private ParticleSystem[] m_particleEmitters;
    public Vector3 m_offset;
    public bool m_alwaysActive;

    public bool m_mustBeTouchingGround = false;
    public float m_range;

    public bool m_triggeredByCollision;
    public bool m_onCollisionEnter;
    public bool m_onCollisionStay;
    public bool m_onCollisionExit;

    public bool m_triggeredByTrigger;
    public bool m_onTriggerEnter;
    public bool m_onTriggerStay;
    public bool m_onTriggerExit;

    public string m_requiredTag = "NULL";
    public float m_requiredForce = 0;

    public bool m_triggeredByMovement;
    public float m_requiredMagnitude;

    // Use this for initialization
    void Start () {
        m_particle = Instantiate(m_particlePrefab as GameObject, transform.parent);
        m_particle.name = name + " " + m_particlePrefab.name;
        m_particleEmitters = m_particle.GetComponents<ParticleSystem>();
        if (m_alwaysActive)
        {
            for (int i = 0; i < m_particleEmitters.Length; i++)
            {
                m_particleEmitters[i].Play();
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (m_mustBeTouchingGround)
        {
            Vector3 dwn = Vector3.down;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dwn, out hit))
            {
                if (hit.distance > m_range)
                {
                    m_particle.SetActive(false);
                }
                else
                {
                    m_particle.SetActive(true);
                }
            }
        }
        m_particle.transform.position = transform.position + m_offset;
	}
     
    void Emit() {
        for (int i = 0; i < m_particleEmitters.Length; i ++) 
        {
            m_particleEmitters[i].Play();
            Debug.Log("particle " + m_particlePrefab.name + " was played");
        }
    }

    bool CheckTag(string tag)
    {
        if (m_requiredTag == "NULL" || m_requiredTag == tag)
            return true;
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (m_onCollisionEnter)
        {
            if (CheckTag(collision.gameObject.tag))
            {
                if (collision.relativeVelocity.magnitude > m_requiredMagnitude)
                {
                    Emit();
                }
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (CheckTag(collision.gameObject.tag))
        {
            if (m_requiredForce > 0 && collision.relativeVelocity.magnitude > m_requiredMagnitude)
            {
                Emit();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (m_onCollisionExit)
        {
            if (CheckTag(collision.gameObject.tag))
            {
                if (collision.relativeVelocity.magnitude > m_requiredMagnitude)
                {
                    Emit();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_onTriggerEnter)
        {
            if (CheckTag(other.gameObject.tag))
            {
                Emit();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (m_onTriggerStay)
        {
            if (CheckTag(other.gameObject.tag))
            {
                Emit();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_onTriggerExit)
        {
            if (CheckTag(other.gameObject.tag))
            {
                Emit();
            }
        }
    }

    void OnDestroy()
    {
        if (m_particle)
            m_particle.AddComponent<DestroyAfterTime>().m_lifeTime = m_particleEmitters[0].main.duration;
    }
}

#if UNITY_EDITOR || UNITY_EDITOR_WIN
[CustomEditor(typeof(ParticleLogic))]
public class ParticleLogicEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var myScript = target as ParticleLogic;

        myScript.m_particlePrefab = EditorGUILayout.ObjectField("Particle", myScript.m_particlePrefab, typeof(GameObject), true) as GameObject;
        myScript.m_offset = EditorGUILayout.Vector3Field("Offset From This", myScript.m_offset);
        myScript.m_alwaysActive = EditorGUILayout.Toggle("Always Active", myScript.m_alwaysActive);
        myScript.m_mustBeTouchingGround = EditorGUILayout.Toggle("Must Be Touching Ground", myScript.m_mustBeTouchingGround);

        if (myScript.m_mustBeTouchingGround)
        {
            myScript.m_range = EditorGUILayout.FloatField("Height Of Transform Origin", myScript.m_range);
        }

        if (!myScript.m_alwaysActive)
        {
            myScript.m_triggeredByCollision = EditorGUILayout.Toggle("Triggered By Collision", myScript.m_triggeredByCollision);
            if (myScript.m_triggeredByCollision)
            {
                myScript.m_onCollisionEnter = EditorGUILayout.Toggle("use OnCollisionEnter", myScript.m_onCollisionEnter);
                myScript.m_onCollisionStay = EditorGUILayout.Toggle("use OnCollisionStay", myScript.m_onCollisionStay);
                myScript.m_onCollisionExit = EditorGUILayout.Toggle("use OnCollisionExit", myScript.m_onCollisionExit);
                myScript.m_requiredTag = EditorGUILayout.TextField("required tag of collider", myScript.m_requiredTag);
                myScript.m_requiredForce = EditorGUILayout.FloatField("required force of collision", myScript.m_requiredForce);
            }

            myScript.m_triggeredByTrigger = EditorGUILayout.Toggle("Triggered By Trigger", myScript.m_triggeredByTrigger);
            if (myScript.m_triggeredByTrigger)
            {
                myScript.m_onTriggerEnter = EditorGUILayout.Toggle("use OnTriggerEnter", myScript.m_onTriggerEnter);
                myScript.m_onTriggerStay = EditorGUILayout.Toggle("use OnTriggerStay", myScript.m_onTriggerStay);
                myScript.m_onTriggerExit = EditorGUILayout.Toggle("use OnTriggerExit", myScript.m_onTriggerExit);
                myScript.m_requiredTag = EditorGUILayout.TextField("required tag of collider", myScript.m_requiredTag);
            }

            myScript.m_triggeredByMovement = EditorGUILayout.Toggle("Triggered By Movement", myScript.m_triggeredByMovement);
            if (myScript.m_triggeredByMovement)
            {
                myScript.m_requiredMagnitude = EditorGUILayout.FloatField("required magnitude of movement", myScript.m_requiredMagnitude);
            }
        }

    }
}
#endif
