using UnityEngine;

public class Food : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        if (Crafting.Instance != null)
        {
            Crafting.Instance.AddFood(gameObject.name); 
        }
        else
        {
            Debug.Log("oopsie");
        }

        Destroy(gameObject);
    }
}
