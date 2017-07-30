using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class Character : MonoBehaviour, IPointerClickHandler {

    public enum CharacterType
    {
        Normal,
        Runner,
        Explorer,
        Sonar,
        Drunk,
        Target,
        Soul
    }

    public void SetIsPossessed(bool enable)
    {
        m_movementComponent.SetEnabled(enable);
        if (m_characterIAMovement) m_characterIAMovement.SetEnabled(!enable);
		m_characterVision.enabled = enable;
        m_isPossessed = enable;
    }

    public bool IsPossessed()
    {
        return m_isPossessed;
    }

    public void SetCharacterType(CharacterType type)
    {
        m_characterType = type;
    }

    public CharacterType GetCharacterType()
    {
        return m_characterType;
    }

    public void SetCharacterMovementEnabled(bool enabled)
    {
        m_movementComponent.SetMovementEnabled(enabled);
    }

	public void OnPointerClick(PointerEventData eventData)
	{
		TeleportManager.Instance.ChangeSoul(this);
	}

	void Awake ()
    {
        m_animator = GetComponent<Animator>();
        m_energyComponent = GetComponent<CharacterEnergy>();
        m_movementComponent = GetComponent<CharacterMovement> ();
	    m_characterVision = GetComponent<CharacterVision> ();
        m_sonarComponent = GetComponent<CharacterSonar>();
        m_characterIAMovement = GetComponent<CharacterIAMovement>();

		m_characterVision.enabled = false;
    }

    void Update ()
    {

	}

    [SerializeField] CharacterType m_characterType;

    private CharacterEnergy m_energyComponent;
	public CharacterMovement m_movementComponent { get; private set; }
	public CharacterVision m_characterVision { get; private set; }
    private CharacterSonar m_sonarComponent;
    private CharacterIAMovement m_characterIAMovement;

    private Animator m_animator;
    private bool m_isPossessed = false;
}