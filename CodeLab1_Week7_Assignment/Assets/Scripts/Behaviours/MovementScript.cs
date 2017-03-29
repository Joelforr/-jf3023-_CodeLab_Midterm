using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {

    public float speed = 5f;
    public Buttons[] input;

    private Rigidbody2D rb2D;
    private InputState inputState;

	// Use this for initialization
	void Start () {
        rb2D = GetComponent<Rigidbody2D>();
        inputState = GetComponent<InputState>();	
	}
	
	// Update is called once per frame
	void Update () {
        var right = inputState.GetButtonValue(input[0]);
        var left = inputState.GetButtonValue(input[1]);
        var velX = speed;

        if(right || left) {
            velX *= left ? -1 : 1;
        }else {
            velX = 0;
        }

        rb2D.velocity = new Vector2(velX, rb2D.velocity.y);

    }
}
