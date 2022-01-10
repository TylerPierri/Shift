using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D Body;
    float speed;

    public Vector2 velocity;

    public AudioSource Bounce;
    public AudioClip[] audioSources;
    public AudioClip SpawnSFX;
    [SerializeField] GameObject Trails;
    private void Start()
    {
        Body = GetComponent<Rigidbody2D>();
    }
    void GoBall()
    {
        if (FindObjectOfType<GameManager>().VOL && FindObjectOfType<GameManager>().GameOn)
        {
            Bounce.clip = SpawnSFX;
            Bounce.Play(); // spawn SFX
        }
        float rand = Random.Range(0, 2);
        if (rand < 1)
        {
            //Body.AddForce(new Vector2(10, -7));
        }
        else
        {
            //Body.AddForce(new Vector2(-10, -7));
        }
        Trails.SetActive(true);
        if (FindObjectOfType<GameManager>().GameOn && !FindObjectOfType<GameManager>().pause)//  if the game is running and isnt paused, fires projectile
            Body.AddForce(new Vector2(Random.Range(-10, 10), Random.Range(-10, 10)));
    }

    public void StartGame()
    {
        Body = GetComponent<Rigidbody2D>();
        Invoke("GoBall", 2);
        speed = 2f;
    }

    
    public void ResetBall()// reset balls position and speed
    {
        Body.velocity = Vector2.zero;
        transform.position = Vector2.zero;
        speed = 2f;
    }

    public void RestartGame()
    {
        if (FindObjectOfType<GameManager>().GameOn)
        {
            ResetBall();
            Trails.SetActive(false);  // disables trail of the ball to prevent stretch when ball is reset in position
            Invoke("GoBall", 1); // calls the function 1 second later
        }
    }


    void OnCollisionEnter2D(Collision2D Hit)// check for collison with any of these objects
    {
        if (Hit.collider.CompareTag("pad") || 
            Hit.collider.CompareTag("pad 2") || 
            Hit.collider.CompareTag("pad 3") || 
            Hit.collider.CompareTag("Bomb") ||
            Hit.collider.CompareTag("Shield"))
        {
            velocity = GetComponent<Rigidbody2D>().velocity;
            //velocity.x = Body.velocity.x;
            //velocity.y = (Body.velocity.y / 2) + (Hit.collider.attachedRigidbody.velocity.y / 3);
            Body.velocity = velocity.normalized * speed; // adds force behind ball on bounce for new direction

            Bounce.clip = audioSources[Random.Range(0, audioSources.Length)];

            if (FindObjectOfType<GameManager>().VOL)
                Bounce.Play();

            if (speed <= 4f)
                speed += 0.2f;

        }
    }
    private void Update()
    {
        if(!FindObjectOfType<GameManager>().GameOn || FindObjectOfType<GameManager>().pause) // checking for if the game has been stoped or paused to reset the ball
        {
            ResetBall();
        }
    }
}
