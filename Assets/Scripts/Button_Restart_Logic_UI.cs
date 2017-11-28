using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Button_Restart_Logic_UI : MonoBehaviour {

    public GameObject m_hand;
    public GameObject m_handToChange;
        
    private void OnMouseOver () {
        m_handToChange.SetActive(false);
        m_hand.SetActive(true);
        Debug.Log("EnterWorking");
    }
    private void OnMouseEnter () {
        Debug.Log("EnterEnter");
    }

    private void OnMouseExit () {
        m_handToChange.SetActive(true);
        m_hand.SetActive(false);
    }
    
}
