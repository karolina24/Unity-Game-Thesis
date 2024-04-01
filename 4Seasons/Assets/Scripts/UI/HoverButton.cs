using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // This should be a using directive, not a separate line

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler // Implement the interfaces
{
    public Color normalColor = Color.white;
    public Color hoverColor = Color.grey;

    private Image buttonImage;
    private float delay = 0.2f; // Delay in seconds

    void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Start coroutine to change color after a delay
        StartCoroutine(ChangeColorAfterDelay(hoverColor, delay));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Start coroutine to revert color after a delay
        StartCoroutine(ChangeColorAfterDelay(normalColor, delay));
    }

    IEnumerator ChangeColorAfterDelay(Color color, float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Change the button color
        buttonImage.color = color;
    }
}
