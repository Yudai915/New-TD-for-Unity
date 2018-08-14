using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_heart : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		if(transform.position.x < -6.52f)
        {
            transform.position = new Vector3(-6.52f,transform.position.y,transform.position.z);
        }
	}

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
