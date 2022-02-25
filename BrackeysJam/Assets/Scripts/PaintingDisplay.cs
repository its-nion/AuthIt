using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintingDisplay : MonoBehaviour
{
    public Painting painting;

    public Text paintingName;

    public Sprite paintingSprite;
    
    public Text paintingInfo1;
    public Text paintingInfo2;

    void Start()
    {
        paintingName.text = painting._name;
        paintingSprite = painting._painting;
    }
}
