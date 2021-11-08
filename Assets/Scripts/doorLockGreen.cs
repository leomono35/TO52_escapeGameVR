using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class doorLockGreen : MonoBehaviour
{

    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject door;
    public AudioClip unlockSound;

    private int greenKeyInteractionLayerMaskValue = 4096;
    private LayerMask noGreenKeyMask = 0;

    public void onSelectEnter()
    {
        //block the right hand from grabing the key inside the lock
        noGreenKeyMask = rightHand.GetComponent<XRDirectInteractor>().interactionLayerMask.value - greenKeyInteractionLayerMaskValue;
        rightHand.GetComponent<XRDirectInteractor>().interactionLayerMask = noGreenKeyMask;

        //block the left hand from grabing the key inside the lock
        noGreenKeyMask = leftHand.GetComponent<XRDirectInteractor>().interactionLayerMask.value - greenKeyInteractionLayerMaskValue;
        leftHand.GetComponent<XRDirectInteractor>().interactionLayerMask = noGreenKeyMask;

        //unlocking door
        door.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        //unlocking sound
        AudioSource.PlayClipAtPoint(unlockSound, door.GetComponent<Transform>().position);

    }
}
