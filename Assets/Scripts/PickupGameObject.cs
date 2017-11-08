using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupGameObject : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            OnPickup();
        }
    }

    public abstract void OnPickup();
}
