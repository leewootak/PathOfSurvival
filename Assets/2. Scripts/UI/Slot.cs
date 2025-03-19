using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image Itemicon;
    public TextMeshProUGUI NumObItems;

    public void changeImage(Sprite image, int quantity)
    {
        Itemicon.sprite = image;
        NumObItems.text = quantity.ToString();
    }
}
