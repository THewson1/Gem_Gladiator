using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : PowerupOrDebuff
{

    public float m_speedMultiplier = 0.5f;
    [Tooltip("used for finding the player's correct speed")]
    public GameObject m_playerPrefab;

    private float m_originalPlayerSpeed;

    public override void Initialize()
    {
        PlayerController playerController = m_playerPrefab.GetComponent<PlayerController>();
        m_originalPlayerSpeed = playerController.m_movementSpeed;
        GetComponent<PlayerController>().m_movementSpeed = m_originalPlayerSpeed * m_speedMultiplier;
    }

    public override void Uninitialize()
    {
        GetComponent<PlayerController>().m_movementSpeed = m_originalPlayerSpeed;
    }
}