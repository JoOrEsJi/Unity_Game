using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class SpriteMaskController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer playerSpriteRenderer;

    [SerializeField]
    private SpriteMask spriteMask;

    private Collider2D spriteMaskCollider;

    private List<SpriteRenderer> otherRenders = new List<SpriteRenderer>();

    public bool checking = false;
    private void Awake()
    {
        spriteMaskCollider = GetComponent<Collider2D>();
        spriteMaskCollider.isTrigger = true;
    }
    private void Update()
    {
        if(checking)
        {
            foreach (SpriteRenderer renderer in otherRenders)
            {
                if (
                    playerSpriteRenderer.sortingLayerName == renderer.sortingLayerName
                    && playerSpriteRenderer.sortingOrder <= renderer.sortingOrder
                    && playerSpriteRenderer.transform.position.y > renderer.transform.position.y)
                {
                    spriteMask.enabled = true;
                    playerSpriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                    return;
                }
                else
                {
                    spriteMask.enabled = false;
                    playerSpriteRenderer.maskInteraction = SpriteMaskInteraction.None;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger == false)
            return;
        SpriteRenderer spriteRenderer = collision.GetComponent<SpriteRenderer>();
        if(spriteRenderer != null)
        {
            otherRenders.Add(spriteRenderer);
            checking = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger == false)
            return;
        SpriteRenderer spriteRenderer = collision.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            otherRenders.Remove(spriteRenderer);
            if(otherRenders.Count <= 0)
            {
                checking = false;
                spriteMask.enabled = false;
                playerSpriteRenderer.maskInteraction = SpriteMaskInteraction.None;
            }
        }
    }
}
