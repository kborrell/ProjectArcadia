using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIIntroPanel : UIPanel
{

    [SerializeField]
    private List<Text> m_texts;

    int m_textsCount;
    int m_currentText;
    Tweener[] m_tweeners;
    IEnumerator m_currentEnumerator;

    void Awake()
    {
        int textsCount = m_texts.Count;
        m_tweeners = new Tweener[textsCount];
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var tween = m_tweeners[m_currentText];
            if (tween != null)
            {
                tween.Complete();
                m_currentEnumerator.MoveNext();
            }
        }
    }

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

        if (UIManager.Instance.IsDebugEnabled())
        {
            GameManager.Instance.StartGame();
        }
        else
        {
            m_currentEnumerator = ShowIntroTexts();
            StartCoroutine(m_currentEnumerator);
        }
    }

    public override void HidePanel(OnHideAnimationFinishedCallback callback)
    {
        if (gameObject.activeSelf)
        {
            //m_currentEnumerator = HideCoroutine(callback);
            //StartCoroutine(m_currentEnumerator);
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

    IEnumerator ShowIntroTexts()
    {
        for (int i = 0; i < m_texts.Count; i++)
        {
            m_currentText = i;
            m_tweeners[i] = m_texts[i].DOFade(1.0f, 1.0f);
            yield return new WaitForSeconds(2.0f);
        }

        GameManager.Instance.StartGame();
    }

    IEnumerator HideCoroutine(OnHideAnimationFinishedCallback callback)
    {
        for (int i = 0; i < m_texts.Count; i++)
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

}
