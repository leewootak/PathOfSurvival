using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    public Button StartButton;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    private void Start()
    {
        if (StartButton != null)
        {
            StartButton.onClick.AddListener(OnClickStartButton);
        }
    }

    void OnClickStartButton()
    {
        gameManager.ChangeScene(NowSceneEnum.GamePlaying);
    }
}
