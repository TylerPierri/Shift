using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpace : MonoBehaviour
{
    Camera cam;
    float width, height;
    public EdgeCollider2D edge;

    [SerializeField] AudioSource LifeLostSFX;
   
    void Start()
    {
        cam = Camera.main;
        edge = GetComponent<EdgeCollider2D>();
    }
    private void Update()
    {
        FindBoundries();
        SetBoundries();
    }

    void FindBoundries()// finds the corners of the screen resolution
    {
        width = 1 / (cam.WorldToViewportPoint(new Vector3(1, 1, 0)).x - 0.5f); 
        height = 1 / (cam.WorldToViewportPoint(new Vector3(1, 1, 0)).y - 0.5f);
    }
    void SetBoundries() // moves the points of the colliders to the corners of the screen
    {
        Vector2 point1 = new Vector2(width / 2, height / 2);
        Vector2 point2 = new Vector2(width / 2, -height / 2);
        Vector2 point3 = new Vector2(-width / 2, -height / 2);
        Vector2 point4 = new Vector2(-width / 2, height / 2);
        Vector2[] tempArray = new Vector2[] { point1, point2, point3, point4, point1 };
        edge.points = tempArray;

    }

    private void OnTriggerExit2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "projectile") // if the projectile goes out of bounds itll reset the ball position
        {
            if (FindObjectOfType<GameManager>().GameOn)
                StartCoroutine(Delay());
        }

        if (hit.gameObject.tag == "Bomb") // if the ball goes out bounds itll be disabled
        {
            if (FindObjectOfType<GameManager>().GameOn)
            {
                GameObject.Find("Bomb").GetComponent<bomb>().RestartGame();
                GameObject.Find("Bomb").SetActive(false);
            }

        }

        if (hit.gameObject.tag == "projectileX")// if the projectile goes out of bounds itll reset the ball position
        {
            if (FindObjectOfType<GameManager>().GameOn)
            {
                FindObjectOfType<ExtraProjectile>().RestartGame();
                FindObjectOfType<lifeSystem>().lives -= 1f;

                if (FindObjectOfType<GameManager>().VOL)
                    LifeLostSFX.Play();


            }
                
        }

    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);

        FindObjectOfType<lifeSystem>().lives -= 1f; // removes a life

        if (FindObjectOfType<GameManager>().VOL)
            LifeLostSFX.Play();

        FindObjectOfType<Projectile>().RestartGame();
    }
}
