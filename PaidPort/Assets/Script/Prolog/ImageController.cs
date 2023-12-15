using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageController : MonoBehaviour
{
    public Image[] images;

    private int activeImageIndex = -1; // Indeks gambar yang sedang ditampilkan

    void Start()
    {
        // Sembunyikan semua gambar pada awalnya
        ResetImages();
    }

    // Method untuk mengatur gambar ke keadaan awal (sembunyikan semua gambar)
    public void ResetImages()
    {
        foreach (Image img in images)
        {
            img.gameObject.SetActive(false);
        }
        activeImageIndex = -1;
    }

    // Method untuk mengubah gambar yang aktif sesuai dengan indeks teks yang muncul
    public void ChangeImage(int index)
    {
        if (index < images.Length)
        {
            // Sembunyikan gambar sebelumnya
            if (activeImageIndex >= 0 && activeImageIndex < images.Length)
            {
                images[activeImageIndex].gameObject.SetActive(false);
            }

            // Tampilkan gambar sesuai indeks teks yang muncul
            images[index].gameObject.SetActive(true);
            activeImageIndex = index;
        }
        else
        {
            Debug.LogWarning("Indeks teks melebihi jumlah gambar yang disediakan!");
        }
    }
}


