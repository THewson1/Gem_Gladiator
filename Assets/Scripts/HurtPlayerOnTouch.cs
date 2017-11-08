using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayerOnTouch : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerDeathLogic playerDeathLogic = collision.gameObject.GetComponent<PlayerDeathLogic>();
            playerDeathLogic.Die();
        }
    }

	private void OnTriggerEnter(Collider Collision)
	{
		if (Collision.CompareTag("Player"))
		{
			PlayerDeathLogic playerDeathLogic = Collision.GetComponent<PlayerDeathLogic>();
			playerDeathLogic.Die();
		}
	}
}
