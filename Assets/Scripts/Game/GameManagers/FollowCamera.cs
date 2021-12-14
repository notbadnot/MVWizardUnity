using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = Vector3.back * 5;

    void Update()
    {
     if (target != null)
        {
            transform.position = target.position + offset;
        }   
    }

    public void SetTarget( Transform newtarget)
    {
        target = newtarget;
    }
}
