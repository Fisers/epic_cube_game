using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public bool EnteredTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        EnteredTrigger = true;

        other.GetComponent<BoxCollider>().isTrigger = false;
    }
}
