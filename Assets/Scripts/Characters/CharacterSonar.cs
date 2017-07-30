using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSonar : MonoBehaviour {

    [SerializeField]
    private Image m_sonarIndicator;

    [SerializeField]
    private Image m_sonarIndicatorBackground;

    [SerializeField]
    private Image m_arrowParent;

    [SerializeField]
    private Image m_arrow;

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
            if (m_chargingSonar)
            {
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    StopChargingSonar();
                }
                else
                {
                    m_sonarIndicator.fillAmount = (m_chargedTimer / m_chargingTime);

                    m_chargedTimer += Time.deltaTime;
                    if (m_chargedTimer >= m_chargingTime)
                    {
                        StartSonar();
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StarChargeSonar();
                }
            }
        }
        
        if (m_arrowParent != null)
        {
            m_arrowParent.transform.LookAt(CharactersManager.Instance.getTargetCharacter().transform);
        }
    }

    void OnDestroy()
    {
        CharacterEnergy.OnCharacterDead -= StopOnDead;
    }

    void StartSonar()
    {
        Debug.Log("Sonar charged");

        //m_sonarIndicator.transform.parent.gameObject.SetActive(false);
        m_sonarActive = true;
        m_chargingSonar = false;
        m_chargedTimer = 0f;
        if (!m_sonarType)
            m_character.SetCharacterMovementEnabled(true);

        StopChargingSonar();
        //(UIManager.Instance.GetPanel(UIManager.UIPanelType.Gameplay) as UIGameplayPanel).DisableTargetDetection();
    }

    void StarChargeSonar()
    {
        Debug.Log("Sonar charging started");

        //m_sonarIndicator.transform.parent.gameObject.SetActive(true);
        m_chargingSonar = true;
        m_sonarActive = false;
        m_chargedTimer = 0f;
        if (!m_sonarType)
            m_character.SetCharacterMovementEnabled(false);
        
        StartCoroutine(DoImageFade(true));
    }

    void StopChargingSonar()
    {
        Debug.Log("Sonar charging stopped");

        //m_sonarIndicator.transform.parent.gameObject.SetActive(false);
        m_chargingSonar = false;
        m_sonarActive = false;
        m_chargedTimer = 0f;
        if (!m_sonarType)
            m_character.SetCharacterMovementEnabled(true);

        m_arrow.color = new Color(m_arrow.color.r, m_arrow.color.g, m_arrow.color.b, 1f);

        StartCoroutine(DoImageFade(false));
    }

    void StopOnDead()
    {
        if (!m_sonarActive && m_chargingSonar)
		{
			m_chargingSonar = false;
			m_chargedTimer = 0f;
		}
		else
            StopChargingSonar();
    }

    public IEnumerator DoImageFade(bool fadeIn)
    {
        if(!fadeIn)
            m_arrow.DOColor(new Color(m_arrow.color.r, m_arrow.color.g, m_arrow.color.b, 0f), 3.5f);
        
        m_sonarIndicator.DOColor(new Color(m_sonarIndicator.color.r, m_sonarIndicator.color.g, m_sonarIndicator.color.b, fadeIn ? 1f : 0f), 0.75f);
        yield return m_sonarIndicatorBackground.DOColor(new Color(m_sonarIndicatorBackground.color.r, m_sonarIndicatorBackground.color.g, m_sonarIndicatorBackground.color.b, fadeIn ? 1f : 0f), 0.75f).WaitForCompletion();
    }
}
