using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIGameplayPanel : UIPanel
{
    public override void ShowPanel(OnShowAnimationFinishedCallback callback)
    {
        base.ShowPanel(callback);

        CharacterEnergy.OnEnergyValueChanged += UpdateProgressBar;
    }

    public override void HidePanel(OnHideAnimationFinishedCallback callback)
    {
        base.HidePanel(callback);

        CharacterEnergy.OnEnergyValueChanged -= UpdateProgressBar;
    }

    public void EnableTargetDetection()
    {
        StartCoroutine(sonarController.PlaySonarAnimation());
    }

    public void DisableTargetDetection()
    {

    }

    private void UpdateProgressBar(float currentValue, float maxValue)
    {
        float percentage = (currentValue / maxValue);
        if (percentage > m_progressBar.fillAmount)
        {
            m_progressBar.DOKill();
            m_progressBar.fillAmount = percentage;
        }
        else
        {
            m_progressBar.DOFillAmount(percentage, 1.0f).SetEase(Ease.Linear);
        }
    }

    [SerializeField]
    private SonarController sonarController;

    [SerializeField]
    private Image m_progressBar;
}
