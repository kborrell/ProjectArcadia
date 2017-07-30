using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIIntroPanel : UIPanel {

    public override void ShowPanel(OnShowAnimationFinishedCallback callback)
    {
        for (int i = 0; i < m_texts.Count; i++)
        {
            m_texts[i].DOFade(0.0f, 0.0f);
        }

        gameObject.SetActive(true);

        if (callback != null)
        {
            callback();
        }

        StartCoroutine(ShowIntroTexts());
    }

    public override void HidePanel(OnHideAnimationFinishedCallback callback)
    {
        StartCoroutine(HideCoroutine(callback));
    }

    IEnumerator ShowIntroTexts()
    {
        for (int i = 0; i < m_texts.Count; i++)
        {
            m_texts[i].DOFade(1.0f, 1.0f);
            yield return new WaitForSeconds(2.0f);
        }

        GameManager.Instance.StartGame();
    }

    IEnumerator HideCoroutine(OnHideAnimationFinishedCallback callback)
    {
        for (int i=0; i < m_texts.Count; i++)
        {
            m_texts[i].DOFade(0.0f, 0.5f).OnComplete(() =>
            {
                m_texts[i].gameObject.SetActive(false);
            });
        }

        yield return new WaitForSeconds(1.5f);

        gameObject.SetActive(false);

        if (callback != null)
        {
            callback();
        }
    }

    public void StartGameButtonPressed()
    {
        UIManager.Instance.ChangeScreen(UIManager.UIPanelType.Intro);
    }

    [SerializeField] private List<Text> m_texts;
}
