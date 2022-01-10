using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public GameObject EndUI;
    public bool EndActive, ConfettiCannon;
    float End, Speed;
    Text ScoreText;
    public void EndGameUI() // GameOver UI is called
    {
        FindObjectOfType<GameManager>().End = true;
        FindObjectOfType<GameManager>().GameOn = false; // stops the game from running

        GameObject.Find("Game UI").SetActive(false);

        EndUI.SetActive(true);
        Invoke("delay", 0.8f);

        if (FindObjectOfType<GameManager>().VOL) // checks if volume is active
        {
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
            GameObject.Find("GameManager").GetComponent<AudioSource>().volume = 0.1f; // dampens the volume to signal the game is over
        }

        GameObject.Find("Wave Text").GetComponent<Text>().text = "Wave: " + FindObjectOfType<GameManager>().Wave.ToString();
        GameObject.Find("HighScore Text").GetComponent<Text>().text = "HighScore: " + FindObjectOfType<PlayerInfo>().HighScore.ToString();
        ScoreText = GameObject.Find("Score Text").GetComponent<Text>();
        End = FindObjectOfType<GameManager>().score;
        ConfettiCannon = true;
    }

    void delay()
    {
        EndActive = true;


        if (FindObjectOfType<GameManager>().VOL)
            GameObject.Find("HighScore Text").GetComponent<AudioSource>().Play();
    }
    private void FixedUpdate()
    {
       if(EndActive)
       {
            float CurrentScore;
            //CurrentScore = Mathf.SmoothDamp(0, End, ref Velocity, Time.deltaTime * 100);
            Speed += Time.deltaTime * 1f;
            CurrentScore = Mathf.Lerp(0, End, Speed); // increase score slowly over time to aquired score
            ScoreText.text = "Score: " + CurrentScore.ToString("F0");

            if (CurrentScore > FindObjectOfType<PlayerInfo>().HighScore) // checks if the score is above the highscore
            {
                GameObject.Find("HighScore Text").GetComponent<Text>().text = "HighScore: " +  "<color=orange><b>" + CurrentScore.ToString("F0") + "</b></color>"; // updates highscore with score

                if (CurrentScore >= FindObjectOfType<GameManager>().Highscore && ConfettiCannon) // once counting is finished, if a new highscore was achieved
                {
                    // new Highscore Notification
                    StartCoroutine(Confetti());
                }
            }
                
            //Debug.Log(CurrentScore);


        }
    }

    private IEnumerator Confetti() // particale effect with sound for celebration of new highscore
    {
        ConfettiCannon = false;
        if (FindObjectOfType<GameManager>().VOL)
            GameObject.Find("Confetti").GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.5f);
        GameObject.Find("Confetti").GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(4);
        GameObject.Find("Confetti").GetComponent<ParticleSystem>().Stop();
    }
}
