using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clonegage : MonoBehaviour {

    Player playercs;
    public Image UIobj;
    public GameObject Eff;

    void Start()
    {
        playercs = GameObject.Find("player").GetComponent<Player>();
    }

    void Update()
    {
        UIobj.fillAmount = playercs.Clonecount / playercs.cloneDefaultTime;
        if (playercs.Clonecount <= 0 && playercs.Clonecount >= -10f)
        {
            Instantiate(Eff, new Vector3(-0.92f, 4.0f, 0), Quaternion.Euler(0f, 0f, 0f));
            playercs.Clonecount = -15f;
        }
    }
}
