using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlatformBehaviourPhysicsBased : MonoBehaviour {

    private GameObject[] Anchors = new GameObject[5];
    public float arenasize = 1;
    public float MiddleAnchorStrength;
    public float rotationalAnchorStrength;
    public float rotationalAnchorDamper;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < Anchors.Length; i++)
        {
            Anchors[i] = new GameObject();
            Anchors[i].AddComponent<SpringJoint>();
            Anchors[i].GetComponent<Rigidbody>().isKinematic = true;
            Anchors[i].GetComponent<Rigidbody>().useGravity = false;
            Anchors[i].GetComponent<SpringJoint>().connectedBody = GetComponent<Rigidbody>();
        }
            
        Anchors[0].GetComponent<SpringJoint>().spring = MiddleAnchorStrength;

        Anchors[1].GetComponent<SpringJoint>().spring = rotationalAnchorStrength;
        Anchors[1].GetComponent<SpringJoint>().anchor = new Vector3(arenasize, 0, 0);
        Anchors[1].GetComponent<SpringJoint>().damper = rotationalAnchorDamper;

        Anchors[2].GetComponent<SpringJoint>().spring = rotationalAnchorStrength;
        Anchors[2].GetComponent<SpringJoint>().anchor = new Vector3(-arenasize, 0, 0);
        Anchors[2].GetComponent<SpringJoint>().damper = rotationalAnchorDamper;

        Anchors[3].GetComponent<SpringJoint>().spring = rotationalAnchorStrength;
        Anchors[3].GetComponent<SpringJoint>().anchor = new Vector3(0, 0, arenasize);
        Anchors[3].GetComponent<SpringJoint>().damper = rotationalAnchorDamper;

        Anchors[4].GetComponent<SpringJoint>().spring = rotationalAnchorStrength;
        Anchors[4].GetComponent<SpringJoint>().anchor = new Vector3(0, 0, -arenasize);
        Anchors[4].GetComponent<SpringJoint>().damper = rotationalAnchorDamper;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Quaternion originalRotation = transform.rotation;

        transform.rotation = originalRotation * Quaternion.AngleAxis(0, Vector3.up);
    }

}
