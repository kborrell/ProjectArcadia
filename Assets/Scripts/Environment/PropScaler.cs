using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PropScaler : MonoBehaviour
{
    [SerializeField]
    Transform visualObject;

    [SerializeField]
    float offset;

    [SerializeField]
    [Range(0, 20)]
    float scale;

    private void OnValidate()
    {
        var position = visualObject.transform.position;
        visualObject.transform.position = new Vector3(position.x, scale * offset, (scale * offset) - offset);
        visualObject.transform.localScale = Vector3.one * scale;
    }

}
