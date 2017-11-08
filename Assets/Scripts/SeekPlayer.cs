using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
public class SeekPlayer : MonoBehaviour {

    [Tooltip("The speed at which the game object will move towards the player")]
    [Range(1000f, 50000f)] public float seekSpeed = 1000f;
    [Tooltip("leave on 0 if no range is required")]
    [Range(0f, 100f)] public float targetRange;
    private Rigidbody m_rb;

    // Use this for initialization
    void Start () {
        m_rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        GameController gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        GameObject closestPlayer = null;
        float currentMinDist = Mathf.Infinity;

        for (int i = 0; i < gc.m_listOfPlayers.Count; i ++)
        {
            float newDistance = (gc.m_listOfPlayers[i].transform.position - transform.position).magnitude;
            if (newDistance < currentMinDist && (targetRange == 0 || newDistance < targetRange))
            {
                currentMinDist = newDistance;
                closestPlayer = gc.m_listOfPlayers[i];
            }
        }

        if (closestPlayer != null)
        {
            
            Vector3 directionTowardsPlayer = (closestPlayer.transform.position - transform.position);
            m_rb.AddForce(directionTowardsPlayer.normalized * seekSpeed * Time.deltaTime);
        }
	}
}
