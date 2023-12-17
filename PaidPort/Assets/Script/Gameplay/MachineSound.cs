using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineSound : MonoBehaviour
{
    //public GameObject footstep;
    void Start()
    {
       // footstep.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.DownArrow))
        {
            //stopFootstep();
            //Audioplayer.instance.StopSpecificSFX(9);
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            //stopFootstep();
            //Audioplayer.instance.StopSpecificSFX(9);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //footStep();
            Audioplayer.instance.PlaySFX(10);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            //footStep();
            Audioplayer.instance.PlaySFX(10);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            //stopFootstep();
            Audioplayer.instance.StopSpecificSFX(10);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            //stopFootstep();
            Audioplayer.instance.StopSpecificSFX(10);
        }
    }
    void footStep()
    {
       // footstep.SetActive(true);
    }
    void stopFootstep()
    {
        //footstep.SetActive(false);
    }
}
