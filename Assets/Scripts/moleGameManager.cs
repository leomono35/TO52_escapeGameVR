using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moleGameManager : MonoBehaviour
{

    public Collider hammer;
    public GameObject[] goodMoles;
    public GameObject[] badMoles;
    public GameObject redKey;
    public GameObject greenKey;
    public AudioClip awardSound;

    private System.Random rand = new System.Random();
    private List<GameObject> goodMolesList = new List<GameObject>();
    private List<GameObject> badMolesList = new List<GameObject>();
    private List<GameObject> activeGoodMole = new List<GameObject>();
    private List<GameObject> activeBadMole = new List<GameObject>();
    private List<int> activePosition = new List<int>();
    private bool gameOn = true;
    private Vector3 defaultPosition = new Vector3(30f, 30f, 1f);
    private int winNumber = 0;

    public void startGame()
    {
        StartCoroutine(gameManager());
    }

    private void listInitialisation()
    {
        goodMolesList.Clear();
        badMolesList.Clear();
        foreach(GameObject mole in goodMoles)
        {
            goodMolesList.Add(mole);
        }
        foreach (GameObject mole in badMoles)
        {
            badMolesList.Add(mole);
        }
    }

    private bool isGameStillOn()
    {
        return !(goodMolesList.Count == 0);
    }

    private Vector3 getCoordinates(int position)
    {
        Vector3 result = new Vector3();

        result.z = 0.5f;
        result.x = 1f;//To initialize
        result.y = 1f;//but thos variable will change

        if(position %3 == 0)
        {
            result.x = -0.663f;
        }
        else if (position % 3 == 1)
        {
            result.x = 0f;
        }
        else if (position % 3 == 2)
        {
            result.x = 0.663f;
        }

        if (position / 3 == 0)
        {
            result.y = -0.663f;
        }
        else if (position / 3 == 1)
        {
            result.y = 0f;
        }
        else if (position / 3 == 2)
        {
            result.y = 0.663f;
        }

        return result;
    }

    public IEnumerator gameManager()    //function is called by the coin machine
    {
        gameOn = true;
        listInitialisation();
        int moleToSpawn = 0; //0 = good, else = bad
        int spawnPosition = 0;
        int movementSpeed = 0;
        bool foundMole = false;
        bool foundPosition = false;
        GameObject moleObjectToSpawn = new GameObject();
        Vector3 spawnVectorPosition = new Vector3();

        while (gameOn)
        {
            if (goodMolesList.Count != activeGoodMole.Count || badMolesList.Count != activeBadMole.Count) //Avoid a softlock with the "while (!foundMole)"
            {
                //define which mole to spawn
                while (!foundMole)
                {
                    moleToSpawn = rand.Next(4);
                    switch (moleToSpawn)
                    {
                        case 0:
                            if (goodMolesList.Count != activeGoodMole.Count)
                            {
                                foundMole = true;
                            }
                            break;
                        case var expression when moleToSpawn > 0:
                            if (badMolesList.Count != activeBadMole.Count)
                            {
                                foundMole = true;
                            }
                            break;
                        default:
                            break;
                    }
                }
                foundMole = false;

                switch (moleToSpawn)
                {
                    case 0:
                        foreach (GameObject mole in goodMolesList)
                        {
                            if (!(activeGoodMole.Contains(mole)))
                            {
                                moleObjectToSpawn = mole;
                                break;
                            }
                        }
                        break;
                    case var expression when moleToSpawn > 0:
                        foreach (GameObject mole in badMolesList)
                        {
                            if (!(activeBadMole.Contains(mole)))
                            {
                                moleObjectToSpawn = mole;
                                break;
                            }
                        }
                        break;
                    default:
                        break;
                }

                //define where to spawn it
                while (!foundPosition)
                {
                    spawnPosition = rand.Next(9);
                    if(!(activePosition.Contains(spawnPosition)))
                        {
                            foundPosition = true;
                        }
                }
                foundPosition = false;

                spawnVectorPosition = getCoordinates(spawnPosition);
                //activePosition.Add(spawnPosition);


                //spawn the mole
                moleObjectToSpawn.GetComponent<Transform>().localPosition = spawnVectorPosition;


                //give it a movement and call it
                movementSpeed = rand.Next(1, 6);

                moleObjectToSpawn.SendMessage("getPosition", spawnPosition);
                activePosition.Add(spawnPosition);

                moleObjectToSpawn.SendMessage("taunt", movementSpeed);


                //check if game still on (green moles left) else, gameOn = false
                if (!isGameStillOn())
                {
                    gameOn = false;
                }

            }
            //wait a bit
            yield return new WaitForSeconds(1);
        }

        resetMolesPositions();
        spawnAward();

        yield return new WaitForSeconds(0); //To avoid an error with the function type requesting a return
    }

    public void newActiveMole(GameObject mole)
    {
        if(goodMolesList.Contains(mole))
        {
            activeGoodMole.Add(mole);
        }
        else
        {
            activeBadMole.Add(mole);
        }
    }

    public void endActiveMole(GameObject mole)
    {
        if (activeGoodMole.Contains(mole))
        {
            activeGoodMole.Remove(mole);
        }
        else
        {
            activeBadMole.Remove(mole);
        }
        mole.GetComponent<Transform>().localPosition = defaultPosition;
    }

    public void hitMole(GameObject mole)
    {
        endActiveMole(mole);
        if (goodMolesList.Contains(mole))
        {
            goodMolesList.Remove(mole);
        }
        else
        {
            addNewGoodMole();
            //badMolesList.Remove(mole);
        }
    }

    public void outPosition(int position)
    {
        activePosition.Remove(position);
    }

    private void resetMolesPositions()
    {
        foreach(GameObject mole in goodMoles)
        {
            mole.GetComponent<Transform>().position = defaultPosition;
        }
        foreach (GameObject mole in badMoles)
        {
            mole.GetComponent<Transform>().position = defaultPosition;
        }
    }

    private void addNewGoodMole()
    {
        if(goodMoles.Length != goodMolesList.Count)
        {
            foreach(GameObject mole in goodMoles)
            {
                if(!(goodMolesList.Contains(mole)))
                {
                    goodMolesList.Add(mole);
                    break;
                }
            }
        }
    }

    private void spawnAward()
    {
        if(winNumber == 0)
        {
            //drop red key
            redKey.GetComponent<Transform>().position = new Vector3(5.273f, 0.84f, 0.334f);
            AudioSource.PlayClipAtPoint(awardSound, redKey.GetComponent<Transform>().position);
            winNumber++;
        }
        else
        {
            //drop green key
            greenKey.GetComponent<Transform>().position = new Vector3(5.273f, 0.84f, 0.334f);
            AudioSource.PlayClipAtPoint(awardSound, greenKey.GetComponent<Transform>().position);
        }
    }
}
