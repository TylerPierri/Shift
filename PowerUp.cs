using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] GameObject Pad;
    [SerializeField] float Bonus;
    public AudioSource PickUp;
    private void Start()
    {
        Move();
    }
    private void OnTriggerExit2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "projectile") // checking for collsion with the blue ball
        {
            if (FindObjectOfType<GameManager>().VOL)
                PickUp.Play();

            Pad.SetActive(true); // the chosen pad is activated, this script is on multiple power ups so the corrosplonding active pad will be different 
            FindObjectOfType<GameManager>().score += Bonus;
            Move();
        }
    }

    void Move() // moves the projectile out of bounds for future use
    {
        gameObject.GetComponent<Hover>().posOffset = GameObject.Find("OutofBoundsPoint").transform.position;
        transform.position = GameObject.Find("OutofBoundsPoint").transform.position;
    }

    private void Update()
    {

        if (!FindObjectOfType<GameManager>().GameOn)
            Move();
    }
}
