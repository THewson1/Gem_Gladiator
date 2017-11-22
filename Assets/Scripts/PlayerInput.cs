using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

[RequireComponent(typeof(PlayerController))]
public class PlayerInput : MonoBehaviour {

    [Tooltip("time until the player can dodge again in seconds")]
    [SerializeField]
    private float m_dodgeCooldown = 2f;
    [SerializeField]
    private float m_attackCooldown = 1f;
    private PlayerController m_Character; // A reference to the PlayerController on the object
    private InputDevice m_usersController = new InputDevice("none");
    public int m_playerNumber = -1;

    private Vector3 m_move;
    private int m_dodging; // dodging is an int because 0 = not dodging, 1 = dodging, 2 = unable to dodge;
    private int m_attacking; // attacking is an int because 0 = not attacking, 1 = attacking, 2 = unable to attack;
    public bool m_ableToAttack = false;

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
        m_Character = GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
	}

    private void FixedUpdate()
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

        m_Character.Move(m_move, ref m_dodging, ref m_attacking);

    }


    void CoolDownDodge()
    {
        m_dodging = 0;
    }

    void CoolDownAttack()
    {
        m_attacking = 0;
    }

    void CheckKeyboardControls(out float h, out float v)
    {
        h = ((Input.GetKey(KeyCode.A) ? -1 : 0) - (Input.GetKey(KeyCode.D) ? -1 : 0));
        v = ((Input.GetKey(KeyCode.W) ? 1 : 0) - (Input.GetKey(KeyCode.S) ? 1 : 0));

        if (m_attacking == 0 && Input.GetKeyDown(KeyCode.Space))
        {
            m_attacking = 1;
            Invoke("CoolDownAttack", m_attackCooldown);
        }

        if (m_dodging == 0 && Input.GetKeyDown(KeyCode.LeftShift))
        {
            m_dodging = 1;
            Invoke("CoolDownDodge", m_dodgeCooldown);
        }
    }

    void CheckControllerControls(out float h, out float v)
    {
        h = m_usersController.LeftStickX;
        v = m_usersController.LeftStickY;

        if (m_attacking == 0 && m_usersController.Action1.WasPressed)
        {
            m_dodging = 1;
            Invoke("CoolDownAttack", m_attackCooldown);
        }

        if (m_dodging == 0 && m_usersController.Action2.WasPressed)
        {
            m_dodging = 1;
            Invoke("CoolDownDodge", m_dodgeCooldown);
        }
    }

}
