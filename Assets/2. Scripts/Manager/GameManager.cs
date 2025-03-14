using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;

    public static GameManager Instance { get { return gameManager; } }
    //public PlayerController player { get; private set; }

    //UIManager uiManager;

    private void Awake()
    {
        gameManager = this; // 싱글톤 인스턴스로 현재 객체 할당
        //player = FindObjectOfType<PlayerController>();
        //uiManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        // uiManager의 게임 시작 메서드 or 스타트 씬 로드
    }

    void GameOver()
    {
        // uiManager의 게임 종료 메서드
    }
}
