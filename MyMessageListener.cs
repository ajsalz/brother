using System.Text.RegularExpressions;
using UnityEngine;

public class MyMessageListener : MonoBehaviour
{
    [SerializeField] private Boat boat;

    public static float horizontal = 0f;
    public static float vertical = 0f;
    public static float mag = 0;

    private float holdTime = .4f;
    private float releaseTime = 0f;

    void OnMessageArrived(string msg)
    {
        //Debug.Log("Arrived: " + msg);
        msg = msg.Trim();

        if (msg == null)
        {
            return;
        }
        if(msg.Length > 4)
        {
            return;
        }

        ParseSerialMessage(msg);
    }

    /* Parse Code not mine because I'm lazy (jk kind of)
     * Prompt: Can I have a function that will parse a message that looks like X
     * and store to three different values of types: string, int, and string?
     */

    void ParseSerialMessage(string msg)
    {

        Match match = Regex.Match(msg, @"([LR])([FB])(\d+)");

        if (match.Success)
        {
            string direction = match.Groups[1].Value; // Left or Right
            string movement = match.Groups[2].Value; // Forward or Backward
            string magnitudeString = match.Groups[3].Value; // 2-9?

            if (int.TryParse(magnitudeString, out int parsedMagnitude))
            {
                float magnitude = parsedMagnitude;
                mag = magnitude;
                //Debug.Log("Parsing...");
            }
            else
            {
                Debug.LogWarning("what if I killed myself");
            }

            Debug.Log($"Direction: {direction}, Magnitude: {mag}, Movement: {movement}");

            if(direction == "L")
            {
                horizontal = -1f;
            }
            else if(direction == "R")
            {
                horizontal = 1f;
            }

            if(movement == "F")
            {
                vertical = 1f;
            }
            else if(movement == "B")
            {
                vertical = -1f;
            }
            releaseTime = Time.time + holdTime;
        }
        else
        {
            Debug.LogWarning("Invalid serial message: " + msg);
        }
    }

    void OnConnectionEvent(bool success) => Debug.Log(success ? "Device Connected" : "Device Disconnected");

    void Update()
    { 
        if(Time.time > releaseTime)
        {
            horizontal = 0f;
            vertical = 0f;
            mag = 0;
        }
    }
}


