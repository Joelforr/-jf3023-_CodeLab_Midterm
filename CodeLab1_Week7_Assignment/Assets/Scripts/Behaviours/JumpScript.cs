using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : AbstractBehaviour {

    public float jumpSpeed = 200f;
    public float jumpDelay = .1f;
    public int jumpCount = 2;

    protected float lastJumpTime = 0f;
    protected int jumpsRemaining = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        var canJump = inputState.GetButtonValue(inputButtons[0]);
        var holdTime = inputState.GetButtonHoldTime(inputButtons[0]);

        if (collisionState.standing) {
            if (canJump && holdTime < .1) {
                jumpsRemaining = jumpCount - 1;
                OnJump();
            }
        }else {
            if (canJump && holdTime < .1f && Time.time - lastJumpTime > jumpDelay) {

                if(jumpsRemaining > 0) {
                    OnJump();
                    jumpsRemaining--;
                }
            }
        }
	}

    protected virtual void OnJump() {
        var vel = rb2D.velocity;
        lastJumpTime = Time.time;
        rb2D.velocity = new Vector2(vel.x, jumpSpeed);
    }
}
