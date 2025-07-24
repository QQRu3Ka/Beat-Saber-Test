using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapBlades : MonoBehaviour
{
    [SerializeField] private Transform redSword;
    [SerializeField] private Transform blueSword;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            redSword.position = new Vector3(-redSword.position.x, redSword.position.y, redSword.position.z);
            blueSword.position = new Vector3(-blueSword.position.x, blueSword.position.y, blueSword.position.z);
        }
    }
}
