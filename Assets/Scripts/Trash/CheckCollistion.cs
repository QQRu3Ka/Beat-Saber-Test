using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollistion : MonoBehaviour
{
    private string _wallTag = "Wall";
    private bool _isInTrigger;

    public string WallTag
    {
        get => _wallTag;
        set => _wallTag = value;
    }

    public bool IsInTrigger
    {
        get => _isInTrigger;
        set => _isInTrigger = value;
    }

    void Update()
    {
        if (IsInTrigger)
        {
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && other.gameObject.CompareTag("RedCube"))
        {
            Destroy(other.gameObject);
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow) && other.gameObject.CompareTag("BlueCube"))
        {
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IsInTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        IsInTrigger = false;
    }
}
