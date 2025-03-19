using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum NowSceneEnum
{
    Start,
    GamePlaying,
    End
}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return _instance;
        }
    }
    public PlayerController player { get; private set; }

    UIManager uiManager;

    public NowSceneEnum nowScene;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this; // 싱글톤 인스턴스로 현재 객체 할당
            //player = FindObjectOfType<PlayerController>();
            uiManager = FindObjectOfType<UIManager>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        // uiManager의 게임 시작 메서드 or 스타트 씬 로드
        nowScene = NowSceneEnum.Start;
    }

    void GameOver()
    {
        // uiManager의 게임 종료 메서드
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }

    public void ChangeScene(NowSceneEnum ToChange)
    {
        if(nowScene == ToChange) return;

        nowScene = ToChange;
        switch (nowScene)
        {
            case NowSceneEnum.Start:
                SceneManager.LoadScene("StartScene");
                break;
            case NowSceneEnum.GamePlaying:
                SceneManager.LoadScene("TestScene");
                break;
            case NowSceneEnum.End:
                GameOver();
                break;
        }
    }
}
