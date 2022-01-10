using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    [SerializeField] Text Score, wave;
    [SerializeField] GameObject VolOn, VolOff;

    public void VolOnClick() // volume is activated
    {
        FindObjectOfType<GameManager>().MainTheme.volume = 0.5f; // makes the volume 0.5f, this was done so switching the vol on or off makes it seemlessly transition rather than just restarting the song
        VolOff.SetActive(true);
        FindObjectOfType<GameManager>().VOL = true;
        VolOn.SetActive(false);

        
        GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
    }
    public void VolOffClick() // volume is deactivated
    {
        FindObjectOfType<GameManager>().MainTheme.volume = 0; // makes the volume to zero
        VolOn.SetActive(true);
        FindObjectOfType<GameManager>().VOL = false;
        VolOff.SetActive(false);

        
        GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
    }
    private void Update()
    {
        Score.text = "HighScore: " + FindObjectOfType<GameManager>().Highscore.ToString(); // updates highscore text
        wave.text = "Wave: " + FindObjectOfType<GameManager>().Wave.ToString(); // updates wave text
        //Vol = FindObjectOfType<PlayerInfo>().Vol;

        if(FindObjectOfType<GameManager>().VOL) // changes volume button according to if volume is active or not
        {
            VolOff.SetActive(false);
            VolOn.SetActive(true);
        }
        else
        {
            VolOn.SetActive(false);
            VolOff.SetActive(true);
        }


    }
}
