using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public string[] var_name;
    public float[] var;

    public string[] switch_name;
    public bool[] switches;

    public List<Item> itemList = new List<Item>();
    public List<AuctionItem> auctionItemsList = new List<AuctionItem>();

    void Start()
    {
        itemList.Add(new Item(10001, "로우 체력 포션", "체력 5회복", Item.ItemType.Use, Item.ItemGrade.Nomal));
        itemList.Add(new Item(10002, "로우 마나", "마나 5회복", Item.ItemType.Use, Item.ItemGrade.Nomal));
        itemList.Add(new Item(10003, "하이 체력 포션", "체력 10회복", Item.ItemType.Use, Item.ItemGrade.Nomal));
        itemList.Add(new Item(10004, "하이 마나 포션", "마나 10회복", Item.ItemType.Use, Item.ItemGrade.Nomal));
        itemList.Add(new Item(11001, "아무 의미 없는 상자", "쓰래기가 나올수도 있다.", Item.ItemType.Use, Item.ItemGrade.Nomal));
        itemList.Add(new Item(20001, "단검", "공격력 1", Item.ItemType.Equip, Item.ItemGrade.Comon));
        itemList.Add(new Item(21001, "반지", "아무런 효과가 없다", Item.ItemType.Equip, Item.ItemGrade.Comon));
        itemList.Add(new Item(30001, "유물 조각 1", "부서진 유뮬", Item.ItemType.Quest, Item.ItemGrade.Rare));
        itemList.Add(new Item(30002, "유물 조각 2", "부서진 유뮬", Item.ItemType.Quest, Item.ItemGrade.Rare));
        itemList.Add(new Item(30003, "유물", "비싸게 팔수있다.", Item.ItemType.Quest, Item.ItemGrade.Epic));



        auctionItemsList.Add(new AuctionItem(10001, "검", "평범한 검", AuctionItem.AuctionItemType.Eqip, AuctionItem.AuctionItemGrade.Nomal,"1000"));
        auctionItemsList.Add(new AuctionItem(10002, "한손 검", "남다른 검", AuctionItem.AuctionItemType.Eqip, AuctionItem.AuctionItemGrade.Legendary, "3000"));
    }

}
