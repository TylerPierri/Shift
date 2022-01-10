using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateShields : MonoBehaviour
{
    GameObject CentreShields;
    float SpinSpeed, Score;
    private void Start()
    {
        CentreShields = GameObject.Find("Centre Shields");
        
        SpinSpeed = 15;
    }
    void Update()
    {
        CentreShields.transform.Rotate(0, 0, SpinSpeed * Time.deltaTime); // rotates the shields around the centre

        if (SpinSpeed <= 40) // increases the spin speed based of score, prevents it from going above 40 
        {
            Score = FindObjectOfType<GameManager>().score;
            SpinSpeed += Score;
        }
    }
}
