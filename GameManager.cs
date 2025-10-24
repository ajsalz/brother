using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public InventoryManager player1Inventory;
    public InventoryManager player2Inventory;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
