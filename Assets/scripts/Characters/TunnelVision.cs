using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelVision : CharacterVision {

    // Use this for initialization
    public override void Init(Light light)
    {
		light.type = LightType.Point;
		light.range = GetVisionRange();
		light.transform.localPosition = new Vector3(-0.1f, 4.2f, -2.8f);

		SetTunnelMaskEnabled (true);
    }

	void Update()
	{
		
		Vector3 direction = CharactersManager.Instance.getPlayerController ().controlledCharacter.m_movementComponent.GetFacingDirection ();

		float angle = Vector3.Angle (Vector3.forward, direction);

		if (direction.x > 0) 
		{
			angle *= -1;
		}
			
		GetRect().transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
	}
}
