using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour {

    [SerializeField]
    private SonarController sonarController;

    public virtual void ShowPanel(OnShowAnimationFinishedCallback callback)
    {
        gameObject.SetActive(true);   

        if (callback != null)
        {
            callback();
        }
    }

    public virtual void HidePanel(OnHideAnimationFinishedCallback callback)
    {
        gameObject.SetActive(false);

        if (callback != null)
        {
            callback();
        }
    }

    public void EnableTargetDetection()
    {
        sonarController.gameObject.SetActive(true);
    }

    public void DisableTargetDetection()
    {
        sonarController.gameObject.SetActive(false);
    }

    public delegate void OnShowAnimationFinishedCallback();
    public delegate void OnHideAnimationFinishedCallback();
}
