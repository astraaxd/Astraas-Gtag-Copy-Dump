using UnityEngine;

public class RGBManager : MonoBehaviour
{
    [Header("THIS SCRIPT WAS MADE BY MystyVR. IT IS NOT YOURS.")]
    [Header("If you make a video on this script")]
    [Header("credit me with my discord")]
    public GameObject targetObject;
    public RainbowEffect rainbowScript;

    private void OnEnable()
    {
        if (rainbowScript != null)
        {
            rainbowScript.enabled = true;
        }
    }

    private void OnDisable()
    {
        if (rainbowScript != null)
        {
            rainbowScript.enabled = false;
        }
    }
}
