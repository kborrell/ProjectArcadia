using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSonar : MonoBehaviour {

    private Character m_character;
    private bool m_chargingSonar;
    private bool m_sonarActive;
    private float m_chargingTime;
    private float m_chargedTimer;
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
                {
					m_chargingSonar = true;
					m_chargedTimer = 0f;
                    if (!m_sonarType)
                        m_character.SetCharacterMovementEnabled(false);
                }
            }

            if(Input.GetKeyUp(KeyCode.Space))
            {
                if (!m_sonarActive)
                {
                    Debug.Log("Stop charging");
                    m_chargingSonar = false;
                    m_chargedTimer = 0f;
					if (!m_sonarType)
						m_character.SetCharacterMovementEnabled(true);
                }
                else
                    StopSonar();
            }

            if (m_chargingSonar)
            {
				Debug.Log("Charging Sonar");
				m_chargedTimer += Time.deltaTime;
                if (m_chargedTimer >= m_chargingTime)
                {
					StartSonar();
					m_chargingSonar = false;
				}
            }
        }
    }

    void OnDestroy()
    {
        CharacterEnergy.OnCharacterDead -= StopOnDead;
    }

    void StartSonar()
    {
        Debug.Log("Sonar charged");
        m_sonarActive = true;
        //Call the sonar UI.

    }

    void StopSonar()
    {
        Debug.Log("Sonar stopped");
		m_sonarActive = false;
		if (!m_sonarType)
            m_character.SetCharacterMovementEnabled(true);
        //Stop the sonar UI.

    }

    void StopOnDead()
    {
        if (!m_sonarActive && m_chargingSonar)
		{
			m_chargingSonar = false;
			m_chargedTimer = 0f;
		}
		else
			StopSonar();
    }
}
