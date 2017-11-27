using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Button_Restart_Logic_UI : MonoBehaviour {

    public int m_desiredRotation;
    public GameObject m_hand;

    private void OnMouseOver () {
        m_hand.transform.rotation = Quaternion.Euler(0, 0, m_desiredRotation);
    }

    private void OnMouseExit () {
        m_hand.transform.rotation = Quaternion.Euler(0, 0, 90);
    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
