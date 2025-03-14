using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image Itemicon;

    public void changeImage(Sprite image)
    {
        Itemicon.sprite = image;
    }
}
