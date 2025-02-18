using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFountain : Collidable
{
    public int healingAmount = 1;
    private float healCooldown = 1.0f;
    private float lastHeal;

    protected override void OnCollide(Collider2D collider)
    {
        if (collider.name != "Protagonist")
            return;

        if(Time.time - lastHeal > healCooldown)
        {
            lastHeal = Time.time;
            GameManager.instance.protagonist.Heal(healingAmount);
        }
    }
}
