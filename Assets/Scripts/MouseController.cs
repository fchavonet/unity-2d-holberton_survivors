using UnityEngine;

public class MouseController : MonoBehaviour
{
    public Texture2D cursor;

    private bool isMouseVisible = false;

    void Start()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isMouseVisible = false;
    }

    public void OnShowMouse()
    {
        isMouseVisible = !isMouseVisible;

        if (isMouseVisible)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
