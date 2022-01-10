using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSystem : MonoBehaviour
{
    Slider ProgressBar;
    Text WaveText;
    public float ProgressFloat;
    //public int CurrentWave;
    public AudioSource NextPart, NextWave, HitSFX;
    public float H, S, V;
    [SerializeField] Color colour;
    bool Hit;

    private void Start()
    {
        colour = gameObject.GetComponent<SpriteRenderer>().color;
        Color.RGBToHSV(colour, out H, out S, out V);
        H = 0.15f;
    }
    public void StartGame()
    {
        WaveText = GameObject.Find("Wave").GetComponent<Text>();
        ProgressBar = GameObject.Find("Progress").GetComponent<Slider>();
        //StartCoroutine(FindObjectOfType<Blast>().BlastWave());
    }
    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "projectile" || hit.gameObject.tag == "projectileX") // checks for if the ball or extra ball hits the centre
        {
            Debug.Log("Hit");
            Waves();
        }
    }

    void Waves()
    {
        if (FindObjectOfType<GameManager>().GameOn)
        {
            if (ProgressFloat < 90) // if the progress is below 90, itll add to the progress
            {
                ProgressFloat += 10;
                H = 0.15f;
                StartCoroutine(ChangeColour());

                if (FindObjectOfType<GameManager>().VOL)
                {
                    HitSFX.pitch = Random.Range(0, 2); // diffrent pitches to prevent annoyance
                    HitSFX.Play();
                }
            }
            else // if the score hits 90 or above, itll reset the progress and add a wave level
            {
                FindObjectOfType<GameManager>().Wave += 1;
                H = 0.6f;
                StartCoroutine(ChangeColour());

                //if (Random.value < 0.5f)
                    FindObjectOfType<lifeSystem>().lives += 1f; // adds a life upon new wave achievement

                ProgressFloat = 0; // resets the progress

                if (FindObjectOfType<GameManager>().VOL)
                    NextWave.Play();

                StartCoroutine(GameObject.Find("BlastWave").GetComponent<Blast>().BlastWave()); // blast wave effect to signal new wave

            }
        }
        


        switch(ProgressFloat) // if the progress hits 30, 60 or 90, itll spawn in a power up
        {
            case 30:
                FindObjectOfType<PowerUpSpawn>().RollForPowerUp();
                GameObject.Find("Centre").GetComponent<Animator>().SetBool("Hit", true);

                if (FindObjectOfType<GameManager>().VOL)
                    NextPart.Play();
                Invoke("Delay", 0.5f);
                break;

            case 60:
                FindObjectOfType<PowerUpSpawn>().RollForPowerUp();
                GameObject.Find("Centre").GetComponent<Animator>().SetBool("Hit", true);
                if (FindObjectOfType<GameManager>().VOL)
                    NextPart.Play();
                Invoke("Delay", 0.5f);
                break;

            case 90:
                FindObjectOfType<PowerUpSpawn>().RollForPowerUp();
                GameObject.Find("Centre").GetComponent<Animator>().SetBool("Hit", true);
                Invoke("Delay", 0.5f);
                break;
        }
    }

    void Delay()
    {
        GameObject.Find("Centre").GetComponent<Animator>().SetBool("Hit", false);
    }
    private void Update()
    {
        if (ProgressBar != null)
        {
            ProgressBar.value = ProgressFloat;
            WaveText.text = "Wave: " + FindObjectOfType<GameManager>().Wave; // updates the wave text
        }

        colour = Color.HSVToRGB(H, S, V);

        gameObject.GetComponent<SpriteRenderer>().color = colour;

        if(Hit)
            S = Mathf.PingPong(Time.time * 2.5f, 1 - 0); // switches between colours to signal a hit
    }
    IEnumerator ChangeColour() // changes the colour of the centre to signal a hit
    {
        Hit = true;
        yield return new WaitForSeconds(0.5f);
        Hit = false;
        S = 0;
    }
}
