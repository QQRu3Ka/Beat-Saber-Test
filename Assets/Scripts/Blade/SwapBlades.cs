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
            var tmp = redSword.position.x;
            redSword.position = new Vector3(blueSword.position.x, redSword.position.y, redSword.position.z);
            blueSword.position = new Vector3(tmp, blueSword.position.y, blueSword.position.z);
        }
    }
}
