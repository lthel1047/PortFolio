using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuctionShop : MonoBehaviour
{
    public static AuctionShop instance;

    private OrderManager orderManager;
    private AuctionShopSlot[] _slots;

    private LoadSaveManager_ dbManager;
    private List<AuctionItemManager> _ItemSlotAuction;    // 슬롯 목록
    private List<AuctionItemManager> _AuctionList;        // 등록된 아이템 리스트

    public Transform _transform;            //  슬롯의 부모객체.
    public GameObject _gameObject;          // 경매창 활성화 / 비활성화.

    public Transform NPC_transform;         // NPC 도움말
    public GameObject NPC_HelpKEY;          // NPC 도움말

    private int page;
    private int slotCount;
    private const int MAX_SLOTS_COUNT = 12;

    private int selectedItem;               // 선택된 아이템.
    private int auctionTab;                 // 경매장 아이템 보여주기 위한 값

    private bool activated;                 // 인벤토리 활성화시 true.
    private bool itemActivated;           // 아이템 활성화시 true.
    private bool stopKeyInput;              // 아이템 구매시 확인 질문시 다른 키 입력 방지
    private bool preventExec;               // 중복실행 방지
    private bool overlap;                   // NPC 와 겹쳐질시 콜라이더 작동 

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    void Start()
    {
        instance = this;

        orderManager = FindObjectOfType<OrderManager>();

        _slots = _transform.GetComponentsInChildren<AuctionShopSlot>(); // 추가되는 슬롯들을 모두 관리

        dbManager = FindObjectOfType<LoadSaveManager_>();
        _ItemSlotAuction = new List<AuctionItemManager>();
        _AuctionList = new List<AuctionItemManager>();

        //auctionItemList.Add(new AuctionItem(20001, "목검", "목검", AuctionItem.AuctionItemType.Eqip, AuctionItem.AuctionItemGrade.Nomal, "1000"));

    }

    void Update()
    {

        if (overlap == true)
        {
            if (!stopKeyInput)
            {
                if (Input.GetKeyDown(KeyCode.F1))
                {
                    for (int i = 0; i < _AuctionList.Count; i++)
                    {

                        // 경매장 창이 열릴때 DB에서 아이템을 불러와 추가 및 특정 아이템 if 문으로 거름망

                    }
                    activated = !activated; // true -> false / false->true
                    if (activated)
                    {
                        orderManager.NotMove();
                        _gameObject.SetActive(true);
                        auctionTab = 0;
                        itemActivated = true;
                        ShowAuctionItem();
                    }
                    else
                    {
                        _gameObject.SetActive(false);
                        itemActivated = false;
                        orderManager.Move();
                    }
                }

                if (activated)
                {
                    if (itemActivated)
                    {
                        if (Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            if (selectedItem < _ItemSlotAuction.Count - 1)
                                selectedItem += 1;
                            else
                                selectedItem %= 2;

                            SelectedAuctionItem();
                        }
                        else if (Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            if (selectedItem > 1)
                                selectedItem -= 1;
                            else
                                selectedItem = _ItemSlotAuction.Count - 1 - selectedItem;
                            SelectedAuctionItem();
                        }

                    }

                }

            }
        }

    }
    public void RemoveSlot()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i].RemoveAuction();
            _slots[i].gameObject.SetActive(false);
        }
    }

    public void AddItem(int _itemID, int _count = 1)
    {
        for (int i = 0; i < _AuctionList.Count; i++)
        {
            // 데이터 베이스에서 아이템 검색
            if (_itemID == _AuctionList[i].ID)
            {
                //  데이터베이스에서 아이템 발견
                for (int j = 0; j < _AuctionList.Count; j++)
                {
                    // 중복되는 아이템이 있는지 검색
                    if (_AuctionList[j].ID == _itemID)
                    {
                        //  중복되는 아이템이 존재시 오류 메시지 호출
                        if (_AuctionList[j].Type == "Eqip")
                        {
                            //  오류 호출
                            Debug.LogError("현재 존재하는 아이템 입니다.");
                        }
                        else
                        {
                            //  중복되지 않을 경우 추가
                            //auctionItemList.Add(dataBase.auctionItemsList[i]);
                        }
                        return;
                    }
                }
              //  auctionItemList.Add(dataBase.auctionItemsList[i]);
                return;
            }
        }

    }

    public void ShowAuctionItem()
    {
        _ItemSlotAuction.Clear();
        RemoveSlot();
        auctionTab = 0;

        //  추후에 다른게 추가될 수 있으니 스위치로 작성
        switch (auctionTab)
        {
            case 0:
                for (int i = 0; i < _AuctionList.Count; i++)
                {
                    _ItemSlotAuction.Add(_AuctionList[i]);
                }

                break;
        }

        for (int i = 0; i < _AuctionList.Count; i++)
        {
            _slots[i].gameObject.SetActive(true);
            _slots[i]._AddAuctionItem(_ItemSlotAuction[i]);
        }
        SelectedAuctionItem();
    }

    public void SelectedAuctionItem()
    {
        StopAllCoroutines();
        if (_ItemSlotAuction.Count < 0)
        {
            Color color = _slots[0].selectCheck_Item.GetComponent<Image>().color;
            color.a = 0f;
            for (int i = 0; i < _ItemSlotAuction.Count; i++)
                _slots[i].selectCheck_Item.GetComponent<Image>().color = color;
            StartCoroutine(SelectedAuctionItemEffect());
        }
    }
    IEnumerator SelectedAuctionItemEffect()
    {
        while (itemActivated)
        {
            Color color = _slots[0].GetComponent<Image>().color;
            while (color.a < 0.5f)
            {
                color.a += 0.03f;
                _slots[selectedItem].selectCheck_Item.GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a -= 0.03f;
                _slots[selectedItem].selectCheck_Item.GetComponent<Image>().color = color;
                yield return waitTime;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }


    // 경매장UI 및 도움말 
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "NPC")
        {
            //NPC_HelpKEY.SetActive(true);
            overlap = true;
        }
        else if (col.gameObject.tag == "testTag")
        {
            overlap = true;

        }

    }
    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("Defuze");
        //NPC_HelpKEY.SetActive(false);
        overlap = false;
    }
}
