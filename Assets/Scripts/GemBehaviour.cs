using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBehaviour : PickupGameObject
{

    public override void OnPickup(GameObject player)
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().m_amountOfGems[player.GetComponent<PlayerInput>().m_playerNumber]++;
    }
}