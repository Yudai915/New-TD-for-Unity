using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{
    Image image;
    Chara characs;

    bool TF = true;

    float colorcode = 0;

    void Start()
    {
        image = this.gameObject.GetComponent<Image>();
        characs = GameObject.Find("Chara").GetComponent<Chara>();
    }

    void Update()
    {
        if (characs.GodHP <= 0)
        {
            if(colorcode >= 170)
            {
                colorcode = 170;
            }
            image.color = new Color(1, 1, 1, colorcode / 255f);
            colorcode++;
        }
    }
}
