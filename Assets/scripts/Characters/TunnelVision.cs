using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelVision : CharacterVision {

    [SerializeField] private float m_tunnelAngle;
    private Light m_light;
    // Use this for initialization
    public override void Init(Light light)
    {
        m_light = light;

        m_light.type = LightType.Spot;
        m_light.range = GetVisionRange();
        m_light.spotAngle = m_tunnelAngle;
        m_light.transform.localPosition = new Vector3(-0.1f, -2.4f, -2.9f);
        m_light.transform.localEulerAngles = new Vector3(-50, 0, 0);
    }
}
