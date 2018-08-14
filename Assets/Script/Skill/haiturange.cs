using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class haiturange : MonoBehaviour {

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.O) || Input.GetButtonUp("ButtonX"))
        {
            Destroy(gameObject);
        }
    }
}
