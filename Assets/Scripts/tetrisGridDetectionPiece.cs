using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tetrisGridDetectionPiece : MonoBehaviour
{
    public GameObject gridDetection;

    //0 = blue / 1 = orange / 2 = green / 3 = pink / 4 = yellow
    private int pieceNumber;

    void Start()
    {
        setPieceNumber();
    }

    private void OnTriggerEnter(Collider other)
    {
        onTriggerHappened(other);
    }

    private void OnTriggerExit(Collider other)
    {
        onTriggerHappened(other);
    }

    private void onTriggerHappened(Collider other)
    {
        int[] intel = new int[2];
        intel[1] = pieceNumber;

        if (other.name.Contains("Blue"))
        {
            intel[0] = 0;
        }
        else if (other.name.Contains("Orange"))
        {
            intel[0] = 1;
        }
        else if (other.name.Contains("Green"))
        {
            intel[0] = 2;
        }
        else if (other.name.Contains("Pink"))
        {
            intel[0] = 3;
        }
        else if (other.name.Contains("Yellow"))
        {
            intel[0] = 4;
        }

        gridDetection.SendMessage("gridUpdate", intel);
    }

    private void setPieceNumber()
    {
        if (this.name.Contains("(0)"))
        {
            pieceNumber = 0;
        }
        else if (this.name.Contains("(1)"))
        {
            pieceNumber = 1;
        }
        else if (this.name.Contains("(2)"))
        {
            pieceNumber = 2;
        }
        else if (this.name.Contains("(3)"))
        {
            pieceNumber = 3;
        }
        else if (this.name.Contains("(4)"))
        {
            pieceNumber = 4;
        }
        else if (this.name.Contains("(5)"))
        {
            pieceNumber = 5;
        }
        else if (this.name.Contains("(6)"))
        {
            pieceNumber = 6;
        }
        else if (this.name.Contains("(7)"))
        {
            pieceNumber = 7;
        }
        else if (this.name.Contains("(8)"))
        {
            pieceNumber = 8;
        }
        else if (this.name.Contains("(9)"))
        {
            pieceNumber = 9;
        }
        else if (this.name.Contains("(10)"))
        {
            pieceNumber = 10;
        }
        else if (this.name.Contains("(11)"))
        {
            pieceNumber = 11;
        }
        else if (this.name.Contains("(12)"))
        {
            pieceNumber = 12;
        }
        else if (this.name.Contains("(13)"))
        {
            pieceNumber = 13;
        }
        else if (this.name.Contains("(14)"))
        {
            pieceNumber = 14;
        }
        else if (this.name.Contains("(15)"))
        {
            pieceNumber = 15;
        }
        else if (this.name.Contains("(16)"))
        {
            pieceNumber = 16;
        }
        else if (this.name.Contains("(17)"))
        {
            pieceNumber = 17;
        }
        else if (this.name.Contains("(18)"))
        {
            pieceNumber = 18;
        }
        else if (this.name.Contains("(19)"))
        {
            pieceNumber = 19;
        }
        else if (this.name.Contains("(20)"))
        {
            pieceNumber = 20;
        }
        else if (this.name.Contains("(21)"))
        {
            pieceNumber = 21;
        }
        else if (this.name.Contains("(22)"))
        {
            pieceNumber = 22;
        }
        else if (this.name.Contains("(23)"))
        {
            pieceNumber = 23;
        }
    }
}
