using System;
using UnityEngine;

public class PaddleInput : MonoBehaviour, InputManager
{
    private MyMessageListener messageListener; 

    private void Start()
    {

        messageListener = GetComponent<MyMessageListener>();

        if (messageListener == null)
        {
            Debug.LogError("she's not listening!");
        }
    }

    public Vector2 ProcessInput()
    {
        //Debug.Log("AYYYYY MISS BOAT IS HOOKED UP");
        Vector2 direction = Vector2.zero;

        float x = MyMessageListener.horizontal;
        float y = MyMessageListener.vertical;
        direction = new Vector2(x, y).normalized;
        return direction;
    }

    public float Magnitude()
    {
        float magnitude = MyMessageListener.mag;
        return magnitude;
    }
}