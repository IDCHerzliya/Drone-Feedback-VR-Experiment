using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotorSpin : MonoBehaviour
{
    [SerializeField]
    private float speed = 2000;
    void Update()
    {
        var angle = transform.rotation.eulerAngles;
        angle.y += (speed * Time.deltaTime )% 360;
        transform.rotation = Quaternion.Euler(angle);
    }
}
