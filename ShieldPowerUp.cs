using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    [SerializeField] GameObject Pad1X, Pad2X, Pad3X, Pad1, Pad2, Pad3;
    [SerializeField] float Bonus;
    public AudioSource PickUp;
    private void Start()
    {
        Move();
    }
    private void OnTriggerExit2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "projectile") // checks for collision of projectile
        {
            if(FindObjectOfType<GameManager>().VOL)
                PickUp.Play();

            ActiveShield();
            FindObjectOfType<GameManager>().score += Bonus;
            Move();
        }
    }

    void ActiveShield()
    {
        FindObjectOfType<GameManager>().Shielded = true;

        //checks to see what pads are currently active on screen at the time of pick up
        if (GameObject.Find("Pad"))
        {
            //switches the pad to the shielded version of it
            Pad1X.SetActive(true);
            Pad1.SetActive(false);
        }
            

        if (GameObject.Find("Pad 2"))
        {
            Pad2X.SetActive(true);
            Pad2.SetActive(false);
        }
            

        if (GameObject.Find("Pad 3"))
        {
            Pad3X.SetActive(true);
            Pad3.SetActive(false);
        }
            

    }
    void Move() // moves the power up out of bounds for future use
    {
        gameObject.GetComponent<Hover>().posOffset = GameObject.Find("OutofBoundsPoint").transform.position;
        transform.position = GameObject.Find("OutofBoundsPoint").transform.position;
    }

    private void Update()
    {
        if (!GameObject.Find("Pad Boost Texture") || !GameObject.Find("Pad 2 Boost Texture") || !GameObject.Find("Pad 3 Boost Texture")) // if one of the pads active doesnt have a shield itll allow the power up to be spawned in again
            FindObjectOfType<GameManager>().Shielded = false;

        if (!FindObjectOfType<GameManager>().GameOn)
            Move();
    }
}
