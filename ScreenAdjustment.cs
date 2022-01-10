using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAdjustment : MonoBehaviour
{
    PlayerInfo info;
    Camera cam;
    float Ratio_A, Ratio_B;
    public bool Res_A, Res_B;
    public float FOV;
   
    private void Start()
    {
        info = FindObjectOfType<PlayerInfo>();
        cam = Camera.main;
        
        float H = Screen.height; // grabs the screen height
        float W = Screen.width; // grabs the screen width

        //finds out what the screen ratio is of the screen
        while (H != 0 && W != 0)
        {
            if (H > W)
                H %= W;
            else
                W %= H;
        }

        Ratio_A = Screen.height / H;
        Ratio_B = Screen.width / H;

        if(Ratio_B > 9)
        {
            Ratio_A = Ratio_A / 2;
            Ratio_B = Ratio_B / 2;
        }

        if (Ratio_A > 16) // if the screen resolution is above 16:9 ratio
        {
            FOV = 5; // changes the orthographic view to be the appropriate size for the screen
            Res_B = true;
        }
        else 
        {
            FOV = 5.5f;
            Res_A = true;
        }
            

        cam.orthographicSize = FOV;

        
    }

    
}
