using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
[System.Serializable]
public class InventorySlot {
    [Header("UI Reference")]
    [Tooltip("The UI Image that always displays the item icon.")]
    public Image icon;

    [Header("Slot Settings")]
    [Tooltip("The in‐game item associated with this slot (should be a GameObject with a SpriteRenderer).")]
    public GameObject item;

    [Tooltip("If true, this slot is locked and cannot accept a new item.")]
    public bool isLocked = false;
}

public class Inventory : MonoBehaviour {
    [Header("Inventory Setup (Exactly 2 Slots)")]
    [Tooltip("Assign 2 inventory slots with their UI Image (and ensure each Image has a Button component).")]
    public InventorySlot[] slots = new InventorySlot[2];
    private List<GameObject> TpObjects = new List<GameObject>();

    [Header("UI Settings")]
    [Tooltip("Sprite to show when a slot is empty.")]
    public Sprite emptySprite;

    [Header("Player Settings")]
    [Tooltip("The transform to which the active inventory item will be parented so that it follows the player (for example, a hand or attachment point).")]
    public Transform playerFollowPoint;
    private int activeSlotIndex = -1;
    void Update()
    {
        foreach(GameObject toplayer in TpObjects)
        {
            toplayer.transform.position = playerFollowPoint.position;
        }
    }
    void Start() {
        for (int i = 0; i < slots.Length; i++) {
            if (slots[i].icon != null) {
                slots[i].icon.sprite = (slots[i].item != null) ? slots[i].icon.sprite : emptySprite;
            }
            if (slots[i].item != null) {
                slots[i].item.SetActive(false);
            }
            
            Button btn = slots[i].icon.GetComponent<Button>();
            if (btn != null) {
                int index = i;
                btn.onClick.AddListener(() => SelectSlot(index));
            }
        }
    }
    public bool AddItem(GameObject newItem, bool doesTpToPlayer = false) {
        if(doesTpToPlayer)
        {
            TpObjects.Add(newItem);
        }
        if (newItem == null) {
            Debug.LogWarning("Attempted to add a null item.");
            return false;
        }

        SpriteRenderer sr = newItem.GetComponent<SpriteRenderer>();
        if (sr == null) {
            Debug.LogWarning("Item does not have a SpriteRenderer component.");
            return false;
        }

        for (int i = 0; i < slots.Length; i++) {
            if (!slots[i].isLocked && slots[i].item == null) {
                slots[i].item = newItem;
                
                if (slots[i].icon != null) {
                    slots[i].icon.sprite = sr.sprite;
                }
                
                newItem.SetActive(false);
                return true;
            }
        }
        

        Debug.Log("Inventory is full; cannot add item.");
        return false;
    }
    public bool RemoveItem(GameObject item) {
        if (item == null) return false;

        for (int i = 0; i < slots.Length; i++) {
            if (slots[i].item == item) {
                if (activeSlotIndex == i) {
                    DeactivateCurrentItem();
                }

                slots[i].item = null;
                if (slots[i].icon != null) {
                    slots[i].icon.sprite = emptySprite;
                }
                return true;
            }
        }

        Debug.Log("Item not found in inventory.");
        return false;
    }

    public void SelectSlot(int index) {
        if (index < 0 || index >= slots.Length) return;

        InventorySlot slot = slots[index];

        if (slot.isLocked) {
            Debug.Log("This slot is locked.");
            return;
        }
        if (slot.item == null) {
            Debug.Log("No item in this slot.");
            return;
        }

        if (activeSlotIndex != -1 && activeSlotIndex != index) {
            DeactivateCurrentItem();
        }

        activeSlotIndex = index;
        ActivateItem(slot.item);
    }
    private void ActivateItem(GameObject item) {
        if (playerFollowPoint == null) {
            Debug.LogWarning("Player follow point is not set.");
            return;
        }
        item.SetActive(true);
    }
    private void DeactivateCurrentItem() {
        if (activeSlotIndex < 0 || activeSlotIndex >= slots.Length) return;

        InventorySlot slot = slots[activeSlotIndex];
        if (slot.item != null) {
            slot.item.SetActive(false);
        }
        activeSlotIndex = -1;
    }
    public GameObject FindItemByID(int id) {
        for (int i = 0; i < slots.Length; i++) {
            if (slots[i].item != null) {
                Key keyComponent = slots[i].item.GetComponent<Key>();
                if (keyComponent != null && keyComponent.keyID == id) {
                    return slots[i].item;
                }
            }
        }
        return null;
    }
}
