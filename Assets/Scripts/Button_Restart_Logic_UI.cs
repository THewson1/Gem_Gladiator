using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button_Restart_Logic_UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{

    public GameObject m_hand;
    public GameObject m_handToChange;

    public void OnPointerEnter (PointerEventData eventData) {
        Enter();
    }

    public void OnPointerExit (PointerEventData eventData) {
        Exit();
    }

    public void Enter()
    {
        m_handToChange.SetActive(false);
        m_hand.SetActive(true);
    }

    public void Exit()
    {
        m_handToChange.SetActive(true);
        m_hand.SetActive(false);
    }
}
