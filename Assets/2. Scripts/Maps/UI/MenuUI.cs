using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public GameObject MenuUIGameObject;
    public GameObject PauseUIGameObject;

    public RectTransform Radio;
    public float RadioSpeed;

    public AudioMixer MasterAudioMixer;
    public Scrollbar VolumeSlider;
    public Button RestartButton;
    public Button OffGameButton;
    public Button PauseButton;

    Coroutine RadioMove;

    private void Awake()
    {
        UIManager.Instance.MenuUI = this;
    }

    private void Start()
    {
        if (RestartButton != null)
        {
            RestartButton.onClick.AddListener(OnClickRestartButton);
        }
        if (OffGameButton != null)
        {
            OffGameButton.onClick.AddListener(OnClickOffGameButton);
        }
        if (PauseButton != null)
        {
            PauseButton.onClick.AddListener(OnClickPauseButton);
        }
        if (VolumeSlider != null)
        {
            VolumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    private void OnEnable()
    {
        PauseUIGameObject.SetActive(false);

        Radio.anchoredPosition = new Vector3(0,-200,0);
        if(RadioMove != null)
        {
            StopCoroutine(RadioMove);
            RadioMove = null;
        }
        RadioMove = StartCoroutine(MoveRadio());

        float CurrentValue;
        MasterAudioMixer.GetFloat("MasterAudioMixer", out CurrentValue);
        VolumeSlider.value = Mathf.InverseLerp(-80f, 0f, CurrentValue);
    }

    IEnumerator MoveRadio()
    {
        while (Radio.anchoredPosition.y < 0)
        {
            Radio.position += new Vector3(0, RadioSpeed, 0) * Time.deltaTime;

            yield return null;
        }
    }

    void OnClickRestartButton()
    {
        OnOffMenu();
    }

    void OnClickOffGameButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
    void OnClickPauseButton()
    {
        PauseUIGameObject.SetActive(true);
        Time.timeScale = 0;
        StartCoroutine(PauseCoroutine());
    }

    IEnumerator PauseCoroutine()
    {
        bool isloop = true;

        while (isloop)
        {
            if (Input.anyKeyDown)
            {
                Time.timeScale = 1;
                PauseUIGameObject.SetActive(false);
                isloop = false;
            }
            yield return null;
        }
    }

    public void OnOffMenu()
    {
        MenuUIGameObject.SetActive(!MenuUIGameObject.activeSelf);
    }

    void SetVolume(float siderValue)
    {
        float volume = Mathf.Lerp(-80f, 0f, siderValue);
        MasterAudioMixer.SetFloat("MasterVolume", volume);  
    }
}
