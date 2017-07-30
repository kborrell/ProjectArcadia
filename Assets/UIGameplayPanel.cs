using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIGameplayPanel : UIPanel
{
    public void EnableTargetDetection()
    {
        sonarController.gameObject.SetActive(true);
    }

    public void DisableTargetDetection()
    {
        sonarController.gameObject.SetActive(false);
    }

    [SerializeField]
    private SonarController sonarController;
}
