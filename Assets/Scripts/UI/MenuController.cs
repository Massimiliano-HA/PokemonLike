using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject menu;

    [SerializeField] GameObject itemsMenu;

    public event Action<int> onMenuSelected;
    public event Action onBack;
    
    List<Text> menuItems;
    int selectedItem = 0;

    private void Awake()
    {
       menuItems = itemsMenu.GetComponentsInChildren<Text>().ToList();
    }

    public void OpenMenu()
    {
        menu.SetActive(true);
        UpdateItemSelection();
    }

     public void CloseMenu()
    {
        menu.SetActive(false);
    }

    public void HandleUpdate()
    {
        int prevSelection = selectedItem;

        if (Input.GetKeyDown(KeyCode.DownArrow))
            ++selectedItem;
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            --selectedItem;

        selectedItem = Mathf.Clamp(selectedItem, 0, menuItems.Count - 1);

        if (prevSelection != selectedItem)
            UpdateItemSelection();

         if (Input.GetKeyDown(KeyCode.Escape))
         {
            onMenuSelected?.Invoke(selectedItem);
            CloseMenu();
         } 
         else if (Input.GetKeyDown(KeyCode.W))
         {
            onBack?.Invoke();
            CloseMenu();
         }
         else if (Input.GetKeyDown(KeyCode.S))
         {
            onBack?.Invoke();
            CloseMenu();
         }
         else if (Input.GetKeyDown(KeyCode.A))
         {
            onBack?.Invoke();
            CloseMenu();
         }
         else if (Input.GetKeyDown(KeyCode.D))
         {
            onBack?.Invoke();
            CloseMenu();
         }

    }

    void UpdateItemSelection()
    {
        for (int i = 0; i < menuItems.Count; i++)
        {
            if (i == selectedItem)
            {
                menuItems[i].color = GlobalSettings.i.HighlightedColor;
            }
            else
            {
                menuItems[i].color = Color.black;
            }
        }
    }
}