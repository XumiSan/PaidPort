using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
   {
    public Transform playerTransform; 
    private void SavePosition()
    {
        
        PlayerPrefs.SetFloat("PlayerPosX", playerTransform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", playerTransform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", playerTransform.position.z);

       
        PlayerPrefs.Save();

        Debug.Log("Player position saved!");
    }

    public void SaveButton()
    {
        SavePosition();
    }
}


