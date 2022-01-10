using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tips : MonoBehaviour
{
    [SerializeField] string[] Tip;
    public bool TipShown;

    public IEnumerator TipsCycle() // cycles through written tips
    {
        if (TipShown)
        {
            GameObject.Find("Tip Text").GetComponent<Text>().text = "Tips:\n" + Tip[Random.Range(0, Tip.Length)]; // randomly choses a tip from the list
            yield return new WaitForSeconds(5); // changes the tip after 5 seconds of reading
            StartCoroutine(TipsCycle()); 
        }
    }
}
