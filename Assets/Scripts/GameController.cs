using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject m_playerPrefab;
    public Material[] m_playerTextures;
    public GameObject[] m_playerIcons;
    public GameObject m_boulderPrefab;
    [Range(1, 2)] public int m_amountOfPlayers = 1;

    public Vector3 m_playerSpawnOffset;
    public Vector3 m_boulderSpawnOffset;
    public Vector3 m_boulderStartingVelocity;
    public float m_boulderSpawnDelay = 10;
    public int m_pointValueOf1Gem;
    public int m_pointValueOf1Second;

    [HideInInspector]
    public float m_secondsPassed;
    [HideInInspector]
    public Dictionary<int, int> m_amountOfGems = new Dictionary<int, int>();

    [HideInInspector] public List<GameObject> m_listOfPlayers = new List<GameObject>();

	// Use this for initialization
	void Awake () {
        m_secondsPassed = 0;
        //create the correct amount of players at the start of the game

		for (int i = 0; i < m_amountOfPlayers; i++)
        {
            if (m_amountOfPlayers % 2 != 0)
            {
                if (i == (m_amountOfPlayers-1)/2)
                {
                    CreatePlayer(0);
                }
            }
            else
            {
                CreatePlayer(i);
            }
        }

        Invoke("SpawnBoulderAfterFalling", m_boulderSpawnDelay);
    }

    void SpawnBoulderAfterFalling()
    {
        CreateBoulder();
        AddPlayersToBoulderTargets();
        for (int i = 0; i < m_listOfPlayers.Count; i ++)
        {
            m_listOfPlayers[i].GetComponent<PlayerInput>().enabled = true;
        }
    }

    private void CreateBoulder()
    {
        GameObject newBoulder = Instantiate(m_boulderPrefab, m_boulderSpawnOffset, Quaternion.identity);
        newBoulder.GetComponent<Rigidbody>().AddForce(m_boulderStartingVelocity, ForceMode.Impulse);
        // the next two lines of code fix a bug where the boulder gains an unrealistic amount of speed downward
        newBoulder.GetComponent<BounceInternaly>().enabled = false;
        Invoke("EnableBoulderBouncing", 2);
    }

    private void CreatePlayer(int i)
    {
        bool evenNumber = (m_amountOfPlayers % 2 > 0)? false : true;
        float xpos = i - m_amountOfPlayers / 2;
        if (evenNumber)
            xpos += 0.5f;
        GameObject newPlayer = Instantiate(m_playerPrefab, new Vector3(xpos, 1, 0) + m_playerSpawnOffset, Quaternion.identity);
        Instantiate(m_playerIcons[i], newPlayer.transform);
        m_listOfPlayers.Add(newPlayer);
        newPlayer.transform.Find("Glen").Find("gladiator_lowpoly:Mesh").gameObject.GetComponent<Renderer>().material = m_playerTextures[i];
        PlayerInput pi = newPlayer.GetComponent<PlayerInput>();
        pi.PlayerNumber = i;
        pi.enabled = false;
        m_amountOfGems.Add(i, 0);
        // fix floating player
        newPlayer.GetComponent<BounceInternaly>().enabled = false;
        Invoke("EnablePlayerBouncing", 2);
    }

    void EnablePlayerBouncing()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<BounceInternaly>().enabled = true;
        }
    }

    void EnableBoulderBouncing()
    {
        GameObject[] boulders = GameObject.FindGameObjectsWithTag("Boulder");
        for (int i = 0; i < boulders.Length; i ++)
        {
            boulders[i].GetComponent<BounceInternaly>().enabled = true;
        }
    }

    private void AddPlayersToBoulderTargets()
    {
        BounceInternaly boulderBouncingScript = GameObject.FindGameObjectWithTag("Boulder").GetComponent<BounceInternaly>();
        foreach (GameObject player in m_listOfPlayers)
            boulderBouncingScript.m_objectsToBounceTowards.Add(player);
    }

    private void Update()
    {
        if (!GetComponent<EndGameCondition>().m_gameOver)
            m_secondsPassed += Time.deltaTime;
    }
}
