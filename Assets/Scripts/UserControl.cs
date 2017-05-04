using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class UserControl : MonoBehaviour {

    private Player character;
    private bool jump;
    private bool crouch;


	// Use this for initialization
    void Awake()
    {
        character = GetComponent<Player>();
    }
	
	
	// Update is called once per frame
	void Update () {
        if (jump == false)
        {
            //Read jump input in Update so that button
            // presses aren't missed
            jump = Input.GetButtonDown("Jump");

        }
		
	}

    void FixedUpdate()
    {
        //Read inputs
        crouch = Input.GetKey(KeyCode.LeftControl);
        float h = Input.GetAxis("Horizontal");
        //Pass all parameters to the character move
        character.Move(h, crouch, jump);
        jump = false;

    }
}
