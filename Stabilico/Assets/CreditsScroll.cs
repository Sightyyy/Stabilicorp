using UnityEngine;

public class CreditsScroll : MonoBehaviour
{
    public RectTransform creditsText;   // RectTransform teks credits
    public RectTransform maskArea;      // RectTransform area mask
    public float scrollSpeed = 50f;     // Kecepatan scrolling
    public float resetDelay = 1f;       // Waktu delay sebelum scroll diulang

    public GameObject nextObject;       // Objek yang diaktifkan setelah scrolling selesai
    public GameObject creditsObject;    // Objek credits yang dinonaktifkan setelah selesai

    private bool isScrolling = true;
    private Vector2 initialPosition;    // Posisi awal teks credits
    private bool isResetting = false;   // Flag untuk menunda reset posisi

    private void Start()
    {
        initialPosition = creditsText.anchoredPosition; // Simpan posisi awal
        Debug.Log($"CreditsScroll initialized. Initial position: {initialPosition}");
        isScrolling = true;
    }

    private void Update()
    {
        if (isScrolling)
        {
            // Geser teks ke atas
            creditsText.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

            // Hitung posisi akhir
            float endPositionY = creditsText.rect.height + maskArea.rect.height;

            // Periksa apakah teks sudah melewati batas akhir
            if (creditsText.anchoredPosition.y >= endPositionY && !isResetting)
            {
                isResetting = true;
                Invoke("ResetCreditsPosition", resetDelay);  // Tunggu sebentar sebelum reset
            }
        }
    }

    public void SkipCredits()
    {
        // Pindahkan posisi teks langsung ke akhir
        float endPositionY = creditsText.rect.height + maskArea.rect.height;
        creditsText.anchoredPosition = new Vector2(creditsText.anchoredPosition.x, endPositionY);
        StopScrolling();
    }

    private void StopScrolling()
    {
        isScrolling = false;

        // Aktifkan objek berikutnya jika tersedia
        if (nextObject != null)
        {
            nextObject.SetActive(true);
        }

        // Nonaktifkan objek credits jika tersedia
        if (creditsObject != null)
        {
            creditsObject.SetActive(false);
        }

        // Kembalikan posisi teks ke posisi awal
        ResetCreditsPosition();
    }

    private void ResetCreditsPosition()
    {
        creditsText.anchoredPosition = initialPosition;
        Debug.Log($"Credits position reset to initial position: {initialPosition}");

        // Mulai scroll ulang
        isScrolling = true;
        isResetting = false;
    }
}
