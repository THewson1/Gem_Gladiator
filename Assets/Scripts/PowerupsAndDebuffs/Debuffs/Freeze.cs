using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : PowerupOrDebuff {

    public GameObject m_iceBlock;
    public GameObject m_iceBreakingParticles;
    private GameObject m_instantiatedIceBlock;

    public override void Initialize()
    {
        GetComponent<PlayerInput>().enabled = false;
        m_instantiatedIceBlock = Instantiate(m_iceBlock, gameObject.transform);
    }

    public override void Uninitialize()
    {
        //create particles
        GameObject iceParticles = Instantiate(m_iceBreakingParticles, transform.position, Quaternion.identity);
        DestroyAfterTime destroyAfterTime = iceParticles.AddComponent<DestroyAfterTime>();
        destroyAfterTime.m_lifeTime = iceParticles.GetComponent<ParticleSystem>().main.duration;
        GetComponent<PlayerInput>().enabled = true;
        Destroy(m_instantiatedIceBlock);
    }
}
