using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultVision : CharacterVision {
    
    // Use this for initialization
    public override void Init(Light light)
    {
        light.type = LightType.Point;
        light.range = GetVisionRange();
        light.transform.localPosition = new Vector3(-0.1f, 4.2f, -2.8f);
    }

    
}
