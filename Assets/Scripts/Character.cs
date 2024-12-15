using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int hitpoint = 10;
    public int maxHitpoint = 10;
    public float pushRecoverySpeed = 0.2f;

    protected float immunity = 1.0f;
    protected float lastImmune;

    protected Vector3 pushDirection;

    //Implementar funciones para sistema de damage y muerte.
}
