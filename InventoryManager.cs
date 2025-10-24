using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public TextMeshProUGUI smoreCounter;
    private int smoreCount = 0;

    [System.Serializable]
    public class InventorySlot
    {
        public Image icon;
        public string itemName;
        public TextMeshProUGUI counter;
        public int count = 0;
    }

    public InventorySlot[] slots = new InventorySlot[3];


    public void Increase(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= slots.Length) return;

        slots[slotIndex].count++;
        UpdateUI(slotIndex);

        FlashHighlight(slotIndex, 0.2f);
    }

    public void Decrease(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= slots.Length) return;

        slots[slotIndex].count = Mathf.Max(0, slots[slotIndex].count - 1);
        UpdateUI(slotIndex);
    }

    public void AddItemByName(string itemName)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].itemName == itemName)
            {
                Increase(i);
                return;
            }
        }

        //Debug.LogWarning($"No slot found for item: {itemName}");
    }

    public void AddSmore()
    {
        smoreCount++;
        smoreCounter.text = smoreCount.ToString();
    }

    public void ClearInventory()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].count = 0;
            UpdateUI(i);
        }
    }

    public void highlightSlot(int slotIndex, bool on)
    {
        if(slotIndex < 0 || slotIndex >= slots.Length)
        {
            return;
        }

        InventorySlot slot = slots[slotIndex];
        slot.icon.color = on ? Color.blue : Color.white;
    }

    IEnumerator FlashHighlight(int slotIndex, float duration = 0.3f)
    {
        highlightSlot(slotIndex, true);
        yield return new WaitForSeconds(duration);
        highlightSlot(slotIndex, slots[slotIndex].count > 0);
    }

    private void UpdateUI(int slotIndex)
    {
        var slot = slots[slotIndex];
        slot.counter.text = slot.count.ToString();
    }
}
