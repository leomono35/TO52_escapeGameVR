using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tokenMachineMechanism : MonoBehaviour
{
    public GameObject moleTable;

    private Vector3 defaultPosition = new Vector3(30f, 30f, 1f);

    public void onSelectEnter()
    {
        moleTable.SendMessage("startGame");
    }
}
