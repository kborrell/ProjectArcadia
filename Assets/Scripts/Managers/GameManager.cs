using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    protected override void Awake()
    {
        base.Awake();

        Initialize();
    }

	public bool GameStarted { get; private set; }

    private void Initialize()
    {
		GameStarted = false;

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

		GameStarted = true;

        CharacterEnergy.OnCharacterDead += OnCharacterDead;
    }

    public void EndGame(bool win)
    {
        GameStarted = false;
        CharacterEnergy.OnCharacterDead -= OnCharacterDead;
        CharactersManager.Instance.getPlayerController().TurnOffLights();
        UIManager.Instance.ChangeScreen(win ? UIManager.UIPanelType.WinMenu : UIManager.UIPanelType.EndMenu);
    }

    public void OnCharacterDead()
    {
        EndGame(false);
    }
}
