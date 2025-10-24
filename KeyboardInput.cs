using System.Diagnostics;
using UnityEngine;

public class KeyboardInput : InputManager
{
    private float movementX = 1f;
    private float movementY = 1f;
    // Will be a set value since we're reading that in from Arduino

    public Vector2 ProcessInput()
    {
        Vector2 direction = Vector2.zero;

        if (Input.GetKey(KeyCode.O))
        {
            direction = new Vector2(movementX, movementY);
        }
        if (Input.GetKey(KeyCode.I))
        {
            direction = new Vector2(-movementX, movementY);
        }
        if (Input.GetKey(KeyCode.K))
        {
            direction = new Vector2(-movementX, -movementY);
        }
        if (Input.GetKey(KeyCode.L))
        {
            direction = new Vector2(movementX, -movementY);
        }

        return direction.normalized;
    }

    public float Magnitude()
    {
        float magnitude = 400f;
        return magnitude;
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
