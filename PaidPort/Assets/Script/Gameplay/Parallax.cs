using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour

{
    public Transform player; 
    private float offsetYThreshold = 5f; 
    public float parallaxSpeed = 0.5f;

    private float initialY;

    private void Start()
    {
        
        initialY = transform.position.y;
    }

    private void Update()
    {
        if (player.position.y >= offsetYThreshold)
        {
           
            float differenceY = player.position.y - initialY;

           
            float targetY = initialY + differenceY * parallaxSpeed;

            
            transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
        }
        else if (player.position.y < offsetYThreshold)
        {
           
           
        }
    }
}



