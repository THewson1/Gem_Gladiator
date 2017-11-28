using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button_Restart_Logic_UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{

    public GameObject m_hand;
    public GameObject m_handToChange;

    public void OnPointerEnter (PointerEventData eventData) {
        m_handToChange.SetActive(false);
        m_hand.SetActive(true);
    }

    public void OnPointerExit (PointerEventData eventData) {
        m_handToChange.SetActive(true);
        m_hand.SetActive(false);
    }
}
