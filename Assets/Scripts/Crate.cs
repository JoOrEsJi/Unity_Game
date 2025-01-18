using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Character
{
    protected override void Death()
    {
        Destroy(gameObject);
    }
}
