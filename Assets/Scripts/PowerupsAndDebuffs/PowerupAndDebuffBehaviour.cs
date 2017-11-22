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

    AudioSource CopySound(AudioSource original, GameObject destination)
    {
        AudioSource audioSource = destination.AddComponent<AudioSource>();
        audioSource.clip = original.clip;
        audioSource.volume = original.volume;
        return audioSource;
    }

    PowerupOrDebuff CopyComponent(Component original, GameObject destination)
    {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        // Copied fields can be restricted with BindingFlags
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return (PowerupOrDebuff)copy;
    }

    public void CopyPowerupOrDebuffToPlayer(GameObject player)
    {
        PowerupOrDebuff script = CopyComponent(GetComponent(m_script.GetType()), player);
        if (GetComponent<AudioSource>())
        {
            AudioSource audioSource = CopySound(GetComponent<AudioSource>(), player);
            script.m_audioSource = audioSource;
        }
    }
}

