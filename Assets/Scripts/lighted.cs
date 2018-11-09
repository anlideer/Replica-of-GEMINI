using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lighted : MonoBehaviour {

    bool islighted;
    Animator anim;


	// Use this for initialization
	void Start () {
        islighted = false;
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !islighted)
        {
            islighted = true;
            anim.Play("light");
        }
    }
}
