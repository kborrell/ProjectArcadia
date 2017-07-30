﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    protected override void Awake()
    {
        base.Awake();

        Initialize();
    }

    private void Initialize()
    {
        UIManager.Instance.Initialize();
        CameraController.Instance.Initialize();
        TeleportManager.Instance.Initialize();
        CharactersManager.Instance.Initialize();

        UIManager.Instance.ChangeScreen(UIManager.UIPanelType.MainMenu);
    }

    public void StartGame()
    {
        UIManager.Instance.ChangeScreen(UIManager.UIPanelType.Gameplay);
        CharactersManager.Instance.getPlayerController().TurnOnLights();
    }
}
