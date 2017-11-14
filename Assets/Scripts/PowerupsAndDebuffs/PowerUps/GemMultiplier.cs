using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemMultiplier : PowerupOrDebuff {

    public int m_extraGemsToAdd = 1;

    public override void Initialize()
    {
        //called when this script is applied to the player
    }

    public override void Uninitialize()
    {
        //called when this script is removed from the player
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gem"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().m_amountOfGems[GetComponent<PlayerInput>().m_playerNumber] += m_extraGemsToAdd;
        }
    }
}
