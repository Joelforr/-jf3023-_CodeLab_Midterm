using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonState {
    public bool value;
    public float holdTime = 0;
}

public enum Directions {
    Right = 1,
    Left = -1
}

public class InputState : MonoBehaviour {

    public Directions direction = Directions.Right;
    public float absVelX = 0f;
    public float absVelY = 0f;

    private Rigidbody2D rb2D;
    private Dictionary<Buttons, ButtonState> buttonStates = new Dictionary<Buttons, ButtonState>();


    void Awake() {
        rb2D = GetComponent<Rigidbody2D>();
    }
	
    // Use this for initialization
	void Start () {
		
	}
	
    void FixedUpdate() {
        absVelX = Mathf.Abs(rb2D.velocity.x);
        absVelY = Mathf.Abs(rb2D.velocity.y);
    }

	// Update is called once per frame
	void Update () {

	}

    public void SetButtonValue(Buttons key, bool value) {
        if (!buttonStates.ContainsKey(key))
            buttonStates.Add(key, new ButtonState());

        var state = buttonStates[key];

        //Compare the current dictionary state value to incoming value argument
        //This determines whether button is being held or has been released
        if(state.value && !value) {
            state.holdTime = 0;
        }else if(state.value && value) {
            state.holdTime += Time.deltaTime;
        }

        state.value = value;
    }

    public bool GetButtonValue(Buttons key) {
        if (buttonStates.ContainsKey(key))
            return buttonStates[key].value;
        else
            return false;
    }

    public float GetButtonHoldTime(Buttons key) {
        if (buttonStates.ContainsKey(key))
            return buttonStates[key].holdTime;
        else
            return 0f;
    }
}
