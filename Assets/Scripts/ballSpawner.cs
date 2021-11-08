using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ballSpawner : MonoBehaviour
{
    public HingeJoint lever;
    public GameObject ball;

    private bool dropped = false;
    private Vector3 dropperBallPosition = new Vector3(7.7f, 3.3f, -5f);
    private bool fuseIsSet = false;

    // Update is called once per frame
    void Update()
    {
        if (fuseIsSet)
        {
            if (!dropped)
            {
                if (lever.angle < -35)
                {
                    dropped = true;

                    //reset ball position in the dropper 
                    ball.GetComponent<Transform>().position = dropperBallPosition;
                    ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                }
            }
            else
            {
                if (lever.angle > -10)
                {
                    dropped = false;
                }
            }
        }
    }

    private void fuseSet()
    {
        fuseIsSet = true;
    }
}
