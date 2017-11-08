using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBehaviour : PickupGameObject
{
    public override void OnPickup()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().m_amountOfGems++;
    }
}

    
