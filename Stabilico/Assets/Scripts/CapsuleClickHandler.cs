using UnityEngine;
using UnityEngine.EventSystems;

public class CapsuleClickHandler : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Kapsul di-klik!");
        // Tambahkan logika lain di sini (misalnya, animasi atau efek)
    }
}
