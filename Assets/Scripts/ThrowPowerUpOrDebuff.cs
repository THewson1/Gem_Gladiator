using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPowerUpOrDebuff : MonoBehaviour
{

    public GameObject[] m_crouds;
    public GameObject[] m_debuffs;
    public GameObject[] m_powerups;

    [Range(0, 9)] public float m_minThrowForce = 10;
    [Range(1, 10)] public float m_maxThrowForce = 10;
    [Range(0, 59)] public float m_minWaitTime = 0;
    [Range(1, 60)] public float m_maxWaitTime = 1;


    // Use this for initialization
    void Start()
    {
        StartCoroutine(ThrowPowerupOrDebuff());
    }

    IEnumerator ThrowPowerupOrDebuff ()
    {
        float time = Random.Range(m_minWaitTime, m_maxWaitTime);
        yield return new WaitForSeconds(time);

        int i = Random.Range(0, m_crouds.Length);
        GameObject arena = GameObject.FindGameObjectWithTag("ArenaHitBox");

        Transform[] possibleAudianceMembers = m_crouds[i].GetComponentsInChildren<Transform>();
        Transform chosenAudianceMember = possibleAudianceMembers[Random.Range(0, possibleAudianceMembers.Length)];
        Vector3 throwOrigin = chosenAudianceMember.position;

        Vector3 throwDestination = arena.transform.position + arena.transform.up * 10;
        Vector3 throwDirection = (throwDestination - throwOrigin).normalized;

        GameObject objectToThrow;
        // 50 50 coin toss
        if (Random.Range(0, 2) < 1)
        {
            // throw powerup
            int j = Random.Range(0, m_powerups.Length);
            objectToThrow = m_powerups[j];
        }
        else
        {
            // throw debuff
            int j = Random.Range(0, m_debuffs.Length);
            objectToThrow = m_debuffs[j];
        }

        float throwForce = Random.Range(m_minThrowForce, m_maxThrowForce);

        GameObject thrownGameObject = Instantiate(objectToThrow, throwOrigin, Quaternion.identity);
        thrownGameObject.GetComponent<Rigidbody>().AddForce(throwDirection * throwForce, ForceMode.VelocityChange);

        StartCoroutine(ThrowPowerupOrDebuff());
    }

}
