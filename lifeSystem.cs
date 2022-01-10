using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lifeSystem : MonoBehaviour
{
    public float lives;
    [SerializeField] Text LifeText;
   
    public AudioSource GameOverSFX;

    private void Update()
    {
        
        LifeText.text = "Lives: " + lives; //  updates life text
        if (FindObjectOfType<GameManager>().GameOn)
        {
            if (lives <= 0 && FindObjectOfType<GameManager>().GameOn) // if lives have hit zero, end game
            {
                GameOver();
                Debug.Log("Dead");
            }


            if (GameObject.Find("Pad") == null && GameObject.Find("Pad 2") == null && GameObject.Find("Pad 3") == null && FindObjectOfType<GameManager>().GameOn) // if none of the pads are left on screen, end game
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        if (FindObjectOfType<GameManager>().VOL)
            GameOverSFX.Play();

        FindObjectOfType<EndGame>().EndGameUI(); // summons teh end game UI

        /*
        FindObjectOfType<PlayerInfo>().HighScore = FindObjectOfType<GameManager>().Highscore;
        FindObjectOfType<PlayerInfo>().Wave = FindObjectOfType<GameManager>().Wave;
        FindObjectOfType<GameManager>().GameOn = false;
        FindObjectOfType<PlayerInfo>().SavePlayer();
        */
       
    }
}
