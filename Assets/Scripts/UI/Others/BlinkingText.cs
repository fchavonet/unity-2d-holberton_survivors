using System.Collections;
using TMPro;
using UnityEngine;

public class BlinkingText : MonoBehaviour
{
    [Space(10)]
    // Interval for blinking.
    public float blinkInterval = 1.0f;

    // TextMeshPro component.
    private TextMeshProUGUI textMesh;

    public void Start()
    {
        // Get the TextMeshPro component.
        textMesh = GetComponent<TextMeshProUGUI>();
        // Start blinking coroutine.
        StartCoroutine(BlinkText());
    }

    // Coroutine for blinking text.
    IEnumerator BlinkText()
    {
        while (true)
        {
            // Toggle text visibility.
            textMesh.enabled = !textMesh.enabled;
            // Wait for blink interval.
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
