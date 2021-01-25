using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public GroundCotroller ground;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ABC")
        {
            ground = FindObjectOfType<GroundCotroller>();
            ground.hope = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ABC")
        {
            ground = FindObjectOfType<GroundCotroller>();
            ground.hope = true;
        }
    }

}
