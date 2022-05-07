using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            VirtualCamController.Instance.ShakeCamera(.3f, .3f);
            transform.position = new Vector3(0f,0f, -0.588f);
        }
    }
}
