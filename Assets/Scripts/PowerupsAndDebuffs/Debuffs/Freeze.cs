using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : PowerupOrDebuff {

    public GameObject m_iceBlockPrefab;
    public GameObject m_iceBreakingParticles;
    public Vector3 m_particleSpawnOffset;
    private GameObject m_instantiatedIceBlock;

    public override void Initialize()
    {
        GetComponent<PlayerInput>().enabled = false;
        m_instantiatedIceBlock = Instantiate(m_iceBlockPrefab, gameObject.transform);
        m_instantiatedIceBlock.transform.position = transform.position;
    }

    public override void Uninitialize()
    {
        if (m_audioSource)
            m_audioSource.Play();
        //create particles
        GameObject iceParticles = Instantiate(m_iceBreakingParticles, transform.position + m_particleSpawnOffset, Quaternion.identity);
        DestroyAfterTime destroyAfterTime = iceParticles.AddComponent<DestroyAfterTime>();
        destroyAfterTime.m_lifeTime = iceParticles.GetComponent<ParticleSystem>().main.duration;
        GetComponent<PlayerInput>().enabled = true;
        Destroy(m_instantiatedIceBlock);
    }
}
