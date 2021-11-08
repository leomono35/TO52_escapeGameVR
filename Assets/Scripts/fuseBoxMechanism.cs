using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class fuseBoxMechanism : MonoBehaviour
{

    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject leverCube;
    public AudioClip pluggedSound;

    private int fuseInteractionLayerMaskValue = 32768;
    private LayerMask noFuseMask = 0;

    public void onSelectEnter()
    {
        //block the right hand from grabing the key inside the lock
        noFuseMask = rightHand.GetComponent<XRDirectInteractor>().interactionLayerMask.value - fuseInteractionLayerMaskValue;
        rightHand.GetComponent<XRDirectInteractor>().interactionLayerMask = noFuseMask;

        //block the left hand from grabing the key inside the lock
        noFuseMask = leftHand.GetComponent<XRDirectInteractor>().interactionLayerMask.value - fuseInteractionLayerMaskValue;
        leftHand.GetComponent<XRDirectInteractor>().interactionLayerMask = noFuseMask;

        //unlocking lever function
        leverCube.SendMessage("fuseSet");

        //pluggin sound
        AudioSource.PlayClipAtPoint(pluggedSound, leverCube.GetComponent<Transform>().position);
    }
}
