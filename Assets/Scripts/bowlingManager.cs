using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowlingManager : MonoBehaviour
{
    public GameObject blueToken;
    public GameObject[] pins;
    public AudioClip awardSound;

    private bool tokenGiven = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    bool isStanding(GameObject pin)
    {
        return (pin.GetComponent<Transform>().rotation.x < -0.6 && pin.GetComponent<Transform>().rotation.x > -0.8 && pin.GetComponent<Transform>().rotation.w > 0.6 && pin.GetComponent<Transform>().rotation.w < 0.8);
    }

    bool allFallen(GameObject[] pinList)
    {
        foreach (GameObject pin in pins)
        {
            if (isStanding(pin))
            {
                return false;
            }
        }

        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if(allFallen(pins) && !tokenGiven)
        {
            blueToken.GetComponent<Transform>().position = new Vector3(7.8f, 1f, -6.6f);
            AudioSource.PlayClipAtPoint(awardSound, blueToken.GetComponent<Transform>().position);
            tokenGiven = true;
        }
    }
}
