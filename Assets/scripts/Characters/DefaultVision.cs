using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultVision : CharacterVision {
    
    // Use this for initialization
    public override void Init(Light light)
    {
        light.type = LightType.Point;
        light.range = GetVisionRange();
        light.transform.localPosition = new Vector3(-0.1f, 3.25f, -3.8f);
    }

    
}
