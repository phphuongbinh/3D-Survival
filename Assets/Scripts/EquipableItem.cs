using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class EquipableItem : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)
            && InventorySystem.Instance.isOpen == false
            && CraftingSystem.instance.isOpen == false
            && SelectionManager.instance.handIsVisible == false)
        {

            GameObject selectedTree = SelectionManager.instance.selectedTree;

            if (selectedTree != null)
            {
                selectedTree.GetComponent<ChoppableTree>().GetHit();
            }
            animator.SetTrigger("hit");

        }
    }
}
