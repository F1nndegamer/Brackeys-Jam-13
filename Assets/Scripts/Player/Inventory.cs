using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Reflection;

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

    [Tooltip("Unique 4-digit code for identifying the item.")]
    public int itemCode;

    [Tooltip("Keeps the location where the item was taken so that the player can go there again when they die.")]
    public Vector2 itemWasTaken;

    [Tooltip("The Location where the object goes to when activated.")]
    public Transform FollowLocation;
}

public class Inventory : MonoBehaviour {
    [Header("Inventory Setup (Exactly 2 Slots)")]
    public InventorySlot[] slots = new InventorySlot[2];
    private Dictionary<GameObject, bool> TpObjects = new Dictionary<GameObject, bool>();

    [Header("UI Settings")]
    public Sprite emptySprite;

    [Header("Player Settings")]
    public Transform playerFollowPoint;
    private int activeSlotIndex = -1;
    public static Inventory instance;

    void Update() {
        foreach (GameObject toplayer in TpObjects.Keys) {
            toplayer.transform.position = slots[activeSlotIndex].FollowLocation.position;
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

    public bool AddItem(GameObject newItem, int itemCode, bool doesTpToPlayer = false) {
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
                TpObjects.Add(newItem, false);
                slots[i].item = newItem;
                slots[i].itemCode = itemCode;
                slots[i].itemWasTaken = newItem.transform.position;

                if (slots[i].icon != null) {
                    slots[i].icon.sprite = sr.sprite;
                }

                // Dynamically assign FollowLocation from item's script
                MonoBehaviour[] itemScripts = newItem.GetComponents<MonoBehaviour>();
                foreach (MonoBehaviour script in itemScripts) {
                    FieldInfo field = script.GetType().GetField("TpPos");
                    if (field != null && field.FieldType == typeof(Transform)) {
                        slots[i].FollowLocation = (Transform)field.GetValue(script);
                        Debug.Log($"Assigned FollowLocation from {script.GetType().Name} for item {newItem.name}");
                        break;
                    }
                }

                newItem.SetActive(false);
                return true;
            }
        }

        Debug.Log("Inventory is full; cannot add item.");
        return false;
    }

    private void AdjustImageAspect(Image image, Sprite sprite) {
        if (image == null || sprite == null) return;

        RectTransform rectTransform = image.rectTransform;
        float spriteWidth = sprite.texture.width;
        float spriteHeight = sprite.texture.height;
        float aspectRatio = spriteWidth / spriteHeight;

        if (aspectRatio > 1) {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.x / aspectRatio);
        } else {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.y * aspectRatio, rectTransform.sizeDelta.y);
        }
    }

    public bool RemoveItem(GameObject item) {
        if (item == null) return false;

        for (int i = 0; i < slots.Length; i++) {
            if (slots[i].item == item) {
                if (activeSlotIndex == i) {
                    DeactivateCurrentItem();
                }

                slots[i].item = null;
                slots[i].itemCode = 0;
                slots[i].itemWasTaken = Vector2.zero;
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
        if (activeSlotIndex == index) {
            Debug.Log("Deselecting slot: " + index);
            DeactivateCurrentItem();
            return;
        }
        if (activeSlotIndex != -1) {
            DeactivateCurrentItem();
        }
        Debug.Log("Selecting slot: " + index);
        activeSlotIndex = index;
        ActivateItem(slot.item);
    }

    private void ActivateItem(GameObject item) {
        if (slots[activeSlotIndex].FollowLocation == null) {
            Debug.LogWarning("FollowLocation is not set.");
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

    public GameObject FindItemByCode(int code) {
        for (int i = 0; i < slots.Length; i++) {
            if (slots[i].item != null && slots[i].itemCode == code) {
                return slots[i].item;
            }
        }
        return null;
    }

    public GameObject GetActiveItem() {
        if (activeSlotIndex < 0 || activeSlotIndex >= slots.Length) {
            return null;
        }
        return slots[activeSlotIndex].item;
    }

    public void DropItem() {
        if (activeSlotIndex < 0 || activeSlotIndex >= slots.Length) {
            Debug.Log("No item is currently selected to drop.");
            return;
        }

        InventorySlot slot = slots[activeSlotIndex];
        if (slot.item != null) {
            slot.item.transform.position = slots[activeSlotIndex].FollowLocation.position;
            slot.item.SetActive(true);
            TpObjects.Remove(slot.item);
            slot.item = null;
            slot.itemCode = 0;
            slot.icon.sprite = emptySprite;
            activeSlotIndex = -1;
        }
    }

    public void DieDropItem() {
        for (int i = 0; i < slots.Length; i++) {
            InventorySlot slot = slots[i];
            if (slot.item != null) {
                bool y;
                bool x = TpObjects.TryGetValue(slot.item, out y);
                if (!y) {
                    slot.item.transform.position = slot.itemWasTaken;
                    slot.item.SetActive(true);
                    TpObjects.Remove(slot.item);
                    slot.item = null;
                    slot.itemCode = 0;
                    slot.icon.sprite = emptySprite;
                    activeSlotIndex = -1;
                }
            }
        }
    }

    public void SaveItem() {
        GameObject[] x = new GameObject[2];
        for (int i = 0; i < slots.Length; i++) {
            x[i] = slots[i].item;
        }
        foreach (var key in TpObjects.Keys.ToList()) {
            TpObjects[key] = x.Contains(key);
        }
    }
}
