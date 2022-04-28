using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private DataBaseManager dataBase;
    private OrderManager theOrder;
    private SlotInventory[] slots;
    private List<Item> invetoryItemList;
    private List<Item> inventoryTabList;


    public Transform transform; // 슬롯의 부모
    public GameObject go;// 가방 활성화 비활성화
    public GameObject[] selectedTabImage;
    public GameObject prefab_floating_text;

    private int selectedItem;
    private int selectedTab;

    private bool activated;     // 가방 활성화
    private bool tabActivated;  // 탭 활성화
    private bool itemActivated; // 아이템 활성화
    private bool stopkeyInput;  // 키 입력제한
    private bool preventExec;   // 중복실행 제한

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        dataBase = FindObjectOfType<DataBaseManager>();
        theOrder = FindObjectOfType<OrderManager>();
        invetoryItemList = new List<Item>();
        inventoryTabList = new List<Item>();
        slots = transform.GetComponentsInChildren<SlotInventory>();

        invetoryItemList.Add(new Item(10001, "로우 체력 포션", "체력 5회복", Item.ItemType.Use, Item.ItemGrade.Nomal));
        invetoryItemList.Add(new Item(10002, "로우 마나", "마나 5회복", Item.ItemType.Use, Item.ItemGrade.Nomal));
        invetoryItemList.Add(new Item(10001, "로우 체력 포션", "체력 5회복", Item.ItemType.Use, Item.ItemGrade.Nomal));
        invetoryItemList.Add(new Item(10002, "로우 마나", "마나 5회복", Item.ItemType.Use, Item.ItemGrade.Nomal));
    }


    // Update is called once per frame
    void Update()
    {
        if (!stopkeyInput)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                activated = !activated; // true -> false / false->true
                if (activated)
                {
                    theOrder.NotMove();
                    go.SetActive(true);
                    selectedTab = 0;
                    tabActivated = true;
                    itemActivated = false;
                    ShowTab();
                }
                else
                {
                    go.SetActive(false);
                    tabActivated = false;
                    itemActivated = false;
                    theOrder.Move();
                }
            }

            if (activated)
            {
                if (tabActivated)
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if (selectedTab < selectedTabImage.Length - 1)
                        {
                            selectedTab++;
                        }
                        else
                        {
                            selectedTab = 0;
                        }
                        SelectedTab();
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        if (selectedTab > 0)
                        {
                            selectedTab--;
                        }
                        else
                        {
                            selectedTab = selectedTabImage.Length - 1;
                        }
                        SelectedTab();
                    }
                    else if (Input.GetKeyDown(KeyCode.Z))
                    {
                        Color color = selectedTabImage[selectedTab].GetComponent<Image>().color;
                        color.a = 0.25f;
                        selectedTabImage[selectedTab].GetComponent<Image>().color = color;
                        itemActivated = true;
                        tabActivated = false;
                        preventExec = true;
                        ShowItem();
                    }
                }              // 탭 활성화시 키 입력 처리

                else if (itemActivated)
                {
                    if (Input.GetKeyDown(KeyCode.DownArrow)){
                        if (selectedItem < inventoryTabList.Count - 2)
                            selectedItem += 2;
                        else
                            selectedItem %= 2;
                        SelectedItem();
                    }
                    else if (Input.GetKeyDown(KeyCode.UpArrow)){
                        if (selectedItem > 1)
                            selectedItem -= 2;
                        else
                            selectedItem = inventoryTabList.Count - 1 - selectedItem;
                        SelectedItem();
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow)){
                        if (selectedItem > inventoryTabList.Count - 1)
                            selectedItem++;
                        else
                            selectedItem = 0;
                        SelectedItem();
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow)){
                        if (selectedItem > 0)

                            selectedItem--;
                        else
                          selectedItem = inventoryTabList.Count - 1;
                        SelectedItem();
                    }
                    else if (Input.GetKeyDown(KeyCode.Z)&& !preventExec){
                        if (selectedTab == 0){
                            Debug.Log(" Z 활성화 ");
                            stopkeyInput = true;
                            // 물약을 마시겠습니까? 와 같은 선택지 호출
                        }
                        else if (selectedTab == 1)
                        {
                            // 장비 장착
                        }
                        else
                        {
                            // 비프음 출력
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.X)){
                        StopAllCoroutines();
                        itemActivated = false;
                        tabActivated = true;
                        ShowTab();
                    }
                }        // 아이템 활성화시 키 입력처리

                if (Input.GetKeyUp(KeyCode.Z))
                {
                    Debug.Log(" Z 비활성화 ");
                    preventExec = false;
                }
            }
        }
    }

    public void GetAnItem(int _itemID,int _count = 1)
    {
        for(int i = 0; i < dataBase.itemList.Count; i++)        // 데이터베이스 아이템 검색
        {
            if (_itemID == dataBase.itemList[i].itemID)         // 데이터베이스에 아이템 발견
            {
               // var clone = Instantiate(prefab_floating_text,Player.instance.transform.position,Quaternion.Euler(Vector3.zero));
               // clone.GetComponent<FloatingText>().text.text = dataBase.itemList[i].itemName + " " + _count + " 개 획득";
               // clone.transform.SetParent(this.transform);
                for(int j = 0; j < invetoryItemList.Count; j++) // 아이템이 이미 있는지 검색
                {
                    if (invetoryItemList[j].itemID == _itemID)  // 같은 아이템이 있을시 갯수 증가
                    {
                        if (invetoryItemList[j].itemType == Item.ItemType.Use)
                        {
                            invetoryItemList[j].itemCount += _count;
                        }
                        else
                        {
                            invetoryItemList.Add(dataBase.itemList[i]);
                        }
                        return;
                    }
                }
                invetoryItemList.Add(dataBase.itemList[i]);     // 아이템 추가
                return;
            }
        }
        Debug.LogError("데이터베이스에 해당 ID값을 가진 아이템이 존재하지 않습니다.");
    }

    public void ShowItem()
    {
        inventoryTabList.Clear();
        RemoveSlot();
        selectedItem = 0;

        switch (selectedTab)
        {
            case 0:
                for (int i = 0; i < invetoryItemList.Count; i++)
                {
                    if (Item.ItemType.Use == invetoryItemList[i].itemType)
                    {
                        inventoryTabList.Add(invetoryItemList[i]);
                    }

                }
                break;
            case 1:
                for (int i = 0; i < invetoryItemList.Count; i++)
                {
                    if (Item.ItemType.Equip == invetoryItemList[i].itemType)
                    {
                        inventoryTabList.Add(invetoryItemList[i]);
                    }

                }
                break;
            case 2:
                for (int i = 0; i < invetoryItemList.Count; i++)
                {
                    if (Item.ItemType.Quest == invetoryItemList[i].itemType)
                    {
                        inventoryTabList.Add(invetoryItemList[i]);
                    }

                }
                break;
            case 3:
                for (int i = 0; i < invetoryItemList.Count; i++)
                {
                    if (Item.ItemType.ETC == invetoryItemList[i].itemType)
                    {
                        inventoryTabList.Add(invetoryItemList[i]);
                    }

                }
                break;

        } // 탭에 따른 아이템분류 / 그리고 인벤토리 탭 리스트에 추가

        for (int i = 0; i < invetoryItemList.Count; i++)
        {
            slots[i].gameObject.SetActive(true);
            slots[i].Additem(inventoryTabList[i]);
        } // 인벤토리 탭 리스트의 내용을 인벤토리 슬롯에 추가

        SelectedItem();
    }                     // 아이템 활성화
    public void ShowTab()
    {
        RemoveSlot();
        SelectedTab();

    }

    // 탭 활성화
    // 선택된 탭을 제외하고 다른 모든 탭의 컬러 알파값을 0으로 조정
    public void SelectedTab()
    {
        StopAllCoroutines();
        Color color = selectedTabImage[selectedTab].GetComponent<Image>().color;
        color.a = 0f;
        for (int i = 0; i < selectedTabImage.Length; i++)
        {
            selectedTabImage[i].GetComponent<Image>().color = color;
        }
        StartCoroutine(SelectedTabEffectCoriutine());
    } 
    
    
    IEnumerator SelectedTabEffectCoriutine()
    {
        while (tabActivated)
        {
            Color color = selectedTabImage[selectedTab].GetComponent<Image>().color;
            while (color.a < 0.5)
            {
                color.a += 0.03f;
                selectedTabImage[selectedTab].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a += 0.03f;
                selectedTabImage[selectedTab].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            yield return new WaitForSeconds(0.3f);

        }
    }   // 선택된 탭에 반짝임 효과
    public void RemoveSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem();
            slots[i].gameObject.SetActive(false);
        }
    }                   // 인벤토리 슬롯 초기화
    public void SelectedItem()
    {
        StopAllCoroutines();
        if (inventoryTabList.Count < 0)
        {
            Color color = slots[0].selectedItem.GetComponent<Image>().color;
            color.a = 0f;
            for (int i = 0; i < inventoryTabList.Count; i++)
                slots[i].selectedItem.GetComponent<Image>().color = color;
            StartCoroutine(SelectedItemEffectCoriutine());
        }

    }                 // 선택된 아이템을 제외한 아이템 컬러 알파값을 0
    IEnumerator SelectedItemEffectCoriutine()
    {
        while (itemActivated)
        {
            Color color = slots[0].GetComponent<Image>().color;
            while (color.a < 0.5)
            {
                color.a += 0.03f;
                slots[selectedItem].selectedItem.GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a -= 0.03f;
                slots[selectedItem].selectedItem.GetComponent<Image>().color = color;
                yield return waitTime;
            }
            yield return new WaitForSeconds(0.3f);

        }
    }  // 선택된 아이템 반짝임효과
}

