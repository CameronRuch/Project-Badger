using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour

{

    public CharacterController PlayerHeight;
    public float normalheight, crouchHeight;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            PlayerHeight.height = crouchHeight;
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            PlayerHeight.height = normalheight;
        }
    }
}
