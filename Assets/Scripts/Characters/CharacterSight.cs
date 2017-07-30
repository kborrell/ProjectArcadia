using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSight : MonoBehaviour {

	[SerializeField]
	private float _sightRange = 6f;
	[SerializeField]
	private float _sightAngle = 60f;
	[SerializeField]
	private CharacterIAMovement charMov;

	public bool m_hasSightOfPlayer { get; private set; }

	
	// Use this for initialization
	void Start ()
	{
		if(!charMov)
			charMov = GetComponent<CharacterIAMovement>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		m_hasSightOfPlayer = false;
		Character playerCharacter = CharactersManager.Instance.getPlayerController().controlledCharacter;
		if (playerCharacter && Vector3.Distance(playerCharacter.transform.position, transform.position) < _sightRange)
		{
			Vector3 facing = charMov.GetFacingDirection();
			Vector3 charToPlayer = playerCharacter.transform.position - transform.position;
			if (Mathf.Abs(Vector3.Angle(charToPlayer.normalized, facing)) < _sightAngle)
			{
				m_hasSightOfPlayer = true;
			}
		}
	}
}
