using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public Vector2 posOffset;
    Vector2 tempPos = new Vector2();

    public float amplitude; // distance for it to travel up and down
    public float frequency; // how fast do you want it to hover in a given time
    private void Start()
    {
        posOffset = gameObject.transform.position;
    }
    private void Update()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude; // calculates a smooth tranistion when hovering up and down

        transform.position = tempPos;
    }
}
