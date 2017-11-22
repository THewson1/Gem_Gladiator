using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVsPlayerCombat : MonoBehaviour {

    public float m_range = 1;
    public float m_knockbackForce = 1;

    // Update is called once per frame
    void Update () {
		
	}

    public void TryToHitPlayers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in players)
        {
            if (player != gameObject)
            {
                Vector3 direction = player.transform.position - gameObject.transform.position;
                float distance = direction.magnitude;
                direction.Normalize();
                if (distance < m_range)
                {
                    player.GetComponent<Rigidbody>().AddForce(direction * m_knockbackForce, ForceMode.VelocityChange);
                    player.GetComponent<Animator>().SetTrigger("Stunned");
                }
            }
        }
    }
}
