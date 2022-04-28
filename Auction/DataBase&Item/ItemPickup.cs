using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    public int itemID;
    public int count;
    //public string pickupSound;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //AudioManager.instance.Play(pickupSound);
            Debug.Log("Z눌림");
            Inventory.instance.GetAnItem(itemID, count);

            Destroy(this.gameObject);
        }
    }
}
