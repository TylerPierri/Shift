using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainControl : MonoBehaviour
{
    
    [SerializeField] Joystick joystick;
    [SerializeField] Transform CentreRotationPoint;

    


    private void Update()
    {
        if (FindObjectOfType<GameManager>().GameOn && !FindObjectOfType<GameManager>().pause)
        {
            Vector3 moveVector = (Vector3.up * joystick.Horizontal + Vector3.left * joystick.Vertical); // syncronises the joysticks position with the rotation of pads to allow control
            if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            {
                CentreRotationPoint.transform.rotation = Quaternion.LookRotation(Vector3.forward, moveVector); // rotates the pads position
            }
        }
        else
        {

            CentreRotationPoint.transform.Rotate(0, 0, 20 * Time.deltaTime); // if the game is paused or not running, the pads will rotate in an idle circle for style

            
        }

    }

 
}
