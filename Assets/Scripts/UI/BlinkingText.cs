using System.Collections;
using TMPro;
using UnityEngine;

public class BlinkingText : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    [Space(10)]
    public float blinkInterval = 1.0f;

    public void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        StartCoroutine(BlinkText());
    }

    IEnumerator BlinkText()
    {
        while (true)
        {
            textMesh.enabled = !textMesh.enabled;

            yield return new WaitForSeconds(blinkInterval);
        }
    }
}