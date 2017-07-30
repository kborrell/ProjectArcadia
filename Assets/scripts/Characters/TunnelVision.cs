using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelVision : CharacterVision {

    [SerializeField] private float m_tunnelAngle;

	RectTransform rect;
    // Use this for initialization
    public override void Init(Light light)
    {
		light.type = LightType.Point;
		light.range = GetVisionRange();
		light.transform.localPosition = new Vector3(-0.1f, 3.25f, -3.8f);

		SetTunnelMaskEnabled (true);
    }

	void Update()
	{
		Vector3 direction = CharactersManager.Instance.getPlayerController ().controlledCharacter.m_movementComponent.GetFacingDirection ();

		float angle = Vector3.Angle (Vector2.right, direction);

		rect.transform.rotation = Quaternion.EulerAngles(new Vector3(0,0,angle));
	}
}
