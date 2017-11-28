using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControllerMenuSupport : MonoBehaviour {

    public GameObject m_gemPrefab;
    public List<GameObject> m_buttons;
    public int m_buttonToStartOn;
    public Vector3 m_offset;
    public bool m_hasMouseOverEvents = false;

    private InputDevice m_usersController = null;
    private int m_currentButton;
    private GameObject m_gem;

    private void Start()
    {
        if (m_currentButton >= m_buttons.Count)
            m_currentButton = 0;
        m_currentButton = m_buttonToStartOn;
        m_gem = Instantiate(m_gemPrefab, transform);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < InputManager.Devices.Count; i++)
        {
            m_usersController = InputManager.Devices[i];
            if (m_usersController.DPadDown.WasPressed)
            {
                if (m_hasMouseOverEvents)
                {
                    Button_Restart_Logic_UI btn = m_buttons[m_currentButton].GetComponent<Button_Restart_Logic_UI>();
                    btn.Exit();
                }
                m_currentButton++;
                if (m_currentButton >= m_buttons.Count)
                    m_currentButton = 0;
                if (m_hasMouseOverEvents)
                {
                    Button_Restart_Logic_UI btn = m_buttons[m_currentButton].GetComponent<Button_Restart_Logic_UI>();
                    btn.Enter();
                }

            }

            if (m_usersController.DPadUp.WasPressed)
            {
                if (m_hasMouseOverEvents)
                {
                    Button_Restart_Logic_UI btn = m_buttons[m_currentButton].GetComponent<Button_Restart_Logic_UI>();
                    btn.Exit();
                }
                m_currentButton--;
                if (m_currentButton <= -1)
                    m_currentButton = m_buttons.Count - 1;
                if (m_hasMouseOverEvents)
                {
                    Button_Restart_Logic_UI btn = m_buttons[m_currentButton].GetComponent<Button_Restart_Logic_UI>();
                    btn.Enter();
                }
            }

            // select
            if (m_usersController.Action1.WasReleased)
            {
                Button btn = m_buttons[m_currentButton].GetComponent<Button>();
                btn.onClick.Invoke();
            }
        }
        if (InputManager.Devices.Count > 0)
        {
            m_gem.SetActive(true);
            m_gem.transform.position = m_buttons[m_currentButton].transform.position - m_offset;
        }
        if (InputManager.Devices.Count <= 0 || gameObject.activeInHierarchy == false)
            m_gem.SetActive(false);
    }
}
