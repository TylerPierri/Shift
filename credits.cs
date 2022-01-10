using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class credits : MonoBehaviour
{
    [SerializeField] GameObject CreditsUI, MenuUI;

    public void OnClickEnter() // credits are shown
    {
        //disables Main Menu UI and enables Credits UI
        FindObjectOfType<Tips>().TipShown = false;
        StopCoroutine(FindObjectOfType<Tips>().TipsCycle());

        if (FindObjectOfType<GameManager>().VOL)
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
        if (FindObjectOfType<GameManager>().VOL)
            FindObjectOfType<GameManager>().MainTheme.volume = 0.5f;
        CreditsUI.SetActive(true);
        MenuUI.SetActive(false);
    }
    public void OnClickExit() // credits are removed
    {
        //enables Main Menu UI and disables Credits UI
        if (FindObjectOfType<GameManager>().VOL)
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
        if (FindObjectOfType<GameManager>().VOL)
            FindObjectOfType<GameManager>().MainTheme.volume = 0.5f;
        MenuUI.SetActive(true);
        CreditsUI.SetActive(false);

        FindObjectOfType<Tips>().TipShown = true;
        StartCoroutine(FindObjectOfType<Tips>().TipsCycle());
    }

    public void OnClcikURL(string URL) // direct link to each credits website
    {
        if (FindObjectOfType<GameManager>().VOL)
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
        Application.OpenURL(URL);
    }
}
