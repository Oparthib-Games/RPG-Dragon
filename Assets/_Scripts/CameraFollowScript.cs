using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    [SerializeField] GameObject target;

    void Update()
    {
        transform.position = target.transform.position;
    }
}
