using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stdamagetxt : MonoBehaviour
{

    Vector3 pos;
    float speed = 1.0f;
	void Start () {
        StartCoroutine("Destroy");
	}
	

	void Update () {
        pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y + speed * Time.deltaTime, pos.z);
	}

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
