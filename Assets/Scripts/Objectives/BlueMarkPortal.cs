using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueMarkPortal : MonoBehaviour
{
    public GameObject targetObject;

    private void OnTriggerEnter(Collider other)
    {
        
        PlayerMovement.SetCharacterPosition(targetObject.transform.position);
    }

}
