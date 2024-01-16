using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] LayerMask _notifyTargetMask;

    private void OnCollisionEnter(Collision collision)
    {
        if ((1 << collision.gameObject.layer & _notifyTargetMask) > 0)
        {
            Debug.Log("This is a box.");
        }
    }
}
