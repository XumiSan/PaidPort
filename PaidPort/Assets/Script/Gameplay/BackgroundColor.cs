using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColor : MonoBehaviour

{
    private Camera backgroundCamera;

    void Start()
    {
        
        backgroundCamera = GetComponent<Camera>();
       
    }

    void Update()
    {
        
    }

    
   public void ChangeBackgroundColor(Color color)
    {
        backgroundCamera.backgroundColor = color;
    }
}


