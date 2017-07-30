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

#if UNITY_EDITOR
    private void OnValidate()
    {
        visualObject.transform.localPosition = new Vector3(0, scale * offset, (scale * offset) - offset);
        visualObject.transform.localScale = Vector3.one * scale;
    }
#endif
}
