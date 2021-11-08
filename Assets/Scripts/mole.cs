using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mole : MonoBehaviour
{

    public GameObject table;

    private Vector3 squeezeScale = new Vector3 (0.25f, 0.05f, 0.25f);
    private Vector3 normalScale = new Vector3 (0.25f, 0.25f, 0.25f);

    private bool moving = false;
    private Vector3 aimedPosition = new Vector3();
    private Vector3 originalPosition = new Vector3();
    private int speed = 0;
    private float journeyLength = 0;
    private float time = 0;
    private bool taunting = false;
    private int position = 0;
    private Vector3 bufferPosition = new Vector3();

    void Start()
    {
        time = Time.time;
    }

    void Update()
    {
        if (moving)
        {
            float distCovered = (Time.time - time) * (0.5f * speed);

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            this.gameObject.GetComponent<Transform>().localPosition = Vector3.Lerp(originalPosition, aimedPosition, fractionOfJourney);

            if (fractionOfJourney >= 1.0f)
            {
                if(taunting)
                {
                    moving = false;
                    fallBack();
                }
                else
                {
                    sendOutPosition();
                    table.SendMessage("endActiveMole", this.gameObject);
                    moving = false;
                }
            }
        }
    }

    public IEnumerator OnCollisionEnter(Collision col)
    {
        if(col.collider.name == "hammerhead")
        {
            moving = false;
            this.gameObject.GetComponent<Transform>().localScale = squeezeScale;
            bufferPosition = this.GetComponent<Transform>().localPosition;
            bufferPosition.z += 0.2f;
            this.GetComponent<Transform>().localPosition = bufferPosition;
            yield return new WaitForSeconds(1);
            sendOutPosition();
            table.SendMessage("hitMole", this.gameObject);
            this.gameObject.GetComponent<Transform>().localScale = normalScale;
        }
    }

    public void getPosition(int positionActual)
    {
        position = positionActual;
    }

    public void sendOutPosition()
    {
        table.SendMessage("outPosition", position);
    }

    public void taunt(int movementSpeed)
    {
        time = Time.time;
        speed = movementSpeed;

        table.SendMessage("newActiveMole", this.gameObject);

        aimedPosition = this.GetComponent<Transform>().localPosition;
        originalPosition = this.GetComponent<Transform>().localPosition;
        aimedPosition.z = 1; //currently is 0.5

        journeyLength = Vector3.Distance(originalPosition, aimedPosition);

        taunting = true;
        moving = true;
    }

    public void fallBack()
    {
        time = Time.time;
        aimedPosition = this.GetComponent<Transform>().localPosition;
        originalPosition = this.GetComponent<Transform>().localPosition;
        aimedPosition.z = 0.5f; //currently is 1

        journeyLength = Vector3.Distance(originalPosition, aimedPosition);

        taunting = false;
        moving = true;
    }
}
