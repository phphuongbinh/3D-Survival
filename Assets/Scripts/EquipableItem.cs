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
            SoundManager.Instance.PlaySound(SoundManager.Instance.toolSwingSound);
            animator.SetTrigger("hit");
        }
    }
    void GetHit()
    {
        GameObject selectedTree = SelectionManager.instance.selectedTree;

        if (selectedTree != null)
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.chopSound);
            selectedTree.GetComponent<ChoppableTree>().GetHit();
        }
    }

    IEnumerator SwingSoundDelay()
    {
        yield return new WaitForSeconds(0.2f);


    }
}
