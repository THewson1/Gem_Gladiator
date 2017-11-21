using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class Sword : PowerupOrDebuff
{

    public GameObject m_sword = null;
    private InputDevice m_usersController = new InputDevice("none");
    public float m_animationLength = 1;
    public float m_attackForce = 100;
    public int m_amountOfGemsToSpawn = 20;
    public GameObject m_gemPrefab;
    public float m_gemVelocity = 5;

    private bool m_attacking = false;
    private PlayerInput m_playerInput;
    private AudioSource m_audioSource;

    private PlayerDeathLogic m_playerDeathLogic;

    public override void Initialize()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_playerInput = GetComponent<PlayerInput>();
        if (InputManager.Devices.Count > 0)
            m_usersController = InputManager.Devices[m_playerInput.m_playerNumber];
        // add sword to character
        foreach (GameObject child in FindChildren(transform))
        {
            if (child.CompareTag("Sword"))
                m_sword = child;
        }
        if (m_sword != null)
            m_sword.SetActive(true);
    }

    List<GameObject> FindChildren(Transform parent)
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in parent)
        {
            children.Add(child.gameObject);
            foreach (GameObject child2 in FindChildren(child))
                children.Add(child2);
        }
        return children;
    }

    public override void Uninitialize()
    {
        // remove sword from character
        if (m_sword != null)
        m_sword.SetActive(false);

        m_playerDeathLogic.m_invincible = false;
        Destroy(this);
    }

    void Update()
    {
        // if attack button is pressed activate sword
        if (m_usersController.Action1)
        {
            Attack();
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
#endif

    }

    void Attack()
    {
        if (m_audioSource)
            m_audioSource.Play();
        m_playerDeathLogic = GetComponent<PlayerDeathLogic>();
        m_playerDeathLogic.m_invincible = true;
        m_attacking = true;
        Invoke("Uninitialize", m_animationLength);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (m_attacking && collision.gameObject.CompareTag("Boulder"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            Vector3 directionToMove = (collision.transform.position -  transform.position).normalized;
            rb.AddForce(directionToMove * m_attackForce, ForceMode.Impulse);

            SpawnGems(collision.gameObject);

            Invoke("Uninitialize", 1);
        }
    }

    void SpawnGems(GameObject boulder)
    {
        for (int i = 0; i < m_amountOfGemsToSpawn; i ++)
        {
            GameObject gem = Instantiate(m_gemPrefab, boulder.transform.position, Quaternion.identity);
            gem.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-m_gemVelocity, m_gemVelocity), m_gemVelocity * 2, Random.Range(-m_gemVelocity, m_gemVelocity));
        }
    }
}
