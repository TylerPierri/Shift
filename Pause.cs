using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseUI;

    GameManager GameRules;
    private void Start()
    {
        GameRules = FindObjectOfType<GameManager>();
    }
    public void OnPause() // the player has paused the game
    {
        if (GameRules.VOL)
        {
           GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
           GameRules.GetComponent<AudioSource>().volume = 0.1f;
        }
        GameRules.pause = true;
        pauseUI.SetActive(true);
    }
    public void OffPause() // the player has unpaused and continues the current game
    {
        if (GameRules.VOL)
        {
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
            GameRules.GetComponent<AudioSource>().volume = 0.5f;
        }
        GameRules.pause = false;
        FindObjectOfType<Projectile>().RestartGame();
        pauseUI.SetActive(false);
    }
    public void OnExit() //  the player has chosen to return to main menu
    {
        if (GameRules.VOL)
        {
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
            GameRules.GetComponent<AudioSource>().volume = 0.5f;
        }
        GameRules.GameOn = false;
        GameRules.pause = false;
        GameRules.End = false;
        FindObjectOfType<EndGame>().ConfettiCannon = false;

        FindObjectOfType<EndGame>().EndActive = false;
        FindObjectOfType<EndGame>().EndUI.SetActive(false);
        save();
        pauseUI.SetActive(false);       
    }
    public void OnRetry() // the player has chosen to restart the current game
    {
        if (GameRules.VOL)
        {
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
            GameRules.GetComponent<AudioSource>().volume = 0.5f;
        }

        // resetting values to default values, resetting all projectiles, power ups and pads for restart
        GameRules.GameOn = true;
        FindObjectOfType<EndGame>().EndActive = false;
        GameRules.pause = false;

        FindObjectOfType<EndGame>().ConfettiCannon = false;

        FindObjectOfType<EndGame>().EndUI.SetActive(false);
        GameRules.ResetGame();
        save();
        FindObjectOfType<lifeSystem>().lives = 3;
        FindObjectOfType<WaveSystem>().ProgressFloat = 0;
        GameRules.score = 0;
        FindObjectOfType<Projectile>().RestartGame();
        GameRules.End = false;

        pauseUI.SetActive(false);
    }
    void save() // saves all current data upon chosen button
    {
        FindObjectOfType<PlayerInfo>().HighScore = FindObjectOfType<GameManager>().Highscore;
        FindObjectOfType<PlayerInfo>().Wave = FindObjectOfType<GameManager>().Wave;
        FindObjectOfType<PlayerInfo>().SavePlayer();

        GameRules.Highscore = FindObjectOfType<PlayerInfo>().HighScore;
        GameRules.Wave = FindObjectOfType<PlayerInfo>().Wave;
    }
}
