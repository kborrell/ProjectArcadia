using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIGameplayPanel : UIPanel
{
    public void EnableTargetDetection()
    {
        StartCoroutine(sonarController.PlaySonarAnimation());
    }

    public void DisableTargetDetection()
    {

    }

    [SerializeField]
    private SonarController sonarController;
}
