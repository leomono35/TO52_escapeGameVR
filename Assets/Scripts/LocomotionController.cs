using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionController : MonoBehaviour
{
    public XRController rightTeleportRay;
    public XRController leftTeleportRay;
    public InputHelpers.Button teleportActivationButton;
    public float activationThreshold = 0.1f;

    // Update is called once per frame
    void Update()
    {
        if(rightTeleportRay)
        {
            rightTeleportRay.gameObject.SetActive(checkIfActivated(rightTeleportRay));
        }
        if (leftTeleportRay)
        {
            leftTeleportRay.gameObject.SetActive(checkIfActivated(leftTeleportRay));
        }
    }

    public bool checkIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, activationThreshold);
        return isActivated;
    }
}
