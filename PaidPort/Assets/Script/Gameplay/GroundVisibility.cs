using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundVisibility : MonoBehaviour
{
    public SpriteRenderer groundSpriteRenderer;
    public Sprite originalSprite;
    public Sprite blackSprite;
    public Sprite overlaySprite;

    private bool isWithinVision = true;
    private Collider2D playerCollider;

    private float visionRange = 5f;
    private float overlayRadius = 1f;

    private float permanentSeenRadius = 1.5f;

    private bool hasBeenSeen = false;
    private bool permanentSeen = false;

    private void Start()
    {
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
    }

    private void Update()
    {

        float distanceToPlayer = Vector2.Distance(transform.position, playerCollider.transform.position);

        if (!permanentSeen && (distanceToPlayer <= permanentSeenRadius || distanceToPlayer <= overlayRadius))
        {
            permanentSeen = true;
        }

        if (permanentSeen)
        {

            if (!hasBeenSeen)
            {
                hasBeenSeen = true;
                groundSpriteRenderer.sprite = originalSprite;
            }
        }
        else if (distanceToPlayer <= visionRange)
        {

            if (!isWithinVision)
            {
                groundSpriteRenderer.sprite = originalSprite;
                isWithinVision = true;
            }

            if (distanceToPlayer <= (visionRange - overlayRadius))
            {

                groundSpriteRenderer.sprite = originalSprite;
            }
            else
            {

                groundSpriteRenderer.sprite = overlaySprite;
            }
        }
        else
        {

            if (isWithinVision)
            {
                groundSpriteRenderer.sprite = blackSprite;
                isWithinVision = false;
            }
        }
    }
}