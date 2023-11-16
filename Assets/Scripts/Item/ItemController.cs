using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    public static ItemController instance;

    public Text itemText;
    public int currentItems = 0;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        itemText.text = "Items : " + currentItems.ToString();
    }

    public void IncreaseItems(int items)
    {
        currentItems += items;
        itemText.text = "Items : " + currentItems.ToString();
    }
}
