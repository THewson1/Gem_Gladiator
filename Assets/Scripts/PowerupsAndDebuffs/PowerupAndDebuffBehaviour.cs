using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PowerupAndDebuffBehaviour : MonoBehaviour {

    public PowerupOrDebuff m_script;

    // Use this for initialization
    void Start () {
        m_script.enabled = false;
        GetComponent<BoxCollider>().isTrigger = true;
	}

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            if (m_script.GetType() != null)
            {
                CopyPowerupOrDebuffToPlayer(other.gameObject);
                Destroy(gameObject);
            }
        }
    }

    Component CopyComponent(Component original, GameObject destination)
    {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        // Copied fields can be restricted with BindingFlags
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy;
    }

    public Component CopyPowerupOrDebuffToPlayer(GameObject player)
    {
        return CopyComponent(GetComponent(m_script.GetType()), player);
    }
}

