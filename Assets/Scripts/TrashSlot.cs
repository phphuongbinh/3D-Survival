using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrashSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject trashAlertUI;
    private Text textToModify;

    public Sprite trash_closed;
    public Sprite trash_opened;

    private Image imageComponent;
    Button YesButton, NoButton;

    GameObject draggedItem
    {
        get
        {
            return DragDrop.itemBeingDragged;
        }
    }

    GameObject itemToBeDeleted;

    public string itemName
    {
        get
        {
            string name = itemToBeDeleted.name;
            string toRemove = "(Clone)";
            string result = name.Replace(toRemove, "");
            return result;
        }
    }

    void Start()
    {
        imageComponent = transform.Find("background").GetComponent<Image>();

        textToModify = trashAlertUI.transform.Find("question").GetComponent<Text>();

        YesButton = trashAlertUI.transform.Find("yes").GetComponent<Button>();
        YesButton.onClick.AddListener(delegate { DeleteItem(); });

        NoButton = trashAlertUI.transform.Find("no").GetComponent<Button>();
        NoButton.onClick.AddListener(delegate { CancelDeletion(); });

    }

    public void OnDrop(PointerEventData eventData)
    {
        if (draggedItem.GetComponent<InventoryItem>().isTrashable == true)
        {
            itemToBeDeleted = draggedItem.gameObject;
            StartCoroutine(notifyBeforeDeletion());
        }
    }

    IEnumerator notifyBeforeDeletion()
    {
        trashAlertUI.SetActive(true);
        textToModify.text = "Bạn có muốn bỏ " + itemName + "?";
        yield return new WaitForSeconds(1f);
    }

    private void CancelDeletion()
    {
        imageComponent.sprite = trash_closed;
        trashAlertUI.SetActive(false);
    }

    private void DeleteItem()
    {
        imageComponent.sprite = trash_closed;
        DestroyImmediate(itemToBeDeleted.gameObject);
        InventorySystem.Instance.ReCalculateList();
        CraftingSystem.instance.RefeshNeededItems();
        trashAlertUI.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (draggedItem != null && draggedItem.GetComponent<InventoryItem>().isTrashable == true)
        {
            imageComponent.sprite = trash_opened;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (draggedItem != null && draggedItem.GetComponent<InventoryItem>().isTrashable == true)
        {
            imageComponent.sprite = trash_closed;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
