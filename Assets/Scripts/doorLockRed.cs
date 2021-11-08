using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class doorLockRed : MonoBehaviour
{

    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject door;
    public AudioClip unlockSound;

    private int redKeyInteractionLayerMaskValue = 8192;
    private LayerMask noRedKeyMask = 0;

    public void onSelectEnter()
    {
        //block the right hand from grabing the key inside the lock
        noRedKeyMask = rightHand.GetComponent<XRDirectInteractor>().interactionLayerMask.value - redKeyInteractionLayerMaskValue;
        rightHand.GetComponent<XRDirectInteractor>().interactionLayerMask = noRedKeyMask;

        //block the left hand from grabing the key inside the lock
        noRedKeyMask = leftHand.GetComponent<XRDirectInteractor>().interactionLayerMask.value - redKeyInteractionLayerMaskValue;
        leftHand.GetComponent<XRDirectInteractor>().interactionLayerMask = noRedKeyMask;

        //unlocking door
        door.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        //unlocking sound
        AudioSource.PlayClipAtPoint(unlockSound, door.GetComponent<Transform>().position);

    }
}
