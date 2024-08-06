using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatChangeForCosmeticMod : MonoBehaviour
{
    public List<GameObject> Disable;
    public List<GameObject> Enable;
    public GameObject Gorilla;
    public Material CustomMata;
    private Renderer OfflineGorilla;
    private Renderer NetworkedGorilla;

    private void OnTriggerEnter(Collider other)
    {
        //random stuff
        foreach (GameObject GameObject in Disable)
        {
            GameObject.SetActive(false);
        }
        foreach (GameObject GameObject in Enable)
        {
            GameObject.SetActive(true);
        }

        OfflineGorilla.material = CustomMata;
        NetworkedGorilla.material = CustomMata;




    }

    void Start()
    {
        OfflineGorilla = Gorilla.GetComponent<Renderer>();
        NetworkedGorilla = GameObject.Find("Global/GorillaParent/GorillaVRRigs/Gorilla Player Networked(Clone)/gorilla").GetComponent<Renderer>();
       

    }
}
