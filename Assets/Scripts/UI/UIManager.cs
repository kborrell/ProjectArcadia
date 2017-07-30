using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    [Serializable]
    public enum UIPanelType
    {
        None,
        MainMenu,
        Intro,
        Gameplay,
        Sonar,
        WinMenu,
        EndMenu
    }

    [Serializable]
    public struct UIPanelInfo
    {
        public UIPanelType type;
        public UIPanel panel;
    }

    public void Initialize()
    {
        for (int i=0; i < m_panelList.Count; i++)
        {
            m_panelList[i].panel.gameObject.SetActive(false);
            m_panels[m_panelList[i].type] = m_panelList[i].panel;
        }
    }

    public bool IsDebugEnabled()
    {
        return m_debug;
    }

    public void ChangeScreen(UIPanelType panelType)
    {
        UIPanel panel = m_panels[panelType];

        if (m_currentPanel != null)
        {
            HidePanel(m_currentPanel, () => {
                ShowPanel(panel, null);
            });
        }
       else
        {
            ShowPanel(panel, null);
        }
    }

    public void HidePanel(UIPanel panel, UIPanel.OnHideAnimationFinishedCallback callback)
    {
        panel.HidePanel(callback);
    }

    public  void ShowPanel(UIPanel panel, UIPanel.OnShowAnimationFinishedCallback callback)
    {
        m_currentPanel = panel;
        panel.ShowPanel(callback);
    }

    private UIPanel m_currentPanel;

    [SerializeField] private List<UIPanelInfo> m_panelList;
    [SerializeField] private Dictionary<UIPanelType, UIPanel> m_panels = new Dictionary<UIPanelType, UIPanel>();

    [SerializeField] bool m_debug;

	public GameObject m_tunnelMask;
}
