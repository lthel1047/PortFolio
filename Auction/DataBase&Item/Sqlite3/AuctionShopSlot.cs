using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AuctionShopSlot : MonoBehaviour
{
    public Image icon;                      // 아이템 사진
    public Text itemName_Text;              // 아이템 이름
    public Text itemValue_Text;             // 아이템 가격
    public GameObject selectCheck_Item;     // 아이템 선택식 반짝임
    public GameObject GradeRanking;         // 아이템 등급 구분

    public int testCount = 0;
    private int R_Value;

    public void _AddAuctionItem(AuctionItemManager _item)
    {
        itemName_Text.text = _item.Name;
        //itemValue_Text.text = _item.itemValue;
        //icon.sprite = _item.itemIcon;

        //// 장비를 경매장 아이템 리스트에 등록 시킨다.
        //if (AuctionItem.AuctionItemType.Eqip == _item.itemType)
        //{
        //    if (AuctionItem.AuctionItemGrade.Nomal == _item.itemGrade)  //  장비의 등급에 따른 추가적인 등록.
        //    {

        //        R_Value = int.Parse(_item.itemValue);
        //        R_Value = Random.Range(10, 50);
        //        Debug.Log("현재 가격 = " + R_Value);
        //        itemValue_Text.text = R_Value.ToString() + " $";    // 등록되는 장비에 가격 같이 등록.
        //        if (R_Value > 25)
        //            itemValue_Text.color = new Color(255, 0, 0);
        //        else
        //            itemValue_Text.color = new Color(0, 0, 255);

        //        Debug.Log("--- 무등급 장비가 등록 되었습니다. ---");
        //    }
        //    else if (AuctionItem.AuctionItemGrade.Comon == _item.itemGrade)
        //    {
        //        itemName_Text.color = new Color(255, 255, 255);
        //        R_Value = int.Parse(_item.itemValue);
        //        R_Value = Random.Range(10, 50);
        //        itemValue_Text.text = R_Value.ToString() + "00 $";    // 등록되는 장비에 가격 같이 등록.
        //        if (R_Value > 25)
        //            itemValue_Text.color = new Color(255, 0, 0);
        //        else
        //            itemValue_Text.color = new Color(0, 0, 255);


        //        Debug.Log("--- 일반 장비가 등록 되었습니다. ---");
        //    }
        //    else if (AuctionItem.AuctionItemGrade.Rare == _item.itemGrade)
        //    {
        //        itemName_Text.color = new Color(0, 0, 255);
        //        R_Value = int.Parse(_item.itemValue);
        //        R_Value = Random.Range(50, 100);
        //        itemValue_Text.text = R_Value.ToString() + ",000 $";    // 등록되는 장비에 가격 같이 등록.
        //        if (R_Value > 75)
        //            itemValue_Text.color = new Color(255, 0, 0);
        //        else
        //            itemValue_Text.color = new Color(0, 0, 255);

        //        Debug.Log("--- 희귀 장비가 등록 되었습니다. ---");
        //    }
        //    else if (AuctionItem.AuctionItemGrade.Epic == _item.itemGrade)
        //    {
        //        itemName_Text.color = new Color(255, 0, 0);
        //        R_Value = int.Parse(_item.itemValue);
        //        R_Value = Random.Range(1, 10);
        //        itemValue_Text.text = R_Value.ToString() + "0,000 $";    // 등록되는 장비에 가격 같이 등록.
        //        if (R_Value > 5)
        //            itemValue_Text.color = new Color(255, 0, 0);
        //        else
        //            itemValue_Text.color = new Color(0, 0, 255);

        //        Debug.Log("--- 매우 희귀한 장비가 등록 되었습니다. ---");
        //    }
        //    else
        //    {
        //        itemName_Text.color = new Color(255, 178, 0);
        //        R_Value = int.Parse(_item.itemValue);
        //        R_Value = Random.Range(50, 90);
        //        itemValue_Text.text = R_Value.ToString() + "0,000 $";    // 등록되는 장비에 가격 같이 등록.
        //        if (R_Value > 75)
        //            itemValue_Text.color = new Color(255, 0, 0);
        //        else
        //            itemValue_Text.color = new Color(0, 0, 255);

        //        Debug.Log("--- 전설 장비가 등록 되었습니다. ---");
        //    }

        //}



    }

    public void RemoveAuction()
    {
        itemName_Text.text = "";
        itemValue_Text.text = "";
        itemValue_Text.text = "";
        icon.sprite = null;
    }
}
