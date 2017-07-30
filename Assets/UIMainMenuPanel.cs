using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIMainMenuPanel : UIPanel {

    public override void HidePanel(OnHideAnimationFinishedCallback callback)
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(HideCoroutine(callback));
        }
        else
        {
            gameObject.SetActive(false);
            if (callback != null)
            {
                callback();
            }
        }
    }

    IEnumerator HideCoroutine(OnHideAnimationFinishedCallback callback)
    {
        m_startButton.gameObject.SetActive(false);
        m_titleText.DOFade(0.0f, 0.5f);

        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
        m_titleText.DOFade(1.0f, 0.0f);

        if (callback != null)
        {
            callback();
        }
    }

    public void StartGameButtonPressed()
    {
        UIManager.Instance.ChangeScreen(UIManager.UIPanelType.Intro);
    }

    [SerializeField] private Text m_titleText;
    [SerializeField] private Button m_startButton;
}
