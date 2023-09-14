using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Script : MonoBehaviour
{
    public bool isalreadycollected;

    public void coincollected()
    {
        PlayerPrefs.SetInt("coiniscolleccted", 1);
        isalreadycollected = true;
    }

}
