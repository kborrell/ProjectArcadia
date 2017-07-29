using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public enum CharacterType
    {
        Normal,
        Runner,
        Explorer,
        Sonar,
        Drunk,
        Target
    }

    public void SetIsPossessed(bool enable)
    {
		m_movementController.enabled = enable;
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

	void Awake ()
    {
        m_animator = GetComponent<Animator>();
        m_energyComponent = GetComponent<CharacterEnergy>();
		m_movementController = GetComponent<MovementController> ();
		m_characterVision = GetComponent<CharacterVision> ();

    }

    void Update ()
    {

	}

    [SerializeField] CharacterType m_characterType;

    private CharacterEnergy m_energyComponent;
	private MovementController m_movementController;
	public CharacterVision m_characterVision { get; private set; }

    private Animator m_animator;
    private bool m_isPossessed = false;
}