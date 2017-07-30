using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterVision : MonoBehaviour {

    [SerializeField] private float m_visionRange;

    public float GetVisionRange() { return m_visionRange; }
    // initialize character light
    public abstract void Init(Light light);

	protected void SetTunnelMaskEnabled(bool enabled)
	{
		RectTransform rect = GetRect ();
		rect.gameObject.SetActive (enabled);
	}

	protected RectTransform GetRect()
	{
		return UIManager.Instance.m_tunnelMask.GetComponent<RectTransform> ();
	}
}
