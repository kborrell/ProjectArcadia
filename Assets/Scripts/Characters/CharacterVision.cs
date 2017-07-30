using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterVision : MonoBehaviour {

    [SerializeField] private float m_visionRange;

    public float GetVisionRange() { return m_visionRange; }
    // initialize character light
    public abstract void Init(Light light);
}
