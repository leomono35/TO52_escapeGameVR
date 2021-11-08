using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class doorLockMechanism : MonoBehaviour
{

    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject door;
    public AudioClip unlockSound;

    private int yellowKeyInteractionLayerMaskValue = 256;
    private LayerMask noYellowKeyMask = 0;

    public void onSelectEnter()
    {
        //block the right hand from grabing the key inside the lock
        noYellowKeyMask = rightHand.GetComponent<XRDirectInteractor>().interactionLayerMask.value - yellowKeyInteractionLayerMaskValue;
        rightHand.GetComponent<XRDirectInteractor>().interactionLayerMask = noYellowKeyMask;

        //block the left hand from grabing the key inside the lock
        noYellowKeyMask = leftHand.GetComponent<XRDirectInteractor>().interactionLayerMask.value - yellowKeyInteractionLayerMaskValue;
        leftHand.GetComponent<XRDirectInteractor>().interactionLayerMask = noYellowKeyMask;

        //unlocking door
        door.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        //unlocking sound
        AudioSource.PlayClipAtPoint(unlockSound, door.GetComponent<Transform>().position);

    }
}
