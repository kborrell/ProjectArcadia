using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    [SerializeField]
    PanelLoader menuLoader, modalLoader, overlayLoader;

    BaseRaycaster rayCast;

    Dictionary<Type, PanelBase> menu = new Dictionary<Type, PanelBase>();
    Dictionary<Type, PanelBase> modal = new Dictionary<Type, PanelBase>();
    Dictionary<Type, PanelBase> overlay = new Dictionary<Type, PanelBase>();
    Stack<PanelBase> activeHistory = new Stack<PanelBase>(4);
    Stack<PanelBase> activeModals = new Stack<PanelBase>(4);

    public const float s_uiTime = 0.3f;
    public readonly Color s_uiFailColor = new Color(1f, 0f, 0f, 1f);
    public readonly Color s_uiDefaultColor = new Color(0.07f, 0.22f, 0.47f, 0.85f);

    public bool ModalsActive { get { return modalLoader.ActivePanels > 0; } }

    public void Initialize()
    {
        rayCast = GetComponent<BaseRaycaster>();
        DontDestroyOnLoad(this);

        menu = menuLoader.LoadPanels();
        modal = modalLoader.LoadPanels();
        overlay = overlayLoader.LoadPanels();

        if (Screen.dpi > 350f)
            EventSystem.current.pixelDragThreshold = EventSystem.current.pixelDragThreshold * 10;
    }

    public T OpenMenu<T>() where T : PanelBase
    {
        PanelBase panel = GetMenu<T>();
        if (panel != null && !panel.IsActive)
        {
            if (activeHistory.Count > 0)
                activeHistory.Peek().Finish();
            activeHistory.Push(panel);
            panel.Init();
        }

        return (T)panel;
    }

    public void SetInput(bool enabled)
    {
        rayCast.enabled = enabled;
    }

    public void CloseMenu()
    {
        if (activeHistory.Count > 0)
        {
            activeHistory.Pop().Finish();

            if (activeHistory.Count > 0)
            {
                activeHistory.Peek().Init();
            }
        }
    }

    public T OpenModal<T>() where T : PanelBase
    {
        PanelBase panel = GetModal<T>();
        if (panel != null && !panel.IsActive)
        {
            panel.Init();
            activeModals.Push(panel);
        }
        return (T)panel;
       
    }

    public void CloseModal<T>() where T : PanelBase
    {
        PanelBase panel = GetModal<T>();
        if (panel != null && panel.IsActive)
        {
            panel.Finish();
        }
    }

    public bool CloseModal()
    {
        if (activeModals.Count > 0)
        {
            var modal = activeModals.Pop();
            if (modal.IsActive)
            {
                modal.Finish();
                return true;
            }
            else
            {
                return CloseModal();
            }
        }
        else
        {
            return false;
        }
    }

    public void ResetHistory()
    {
        if (activeHistory.Count > 0)
        {
            var panel = activeHistory.Pop();
            activeHistory.Clear();
            activeHistory.Push(panel);
        }
    }

    /// <summary>
    /// Gets specific menu panel.
    /// </summary>
    /// <returns>menu panel.</returns>
    /// <typeparam name="T">Menu type.</typeparam>
    public T GetMenu<T>() where T : PanelBase
    {
        PanelBase modal;
        if (menu.TryGetValue(typeof(T), out modal))
        {
            return (T)modal;
        }

        return null;
    }

    /// <summary>
    /// Gets specific modal panel.
    /// </summary>
    /// <returns>hud panel.</returns>
    /// <typeparam name="T">Menu type.</typeparam>
    public T GetModal<T>() where T : PanelBase
    {
        PanelBase panel;
        if (modal.TryGetValue(typeof(T), out panel))
        {
            return (T)panel;
        }

        return null;
    }

    /// <summary>
    /// Gets specific overlay panel.
    /// </summary>
    /// <returns>hud panel.</returns>
    /// <typeparam name="T">Menu type.</typeparam>
    public T GetOverlay<T>() where T : PanelBase
    {
        PanelBase panel;
        if (overlay.TryGetValue(typeof(T), out panel))
        {
            return (T)panel;
        }

        return null;
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        ProcessBackButton();
    //    }
    //}

    public void ProcessBackButton()
    {
        if (CloseModal())
        {
            return;
        }
        else if (activeHistory.Count > 1)
        {
            CloseMenu();
        }
    }
}