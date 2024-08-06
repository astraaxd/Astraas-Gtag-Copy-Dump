using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using easyInputs;
using GorillaNetworking;
public class TagGun : MonoBehaviour
{
    public GameObject Sphere;
    private Renderer SphereRender;
    public Material UnpressedMaterial;
    public Material PressedMaterial;
    public GameObject Platform;
    
    void Start()
    {
        Sphere.SetActive(false);
        SphereRender = Sphere.GetComponent<Renderer>();

    }


    void Update()
    {

        if (EasyInputs.GetGripButtonDown(EasyHand.RightHand))
        {
            Sphere.SetActive(true);
            if (EasyInputs.GetTriggerButtonDown(EasyHand.RightHand))
            {
                SphereRender.material = PressedMaterial;
                Platform.transform.position = Sphere.transform.position;
            }
        }

        else
        {
            Sphere.SetActive(false);
            SphereRender.material = UnpressedMaterial;
        }
    }
}
