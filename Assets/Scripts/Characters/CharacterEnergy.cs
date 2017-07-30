using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEnergy : MonoBehaviour {

    public float GetCurrentEnergy()
    {
        return m_currentEnergy;
    }

    public bool isDead()
    {
        return m_currentEnergy <= 0;
    }

    public void SetDecreaseRate(float rate)
    {
        m_decreaseRate = rate;
    }

    private void KillCharacter()
    {
		if (OnCharacterDead != null) {
			OnCharacterDead ();
		}
    }

    private void DecreaseEnergy()
    {
        m_currentEnergy -= m_decreaseRate / 100;

        if (m_currentEnergy <= 0)
        {
            KillCharacter();
        }
    }

    void Awake()
    {
        m_character = GetComponent<Character>();
    }

    void Start()
    {
        m_currentEnergy = m_initialEnergy;
    }

    void Update()
    {
        if (m_character.IsPossessed() && m_timeSinceLastUpdate > 0.01f)
        {
            DecreaseEnergy();
            m_timeSinceLastUpdate = 0.0f;
        }

        if (m_character.IsPossessed() && OnEnergyValueChanged != null)
        {
            OnEnergyValueChanged(m_currentEnergy, m_initialEnergy);
        }

        m_timeSinceLastUpdate += Time.deltaTime;
    }

    private Character m_character;

    [SerializeField] private float m_initialEnergy = 100.0f;
    [SerializeField] private float m_decreaseRate = 1.0f;

    private float m_currentEnergy;
    private float m_timeSinceLastUpdate = 0.0f;

    public delegate void OnCharacterDeadEvent();
    public static event OnCharacterDeadEvent OnCharacterDead;

    public delegate void OnEnergyValueChangedEvent(float newValue, float maxValue);
    public static event OnEnergyValueChangedEvent OnEnergyValueChanged;
}
