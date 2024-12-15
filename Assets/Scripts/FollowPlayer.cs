using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform follow;
    public float boundX = 0.15f;
    public float boundY = 0.15f;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        float deltaX = follow.position.x - transform.position.x;
        if (deltaX > boundX || deltaX < -boundX)
            if (transform.position.x < follow.position.x)
                delta.x = deltaX - boundX;
            else
                delta.x = deltaX + boundX;

        float deltaY = follow.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
            if (transform.position.y < follow.position.y)
                delta.y = deltaY - boundY;
            else
                delta.y = deltaY + boundY;

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
