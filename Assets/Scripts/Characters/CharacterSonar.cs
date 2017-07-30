using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSonar : MonoBehaviour {

    private Character m_character;
    private bool m_chargingSonar;
    private bool m_sonarActive;
    private float m_chargingTime;
    private bool m_sonarType;

    void Start()
    {
        m_character = GetComponent<Character>();

        m_sonarType = (m_character.GetCharacterType() == Character.CharacterType.Sonar);
        m_chargingTime = (m_sonarType)? 0f : 2f;
		CharacterEnergy.OnCharacterDead += StopOnDead;
	}

    void Update()
    {
        if(m_character.IsPossessed())
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if (!m_sonarActive && !m_chargingSonar)
                    StartCoroutine(StartSonar());
            }

            if(Input.GetKeyUp(KeyCode.Space))
            {
                if (m_chargingSonar && !m_sonarActive)
                    StopCoroutine(StartSonar());
                else if (m_sonarActive)
                    StopSonar();
            }
        }
    }

    void OnDestroy()
    {
        CharacterEnergy.OnCharacterDead -= StopOnDead;
    }

    IEnumerator StartSonar()
    {
        Debug.Log("Charging Sonar");
        m_chargingSonar = true;
        if (!m_sonarType)
            m_character.SetCharacterMovementEnabled(false);
		yield return new WaitForSeconds(m_chargingTime);
        Debug.Log("Sonar charged");
        m_sonarActive = true;
        //Call the sonar UI.
        UIManager.Instance.GetPanel(UIManager.UIPanelType.Gameplay).EnableTargetDetection();
    }

    void StopSonar()
    {
        Debug.Log("Sonar stopped");
        if (!m_sonarType)
            m_character.SetCharacterMovementEnabled(true);
		m_chargingSonar = false;
		m_sonarActive = false;
        //Stop the sonar UI.
        UIManager.Instance.GetPanel(UIManager.UIPanelType.Gameplay).DisableTargetDetection();
    }

    void StopOnDead()
    {
        if (m_chargingSonar && !m_sonarActive)
            StopCoroutine(StartSonar());
        else if (m_sonarActive)
            StopSonar();
    }
}
