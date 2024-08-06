using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipSlap : MonoBehaviour
{

    [Header("Gorilla Player")]
    public Rigidbody GorillaPlayer;

    [Header("Force (Reccomended Force Is 1000)")]
    public int Force;

    void OnTriggerEnter()
    {
        GorillaPlayer.AddForce(new Vector3(0, Force, 0), ForceMode.Impulse);

    }
}

//slip slap script