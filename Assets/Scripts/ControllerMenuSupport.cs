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
        // fix any errors created by the user
        if (m_buttonToStartOn >= m_buttons.Count)
            m_buttonToStartOn = 0;
        // start on the correct button
        m_currentButton = m_buttonToStartOn;
        // spawn the gem used for UI selection
        m_gem = Instantiate(m_gemPrefab, transform);
    }

    // Update is called once per frame
    void Update()
    {
        // for all the controllers connected to the computer
        for (int i = 0; i < InputManager.Devices.Count; i++)
        {
            m_usersController = InputManager.Devices[i];
            // go to the next button in the list of buttons
            if (m_usersController.DPadDown.WasPressed)
            {
                if (m_hasMouseOverEvents)
                {
                    // call exit on the previous button
                    ButtonRestartLogicUI btn = m_buttons[m_currentButton].GetComponent<ButtonRestartLogicUI>();
                    btn.Exit();
                }
                m_currentButton++;
                // loop back to the first button if the previous button was the last one in the list
                if (m_currentButton >= m_buttons.Count)
                    m_currentButton = 0;
                if (m_hasMouseOverEvents)
                {
                    // call enter on the new button
                    ButtonRestartLogicUI btn = m_buttons[m_currentButton].GetComponent<ButtonRestartLogicUI>();
                    btn.Enter();
                }

            }

            // go to the previous button in the list of buttons
            if (m_usersController.DPadUp.WasPressed)
            {
                if (m_hasMouseOverEvents)
                {
                    // call exit on the previous button
                    ButtonRestartLogicUI btn = m_buttons[m_currentButton].GetComponent<ButtonRestartLogicUI>();
                    btn.Exit();
                }
                m_currentButton--;
                // loop back to the last button if the previous button was the first one in the list
                if (m_currentButton <= -1)
                    m_currentButton = m_buttons.Count - 1;
                if (m_hasMouseOverEvents)
                {
                    // call enter on the previous button
                    ButtonRestartLogicUI btn = m_buttons[m_currentButton].GetComponent<ButtonRestartLogicUI>();
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
        // activate the gem if at least one controller is connected
        if (InputManager.Devices.Count > 0)
        {
            m_gem.SetActive(true);
            m_gem.transform.position = m_buttons[m_currentButton].transform.position - m_offset;
        }
        // de-activate the gem if it should not be seen
        if (InputManager.Devices.Count <= 0 || gameObject.activeInHierarchy == false)
            m_gem.SetActive(false);
    }
}
