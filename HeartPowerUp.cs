using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPowerUp : MonoBehaviour
{
    [SerializeField] float Bonus;
    public AudioSource PickUp;
    private void Start()
    {
        Move();
    }
    private void OnTriggerExit2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "projectile") // checks for if the ball has collided with the powerup
        {
            FindObjectOfType<lifeSystem>().lives += 1; // adds a life
            FindObjectOfType<GameManager>().score += Bonus; // adds score

            if (FindObjectOfType<GameManager>().VOL)
                PickUp.Play();

            Move();
        }
    }

    void Move() // moves the power up away out of bounds for future use
    {
        gameObject.GetComponent<Hover>().posOffset = GameObject.Find("OutofBoundsPoint").transform.position;
        transform.position = GameObject.Find("OutofBoundsPoint").transform.position;
    }
    private void Update()
    {
        if(!FindObjectOfType<GameManager>().GameOn)  // if game isnt on, moves the projectile
        {
            Move();
        }
    }
}
