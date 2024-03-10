using UnityEngine;

public class MouseController : MonoBehaviour
{
    // The custom cursor texture to use
    public Texture2D cursor;

    // Flag to track if the mouse is currently visible
    private bool isMouseVisible;

    void Start()
    {
        // Set the custom cursor texture
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
        // Initialize mouse visibility state to true (visible)
        isMouseVisible = true;
    }

    // Method to toggle showing/hiding the mouse cursor
    public void OnShowMouse()
    {
        // Toggle the flag for mouse visibility
        isMouseVisible = !isMouseVisible;

        // If the mouse is now visible
        if (isMouseVisible)
        {
            // Set the cursor to be visible
            Cursor.visible = true;
            // Set the cursor lock state to None (unlocked)
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            // If the mouse is now hidden
            // Set the cursor to be invisible
            Cursor.visible = false;
            // Set the cursor lock state to Locked (hidden and confined to the window)
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
