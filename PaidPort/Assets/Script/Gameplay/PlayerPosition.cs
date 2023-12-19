using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPosition : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI positionText;
   
    public Transform playerTransform; 

    private float lastYPosition;

    // Start is called before the first frame update
    void Start()
    {
        lastYPosition = playerTransform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //Memanggil fungsi PositionPlayer
        PosisionPlayer();
    }
    private void PosisionPlayer()
    {
        if (positionText != null && playerTransform != null)
        {
            //Mendapatkan posisi y pemain
            float playerY = playerTransform.position.y;

            if (playerY != lastYPosition)
            {
                // Update teks UI dengan angka posisi pemain hanya jika terjadi perubahan pada y
                positionText.text = Mathf.RoundToInt(playerY) + "Ft";
                // Memperbarui nilai posisi y
                lastYPosition = playerY;
            }
        }
    }
}
