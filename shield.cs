using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield : MonoBehaviour
{
    [SerializeField] GameObject Shield_Obj;
    [SerializeField] AudioSource Break_SFX;

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "projectile") // if the enemy shield is hit in its weakspot, itll break the shield
        {
            Debug.Log("Hit");

            if (FindObjectOfType<GameManager>().VOL)
                Break_SFX.Play();

            FindObjectOfType<GameManager>().score += 5; // awards score for breaking
            Invoke("Disable", 0.2f);
        }
    }

    private void Disable()
    {
        Shield_Obj.SetActive(false);
    }

}
