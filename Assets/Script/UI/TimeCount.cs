using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour
{
    float time = 0;
    string timestr;
    Text timetxt;
    int phase = 0;

    Chara chara;

    void Start()
    {
        timetxt = GetComponent<Text>();
        chara = GameObject.Find("Chara").GetComponent<Chara>();
    }

    void Update()
    {
        if (chara.playerHP <= 0)
        {
            if (phase == 0)
            {
                time = chara.deathcount * 5;
            phase++;
            }
            if (phase == 1)
            {
                time -= Time.deltaTime;
                timestr = time.ToString("0.##");
                timetxt.text = timestr;
            }
        }
        else
        {
            time = 0;
            time -= Time.deltaTime;
            timestr = time.ToString("#");
            timetxt.text = timestr;
            phase = 0;
        }
    }
}
