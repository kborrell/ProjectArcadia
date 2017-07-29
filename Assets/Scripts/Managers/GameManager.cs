using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    protected override void Awake()
    {
        base.Awake();

        UIManager.Instance.Initialize();
        CameraController.Instance.Initialize();
		CharactersManager.Instance.Initialize ();
    }
}
