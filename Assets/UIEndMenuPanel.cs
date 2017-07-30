using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIEndMenuPanel : UIPanel {

    public override void ShowPanel(OnShowAnimationFinishedCallback callback)
    {
        m_titleText.DOFade(0.0f, 0.0f);
        m_restartButton.gameObject.SetActive(false);

        gameObject.SetActive(true);

        if (callback != null)
        {
            callback();
        }

        m_titleText.DOFade(1.0f, 0.5f).OnComplete(() =>
        {
            m_restartButton.gameObject.SetActive(true);
        });     
    }

    public void RestartGameButtonPressed()
    {
        Application.LoadLevel(0);
    }

    [SerializeField] private Text m_titleText;
    [SerializeField] private Button m_restartButton;
}
