using UnityEngine;
//MADE BY MNSHADOW
public class GorillaStickyPlatforms : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Make the player stick to the platform
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Release the player from the platform
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            collision.transform.SetParent(null);
        }
    }
}