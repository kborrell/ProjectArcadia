﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    protected override void Awake()
    {
        base.Awake();

        Time.timeScale = 1.0f;

        Initialize();
    }

	public bool GameStarted { get; set; }

    private void Initialize()
    {
		GameStarted = false;

        UIManager.Instance.Initialize();
        CameraController.Instance.Initialize();
        TeleportManager.Instance.Initialize();
        CharactersManager.Instance.Initialize();

        UIManager.Instance.ChangeScreen(UIManager.UIPanelType.MainMenu);

        TeleportManager.Instance.enabled = false;
    }

    public void StartGame()
    {
        UIManager.Instance.ChangeScreen(UIManager.UIPanelType.Gameplay);
        CharactersManager.Instance.getPlayerController().TurnOnLights();

        CharacterEnergy.OnCharacterDead += OnCharacterDead;

        StartCoroutine(EnableTeleport());

    }

    public void EndGame(bool win)
    {
        GameStarted = false;
        CharacterEnergy.OnCharacterDead -= OnCharacterDead;
        StartCoroutine(EndGameMenu(win));
    }

    private IEnumerator EndGameMenu(bool win)
    {
        yield return new WaitForSeconds(1.3f);
        CharactersManager.Instance.getPlayerController().TurnOffLights();
        UIManager.Instance.ChangeScreen(win ? UIManager.UIPanelType.WinMenu : UIManager.UIPanelType.EndMenu);
    }

    public void OnCharacterDead()
    {
        EndGame(false);
    }

    IEnumerator EnableTeleport()
    {
        yield return new WaitForSeconds(2f);
        TeleportManager.Instance.enabled = true;
    }
}
