using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class SpeedBoost : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
            GorillaLocomotion.Player.Instance.maxJumpSpeed = 600f;
            GorillaLocomotion.Player.Instance.jumpMultiplier = 2.20f;
    }
}
