using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

[RequireComponent(typeof(PlayerController))]
public class PlayerInput : MonoBehaviour {

    [Tooltip("time until the player can dodge again in seconds")]
    public float m_dodgeCooldown = 2f;
    public float m_attackCooldown = 1f;
    private PlayerController m_Character; // A reference to the PlayerController on the object
    private InputDevice m_usersController = new InputDevice("none");
    public int m_playerNumber = -1;

    private Vector3 m_move;
    public float m_dodging; // dodging is time in seconds since last dodge (m_dodgeCooldown being max)
    public float m_attacking; // attacking is time in seconds since last Attack (m_attackCooldown being max)
    //public bool m_ableToAttack = false; // used for sword power up (not ideal :/)

    //public accessor for the player number
    public int PlayerNumber
    {
        get { return m_playerNumber; }
        set { m_playerNumber = value;
            if (InputManager.Devices.Count > 0)
                m_usersController = InputManager.Devices[m_playerNumber];
        }
    }

    // Use this for initialization
    void Start () {
        m_attacking = m_attackCooldown;
        m_dodging = m_dodgeCooldown;
        m_Character = GetComponent<PlayerController>();
    }

    private void Update()
    {
        float h = 0;
        float v = 0;

        switch (InputManager.Devices.Count)
        {
            case (0):
                //keyboard controls
                CheckKeyboardControls(out h, out v);
                break;

            case (1):
                GameController Gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
                //if singlePlayer
                if (Gc.m_amountOfPlayers == 1)
                {
                    CheckControllerControls(out h, out v);
                }

                //if multiPlayer
                if (Gc.m_amountOfPlayers > 1)
                {
                    if (m_playerNumber == 0)
                    {
                        CheckKeyboardControls(out h, out v);
                    }
                    else
                    {
                        m_usersController = InputManager.Devices[m_playerNumber - 1];
                        CheckControllerControls(out h, out v);
                    }
                }
                break;

            case (2):
                m_usersController = InputManager.Devices[m_playerNumber];
                CheckControllerControls(out h, out v);
                break;
        }

        // we use world-relative directions in the case of no main camera
        m_move = new Vector3(-h, 0, -v);

        m_Character.Move(m_move, m_dodging, m_attacking);

        if (m_dodging < m_dodgeCooldown)
            m_dodging += Time.deltaTime;
        if (m_attacking < m_attackCooldown)
            m_attacking += Time.deltaTime;
        if (m_dodging > m_dodgeCooldown)
            m_dodging = m_dodgeCooldown;
        if (m_attacking > m_attackCooldown)
            m_attacking = m_attackCooldown;

        Debug.Log("attacking: " + m_attacking + " attack cooldown: " + m_attackCooldown);
        Debug.Log("dodging: " + m_dodging + " dodging cooldown " + m_dodgeCooldown);

    }

    void CheckKeyboardControls(out float h, out float v)
    {
        h = ((Input.GetKey(KeyCode.A) ? -1 : 0) - (Input.GetKey(KeyCode.D) ? -1 : 0));
        v = ((Input.GetKey(KeyCode.W) ? 1 : 0) - (Input.GetKey(KeyCode.S) ? 1 : 0));

        if (m_attacking >= m_attackCooldown && Input.GetKeyDown(KeyCode.Space))
        {
            m_attacking = 0;
        }

        if (m_dodging >= m_dodgeCooldown && Input.GetKeyDown(KeyCode.LeftShift))
        {
            m_dodging = 0;
        }
    }

    void CheckControllerControls(out float h, out float v)
    {
        h = m_usersController.LeftStickX;
        v = m_usersController.LeftStickY;

        if (m_attacking >= m_attackCooldown && m_usersController.Action1.WasPressed)
        {
            m_attacking = 0;
        }

        if (m_dodging >= m_dodgeCooldown && m_usersController.Action2.WasPressed)
        {
            m_dodging = 0;
        }
    }

}
