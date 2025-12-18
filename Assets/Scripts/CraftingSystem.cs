using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    public static CraftingSystem instance { get; set; }

    public GameObject craftingScreenUI;
    public GameObject toolsScreenUI, survivalScreenUI, refineScreenUI;
    public List<string> inventoryItemList;

    // Category Buttons
    Button toolsBTN, survivalBTN, refineBTN;

    // Craft Buttons
    Button craftAxeBTN, craftingPlankBTN;

    // Requirement Text
    Text AxeReq1, AxeReq2, PlankReq1;
    public bool isOpen;

    // All Blueprint
    public Blueprint AxeBLP = new Blueprint("Axe", 1, 2, "Stone", 3, "Stick", 3);
    public Blueprint PlankBLP = new Blueprint("Plank", 2, 2, "Log", 1, "", 0);

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        isOpen = false;

        toolsBTN = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate { OpenToolsCategory(); });

        survivalBTN = craftingScreenUI.transform.Find("SurvivalButton").GetComponent<Button>();
        survivalBTN.onClick.AddListener(delegate { OpenSurvivalCategory(); });

        refineBTN = craftingScreenUI.transform.Find("RefineButton").GetComponent<Button>();
        refineBTN.onClick.AddListener(delegate { OpenRefineCategory(); });

        //Axe
        AxeReq1 = toolsScreenUI.transform.Find("Axe").transform.Find("req1").GetComponent<Text>();
        AxeReq2 = toolsScreenUI.transform.Find("Axe").transform.Find("req2").GetComponent<Text>();

        craftAxeBTN = toolsScreenUI.transform.Find("Axe").transform.Find("Button").GetComponent<Button>();
        craftAxeBTN.onClick.AddListener(delegate { CraftAnyItem(AxeBLP); });

        // Plank
        PlankReq1 = refineScreenUI.transform.Find("Plank").transform.Find("req1").GetComponent<Text>();

        craftingPlankBTN = refineScreenUI.transform.Find("Plank").transform.Find("Button").GetComponent<Button>();
        craftingPlankBTN.onClick.AddListener(delegate { CraftAnyItem(PlankBLP); });

    }

    void OpenToolsCategory()
    {
        craftingScreenUI.SetActive(false);
        refineScreenUI.SetActive(false);
        survivalScreenUI.SetActive(false);

        toolsScreenUI.SetActive(true);

    }
    void OpenSurvivalCategory()
    {
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(false);
        refineScreenUI.SetActive(false);

        survivalScreenUI.SetActive(true);
    }
    void OpenRefineCategory()
    {
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(false);
        survivalScreenUI.SetActive(false);

        refineScreenUI.SetActive(true);
    }

    void CraftAnyItem(Blueprint blueprintToCraft)
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.craftingSound);

        StartCoroutine(craftedDelayForSound(blueprintToCraft));


        // InventorySystem.Instance.AddToInventory(blueprintToCraft.itemName);


        if (blueprintToCraft.numOfRequirements == 1)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);
        }
        else if (blueprintToCraft.numOfRequirements == 2)
        {

            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req2, blueprintToCraft.Req2amount);
        }

        StartCoroutine(calcualte());


    }

    private IEnumerator craftedDelayForSound(Blueprint blueprintToCraft)
    {
        yield return new WaitForSeconds(6f);
        for (var i = 0; i < blueprintToCraft.numberOfItemsProduce; i++)
        {
            InventorySystem.Instance.AddToInventory(blueprintToCraft.itemName);
        }
    }

    void Update()
    {
        // RefeshNeededItems();
        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {

            craftingScreenUI.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SelectionManager.instance.DisableSelection();
            SelectionManager.instance.GetComponent<SelectionManager>().enabled = false;

            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolsScreenUI.SetActive(false);
            survivalScreenUI.SetActive(false);
            refineScreenUI.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            SelectionManager.instance.EnableSelection();
            SelectionManager.instance.GetComponent<SelectionManager>().enabled = true;

            isOpen = false;
        }
    }


    public IEnumerator calcualte()
    {
        yield return 0;
        InventorySystem.Instance.ReCalculeList();
        RefeshNeededItems();
    }

    public void RefeshNeededItems()
    {
        int stone_count = 0;
        int stick_count = 0;
        int log_count = 0;

        inventoryItemList = InventorySystem.Instance.itemList;

        foreach (string itemName in inventoryItemList)
        {
            switch (itemName)
            {
                case "Stone":
                    stone_count += 1;
                    break;
                case "Stick":
                    stick_count += 1;
                    break;
                case "Log":
                    log_count += 1;
                    break;
            }
        }

        // Axe 
        AxeReq1.text = "3 Stone [" + stone_count + "]";
        AxeReq2.text = "3 Stick [" + stick_count + "]";

        if (stone_count >= 3 && stick_count >= 3 && InventorySystem.Instance.CheckSlotsAvailable(1))
        {
            craftAxeBTN.gameObject.SetActive(true);
        }
        else
        {
            craftAxeBTN.gameObject.SetActive(false);

        }

        // Plank 
        PlankReq1.text = "1 Log [" + log_count + "]";

        if (log_count >= 1 && InventorySystem.Instance.CheckSlotsAvailable(2))
        {

            craftingPlankBTN.gameObject.SetActive(true);
        }
        else
        {
            craftingPlankBTN.gameObject.SetActive(false);

        }
    }

}
