using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tetrisGridDetection : MonoBehaviour
{
    public GameObject blueToken;
    public GameObject yellowKey;
    public AudioClip awardSound;

    private bool[] detectionGridBlue = new bool[24];    //all false by default
    private bool[] detectionGridOrange = new bool[24];
    private bool[] detectionGridGreen = new bool[24];
    private bool[] detectionGridPink = new bool[24];
    private bool[] detectionGridYellow = new bool[24];
    private List<bool[]> gridList = new List<bool[]>(); //0 = blue / 1 = orange / 2 = green / 3 = pink / 4 = yellow
    private bool keyGiven = false;
    private bool tokenGiven = false;

    // Start is called before the first frame update
    void Start()
    {
        gridList.Add(detectionGridBlue);
        gridList.Add(detectionGridOrange);
        gridList.Add(detectionGridGreen);
        gridList.Add(detectionGridPink);
        gridList.Add(detectionGridYellow);
    }

    // Update is called once per frame
    void Update()
    {
        if(isGridFull() == 0 && !tokenGiven)
        {
            //0 = blue : we spawn the token
            blueToken.GetComponent<Transform>().position = new Vector3(-1.07f, 1.1f, -0.15f);
            AudioSource.PlayClipAtPoint(awardSound, blueToken.GetComponent<Transform>().position);
            tokenGiven = true;
        }
        else if (isGridFull() == 4 && !keyGiven)
        {
            //4 = yellow : we spawn the key
            yellowKey.GetComponent<Transform>().position = new Vector3(-1.07f, 1.1f, -0.15f);
            AudioSource.PlayClipAtPoint(awardSound, yellowKey.GetComponent<Transform>().position);
            keyGiven = true;
        }
    }

    private int isGridFull()
    {
        foreach(bool[] grid in gridList)
        {
            if(checkGrid(grid))
            {
                return gridList.IndexOf(grid);
            }
        }

        return -1;
    }

    private bool checkGrid(bool[] grid)
    {
        for (int i = 0; i < 24; ++i)
        {
            if (grid[i] == false)
            {
                return false;
            }
        }

        return true;
    }

    public void gridUpdate(int[] intel)
    {
        if (gridList[intel[0]][intel[1]] == false)
        {
            gridList[intel[0]][intel[1]] = true;
        }
        else
        {
            gridList[intel[0]][intel[1]] = false;
        }
    }
}
