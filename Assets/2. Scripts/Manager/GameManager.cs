using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;

    public static GameManager Instance { get { return gameManager; } }

    //UIManager uiManager;

    private void Awake()
    {
        gameManager = this; // 싱글톤 인스턴스로 현재 객체 할당
        //uiManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        Time.timeScale = 0f;
        //
    }

    void Update()
    {
        
    }

    void GameOver()
    {

    }

    void Restart()
    {

    }
}
