using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour {

    public GameObject m_startingCharacter;

    private static TeleportManager instance = null;
    private GameObject m_currentCharacter;
    private float m_maxTeleportZone;
    private bool m_changingSoul;
    [SerializeField] private LineRenderer m_soulParticle;
    private float m_soulChangeDuration;
    private float m_soulSpeed;

    public static TeleportManager GetInstance()
    {
        if(instance == null)
        {
            instance = new TeleportManager();
        }

        return instance;
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        m_currentCharacter = m_startingCharacter;
        m_maxTeleportZone = 10f;
        m_soulSpeed = 5f;
    }

	void Update () 
    {
        if (!m_changingSoul)
            HandleMouseInput();
        else
            DisplaySoulChange();
	}

    void HandleMouseInput()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(mouseRay, out hit))
        {
            if(Input.GetMouseButtonDown(0) && hit.transform.tag == "Character" && Vector3.Distance(m_currentCharacter.transform.position, hit.transform.position) <= m_maxTeleportZone)
            {
				ChangeSoul(hit.transform.gameObject);
            }
        }
    }

    void ChangeSoul(GameObject character)
    {
        if(m_currentCharacter != character)
        {
            m_soulParticle.gameObject.SetActive(true);
            m_soulParticle.transform.position = m_currentCharacter.transform.position;                 
			m_currentCharacter = character;
            m_changingSoul = true;

            Debug.Log("Teleported to " + character.name);
		}
    }

    void DisplaySoulChange()
    {
        m_soulParticle.transform.position = Vector3.MoveTowards(m_soulParticle.transform.position, m_currentCharacter.transform.position, Time.deltaTime * m_soulSpeed);
        m_soulParticle.transform.LookAt(m_currentCharacter.transform);

        if (Vector3.Distance(m_soulParticle.transform.position, m_currentCharacter.transform.position) <= 0.5f)
        {
            m_changingSoul = false;
            m_soulParticle.gameObject.SetActive(false);
        }
    }
}
