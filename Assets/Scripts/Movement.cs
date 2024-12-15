using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : Character
{
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    protected float ySpeed = 0.75f;
    protected float xSpeed = 0.75f;

    private Animator animator;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed);

        //Si el input cambia de sentido, el sprite tambi�n
        if (moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        //Si el boxcollider no da permiso, el jugador no puede pasar por ah�
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Characters", "Blocking"));
        if (hit.collider == null)
            //Movimiento
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Characters", "Blocking"));
        if (hit.collider == null)
            //Movimiento
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);

        if(moveDelta.x != 0 || moveDelta.y != 0)
        {
            animator.SetFloat("X", moveDelta.x);
            animator.SetFloat("Y", moveDelta.y);

            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
       
    }
}
