using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Item
{
    public int itemID;              // 아이템의 ID값 (중복 불가)
    public string itemName;         // 아이템의 이름 (중복 가능) 예) 사과,사과
    public string itemDescription;  // 아이템 설명
//    public string itemValue;        // 아이템 가격
    public int itemCount;           // 소지중인 갯수
    public Sprite itemIcon;         // 아이템의 아이콘
    public ItemType itemType;       // 아이템 종류
    public ItemGrade itemGrade;     // 아이템 등급

    public enum ItemType
    {
        Use,
        Equip,
        Quest,
        ETC
    }// 소비, 장비, 퀘스트아이템, 기타


    public enum ItemGrade
    {
        Nomal = 4,
        Comon,
        Rare,
        Epic,
        Legendary
    }// 등급 (무등급, 일반 ,희귀 ,잠재된 ,전설적인)


    public Item(int _itemID, string _itemName, string _itemDes, ItemType _itemType, ItemGrade _itemGrade, int _itemCount = 1)
    {
        itemID = _itemID;
        itemName = _itemName;
        itemDescription = _itemDes;
        itemType = _itemType;
        itemCount = _itemCount;
        itemGrade = _itemGrade;
        itemIcon = Resources.Load("ItemIcon/"+_itemID.ToString(),typeof(Sprite))as Sprite;
    }


}

[System.Serializable]
public class AuctionItem
{
    public int itemID;                  // 아이템의 ID값 (중복 불가)
    public string itemName;             // 아이템의 이름 (중복 가능) 예) 사과,사과
    public string itemDescription;      // 아이템 효과 설명
    public string itemValue;            // 아이템 가격
    public Sprite itemIcon;             // 아이템의 아이콘

    
    public AuctionItemType itemType;    // 아이템 종류
    public AuctionItemGrade itemGrade;  // 아이템 등급

    public enum AuctionItemGrade
    {
        Nomal,
        Comon,
        Rare,
        Epic,
        Legendary
    }// 등급 (무등급, 일반 ,희귀 ,잠재된 ,전설적인)

    public enum AuctionItemType
    {
        Eqip
    }

    public AuctionItem(int _itemID, string _itemName, string _itemDes, AuctionItemType _itemType, AuctionItemGrade _itemGrade,  string _itemValue)
    {
        itemID = _itemID;
        itemName = _itemName;
        itemDescription = _itemDes;
        itemValue = _itemValue;
        itemType = _itemType;
        itemGrade = _itemGrade;
        itemIcon = Resources.Load("ItemIcon/" + _itemID.ToString(), typeof(Sprite)) as Sprite;      
        
    }
}
