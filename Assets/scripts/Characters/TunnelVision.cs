using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelVision : CharacterVision {

    [SerializeField] private float m_tunnelAngle;
    // Use this for initialization
    public override void Init(Light light)
    {
        light.type = LightType.Spot;
        light.range = GetVisionRange();
        light.spotAngle = m_tunnelAngle;
        light.transform.localPosition = new Vector3(-0.1f, -2.4f, -2.9f);
        light.transform.localEulerAngles = new Vector3(-50, 0, 0);
    }
}
