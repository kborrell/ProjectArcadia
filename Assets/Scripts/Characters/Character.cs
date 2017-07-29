using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public static Character GetActiveCharacter()
    {
        return s_activeCharacter;
    }

    public void SetIsPossessed(bool enable)
    {
        m_isPossessed = enable;
    }

    public float GetCurrentEnergy()
    {
        return m_currentEnergy;
    }

    public bool isDead()
    {
        return m_currentEnergy <= 0;
    }

    private void KillCharacter()
    {
        if (OnCharacterDead != null)
        {
            OnCharacterDead(this);
        }
    }

    private void DecreaseEnergy()
    {
        m_currentEnergy -= m_decreaseRate;

        Debug.Log("CURRENT ENERGY: " + m_currentEnergy);

        if (OnEnergyValueChanged != null)
        {
            OnEnergyValueChanged(m_currentEnergy);
        }
    }

	void Awake ()
    {
        m_animator = GetComponent<Animator>();
        if (m_startActive)
        {
            s_activeCharacter = this;
        }
	}

    private void Start()
    {
        m_currentEnergy = m_initialEnergy;
    }

    void Update ()
    {
		if (m_timeSinceLastUpdate > 1.0f)
        {
            DecreaseEnergy();
            m_timeSinceLastUpdate = 0.0f;
        }

        m_timeSinceLastUpdate += Time.deltaTime;
	}
    
    [SerializeField] private float m_initialEnergy = 100.0f;
    [SerializeField] private float m_decreaseRate = 1.0f;
    private float m_currentEnergy;
    private float m_timeSinceLastUpdate = 0.0f;

    private Animator m_animator;

    [SerializeField] private bool m_startActive = false;
    private bool m_isPossessed;

    private static Character s_activeCharacter;

    public delegate void OnCharacterDeadEvent(Character character);
    public static event OnCharacterDeadEvent OnCharacterDead;

    public delegate void OnEnergyValueChangedEvent(float newValue);
    public static event OnEnergyValueChangedEvent OnEnergyValueChanged;
}