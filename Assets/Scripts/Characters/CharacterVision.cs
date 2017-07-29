using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVision : MonoBehaviour {

	[SerializeField] private float visionRange;

	public float GetVisionRange()
	{
		return visionRange;
	}
}
