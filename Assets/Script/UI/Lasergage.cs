using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lasergage : MonoBehaviour
{

    Player playercs;
    public Image UIobj;
    public GameObject Eff;

    void Start()
    {
        playercs = GameObject.Find("player").GetComponent<Player>();
    }

    void Update()
    {
        UIobj.fillAmount = playercs.lasercount / playercs.laserDefaultTime;
        if (playercs.lasercount <= 0 && playercs.lasercount >= -10f)
        {
            Instantiate(Eff, new Vector3(-4.5f, 4.0f, 0), Quaternion.Euler(0f, 0f, 0f));
            playercs.lasercount = -15f;
        }
    }
}
