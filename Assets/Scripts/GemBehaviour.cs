using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBehaviour : PickupGameObject
{

    public override void OnPickup(GameObject player)
    {
        // add to the correct players gem counter
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().m_amountOfGems[player.GetComponent<PlayerInput>().m_playerNumber]++;
    }
}