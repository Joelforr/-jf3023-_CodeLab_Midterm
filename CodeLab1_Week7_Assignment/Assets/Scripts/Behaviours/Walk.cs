using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : AbstractBehaviour {

    public float speed = 50f;

    // Use this for initialization
    void Start () {
        speed = this.GetComponent<PlayerManager>().movespeed;
	}
	
	// Update is called once per frame
	void Update () {
        var right = inputState.GetButtonValue(inputButtons[0]);
        var left = inputState.GetButtonValue(inputButtons[1]);

        if(right || left) {

            var tempSpeed = speed;
            var velX = tempSpeed * (float)inputState.direction;

            rb2D.velocity = new Vector2(velX, rb2D.velocity.y);
        }
    }
}
