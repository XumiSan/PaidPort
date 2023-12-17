using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingControl : MonoBehaviour
{
    [SerializeField]
    private GameObject EndSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Control()
    {
        Audioplayer.instance.PlaySFX(11);
    }

    void cheers()
    {
        Audioplayer.instance.PlaySFX(9);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
