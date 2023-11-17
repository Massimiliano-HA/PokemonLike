using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

        if (currentItems == 9)
        {
            SceneManager.LoadSceneAsync(2);
            SavingSystem.i.Delete("saveSlot1");
        }
    }


}
