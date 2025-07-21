using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollisionCheck : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Check");
        if (other.CompareTag("Wall"))
        {
            if (Input.GetKey(KeyCode.LeftArrow) && gameObject.CompareTag("RedCube"))
            {
                Destroy(gameObject);
            }
        
            if (Input.GetKey(KeyCode.RightArrow) && gameObject.CompareTag("BlueCube"))
            {
                Destroy(gameObject);
            }
        }
    }
}
