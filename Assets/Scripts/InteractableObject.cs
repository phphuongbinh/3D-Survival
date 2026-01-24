using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool playerInRange;
    public string ItemName;

    public string GetItemName()
    {
        return ItemName;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && playerInRange && SelectionManager.instance.onTarget && SelectionManager.instance.selectedObject == gameObject)
        {

            if (InventorySystem.Instance.CheckSlotsAvailable(1))
            {
                InventorySystem.Instance.AddToInventory(ItemName);
                InventorySystem.Instance.itemPickedup.Add(gameObject.name);
                Destroy(gameObject);

            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}