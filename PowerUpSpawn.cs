using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawn : MonoBehaviour
{
    public List<GameObject> SpawnList;

    public GameObject[] Powerup;

    public GameObject OutofBoundsPoint;

    Vector3 SpawnLocation;

    public AudioSource Spawn;

    int i = 0;
    private void Start()
    {
        //RollForPowerUp();
    }
    void RollForSpawn()
    {
        i = Random.Range(0, SpawnList.Count); // chooses a random number between 0 and the max count of the spawn list
        SpawnLocation = SpawnList[i].transform.position; // the choosen spawn number corrospondes to the choosen spawn postion for the power up
        //SpawnList.RemoveAt(i);
    }
    public void RollForPowerUp()
    {
        RollForSpawn();
        int a = Random.Range(0, Powerup.Length);
        //Debug.Log("Item" + a);
        switch(a)
        {
            case 0:// pad 2
                if(!GameObject.Find("Pad 2")) // if the pad doesnt already exist ingame then activate it
                {
                    Powerup[0].transform.position = SpawnLocation;
                    Powerup[0].GetComponent<Hover>().posOffset = SpawnLocation;
                    if (FindObjectOfType<GameManager>().VOL)
                        Spawn.Play();
                }
                else // if it doesnt meet the requirements for this power up, itll roll again until it can produce a powerup thats allowed to spawn
                {
                    RollForPowerUp();
                }
                break;

            case 1: // pad 3
                if (!GameObject.Find("Pad 3"))// if the pad doesnt already exist ingame then activate it
                {
                    Powerup[1].transform.position = SpawnLocation;
                    Powerup[1].GetComponent<Hover>().posOffset = SpawnLocation;
                    if (FindObjectOfType<GameManager>().VOL)
                        Spawn.Play();
                }
                else// if it doesnt meet the requirements for this power up, itll roll again until it can produce a powerup thats allowed to spawn
                {
                    RollForPowerUp();
                }
                break;

            case 2: // heart
                if (FindObjectOfType<lifeSystem>().lives < 5) // if the life count is above 5 then activate it
                {
                    Powerup[2].transform.position = SpawnLocation;
                    Powerup[2].GetComponent<Hover>().posOffset = SpawnLocation;
                    if (FindObjectOfType<GameManager>().VOL)
                        Spawn.Play();
                }
                else// if it doesnt meet the requirements for this power up, itll roll again until it can produce a powerup thats allowed to spawn
                {
                    RollForPowerUp();
                }
                break;

            case 3: // bomb

                Powerup[3].SetActive(true);
                break;

            case 4:// pad 1
                if (!GameObject.Find("Pad")) // if the pad doesnt already exist ingame then activate it
                {
                    Powerup[4].transform.position = SpawnLocation;
                    Powerup[4].GetComponent<Hover>().posOffset = SpawnLocation;

                    if (FindObjectOfType<GameManager>().VOL)
                        Spawn.Play();
                }
                else// if it doesnt meet the requirements for this power up, itll roll again until it can produce a powerup thats allowed to spawn
                {
                    RollForPowerUp();
                }
                break;

            case 5: // Enemy Shield
                if (!GameObject.Find("Enemy Shield")) // if the enemy shield doesnt already exist ingame then activate it
                { 
                    Powerup[5].SetActive(true);

                    if(FindObjectOfType<GameManager>().VOL)
                        Spawn.Play();

                    //Debug.Log("Shield");
                }
                else// if it doesnt meet the requirements for this power up, itll roll again until it can produce a powerup thats allowed to spawn
                {
                    RollForPowerUp();
                }
                break;

            case 6: // shield Pads
                if (!FindObjectOfType<GameManager>().Shielded) // if the pads arent shielded then activate it
                {
                    Powerup[6].transform.position = SpawnLocation;
                    Powerup[6].GetComponent<Hover>().posOffset = SpawnLocation;

                    if (FindObjectOfType<GameManager>().VOL)
                        Spawn.Play();
                }
                else// if it doesnt meet the requirements for this power up, itll roll again until it can produce a powerup thats allowed to spawn
                {
                    RollForPowerUp();
                }
                break;

            case 7: // Extra Projectile
                if (!GameObject.Find("PowerUp Projectile") && FindObjectOfType<lifeSystem>().lives > 3) // if the extra ball doesnt already exist ingame and the life count is above 3 then activate it
                {
                    Powerup[7].SetActive(true);
                    FindObjectOfType<ExtraProjectile>().StartGame();

                    if (FindObjectOfType<GameManager>().VOL)
                        Spawn.Play();
                }
                else// if it doesnt meet the requirements for this power up, itll roll again until it can produce a powerup thats allowed to spawn
                {
                    RollForPowerUp();
                }
                break;
        }
    }
}
