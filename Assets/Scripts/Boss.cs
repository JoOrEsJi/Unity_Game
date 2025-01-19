using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public float bombSpeed = 2.5f;
    public float distance = 0.5f;
    public Transform bomb;

    private void Update()
    {
        bomb.position = transform.position + new Vector3(-Mathf.Cos(Time.time * bombSpeed) * distance, Mathf.Sin(Time.time * bombSpeed) * distance, 0);
    }
}
