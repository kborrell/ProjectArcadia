using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelVision : CharacterVision {

    [SerializeField] private float m_tunnelAngle;

    // Use this for initialization
    public override void Init(Light light)
    {
//		light.type = LightType.Point;
//
//		light.type = LightType.Spot;
//		light.range = GetVisionRange();
//		light.spotAngle = m_tunnelAngle;
//		light.transform.localPosition = new Vector3(-0.1f, -2.4f, -2.9f);
//		light.transform.localEulerAngles = new Vector3(-50, 0, 0);
        	
		DefaultVision:Init (light);

		SetTunnelMaskEnabled (true);
    }

	void Update()
	{
		
		Vector3 direction = CharactersManager.Instance.getPlayerController ().controlledCharacter.m_movementComponent.GetFacingDirection ();

		float angle = Vector3.Angle (Vector3.up, direction);

		GetRect().transform.rotation = Quaternion.EulerAngles(new Vector3(0,0,angle));
	}
}
