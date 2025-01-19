using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protagonist : Movement
{
    private bool isAlive = true;
    protected override void Death()
    {
        isAlive = false;

        if (GameManager.instance.deathMenuAnimation != null && GameManager.instance.deathMenuAnimation.gameObject.activeInHierarchy)
        {
            GameManager.instance.deathMenuAnimation.SetTrigger("Showing");
        }
        else if (GameManager.instance.deathMenuAnimation != null)
        {
            GameManager.instance.deathMenuAnimation.gameObject.SetActive(true);
            GameManager.instance.deathMenuAnimation.SetTrigger("Showing");
        }
        else
        {
            Debug.LogError("El Animator del menú de muerte no está asignado en el GameManager.");
        }
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if (isAlive)
        {
            UpdateMotor(new Vector3(x, y, 0));
        }
    }

    public void OnLevelUp()
    {
        maxHitpoint++;
        hitpoint = maxHitpoint;
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            OnLevelUp();
        }
    }

    public void Heal(int healingAmount)
    {
        hitpoint += healingAmount;
        if (hitpoint == maxHitpoint)
        {
            return;
        }
        hitpoint += healingAmount;
        if (hitpoint > maxHitpoint)
        {
            hitpoint = maxHitpoint;
            GameManager.instance.ShowText("+" + healingAmount.ToString() + "hp", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
        }    
    }
}
