using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public GameObject MenuUIGameObject;

    public GameObject Radio;

    public Scrollbar VolumeSlider;

    private void Awake()
    {
        UIManager.Instance.MenuUI = this;
    }

    private void OnEnable()
    {
        
    }
}
