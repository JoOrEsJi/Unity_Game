using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Movement
{
    public float triggerLength = 1;
    public float chaseLenght = 5;
    private bool chasing;
    private bool collidingWPlayer;
    private Transform protaTransform;
    private Vector3 startingPoint;

    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        protaTransform = GameManager.instance.protagonist.transform;
        startingPoint = transform.position;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if(Vector3.Distance(protaTransform.position, startingPoint) < chaseLenght)
        {
            if(Vector3.Distance(protaTransform.position, startingPoint) < triggerLength)
            {
                chasing = true;
            }
            if(chasing)
            {
                if(!collidingWPlayer)
                {
                    UpdateMotor((protaTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(startingPoint - transform.position);
            }
        }
        else
        {
            UpdateMotor(startingPoint - transform.position);
            chasing = false;
        }

        collidingWPlayer = false;
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;
            
            if(hits[i].tag == "Character" && hits[i].name == "Protagonist")
            {
                collidingWPlayer = true;
            }

            hits[i] = null;
        }
    }
}

