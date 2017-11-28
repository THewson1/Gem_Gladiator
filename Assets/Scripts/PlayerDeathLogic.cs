using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathLogic : MonoBehaviour {

    public int m_lives = 1;
    public GameObject m_deadPlayer;
    [Range(0, 10)] public float m_respawnTime = 0;
    [Range(0, 10)] public float m_invincibilityTimer = 0;

    public bool m_invincible = false;

    public void Die()
    {
        if (!m_invincible)
        {
            m_lives--;
            GameObject deadPlayer = Instantiate(m_deadPlayer, transform.position, transform.rotation);
            // this next line is a very long, hard coded, way of finding the texture and putting it on the dead player.
            deadPlayer.transform.Find("gladiator_lowpoly:Mesh").gameObject.GetComponent<Renderer>().material = transform.Find("Glen").Find("gladiator_lowpoly:Mesh").gameObject.GetComponent<Renderer>().material;

            gameObject.SetActive(false);

            // remove all powerups and debuffs
            PowerupOrDebuff[] powerUpsAndDebuffs = GetComponents<PowerupOrDebuff>();
            for (int i = 0; i < powerUpsAndDebuffs.Length; i++)
            {
                powerUpsAndDebuffs[i].Uninitialize();
                Destroy(powerUpsAndDebuffs[i]);
            }

            if (m_lives > 0)
                Invoke("Respawn", m_respawnTime);
            else
                GameOver();
        }
    }

    void Respawn()
    {
        transform.position = Vector3.zero + (Vector3.up * 5);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(true);
        m_invincible = true;
        gameObject.layer = LayerMask.NameToLayer("Ghost");
        Invoke("MakeVincible", m_invincibilityTimer);
        GetComponent<Animator>().Play("Idle_Move");
    }

    void MakeVincible()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        m_invincible = false;
    }

    void GameOver()
    {

    }

}
