using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Dialogue, Menu }

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Camera worldCamera;
    //[SerializeField] BattleSystem battleSystem;

    public GameState state;

    MenuController menuController;

    private void Awake() {
        menuController = GetComponent<MenuController>();
        // ConditionsDB.Init();
    }

    private void Start() {
        //player.OnEncountered += StartBattle;
        //battleSystem.OnBattleOver += EndBattle;

        DialogueManager.Instance.OnShowDialogue += () => {
        state = GameState.Dialogue;
        };

        DialogueManager.Instance.OnCloseDialogue += () => {
        if (state == GameState.Dialogue)
        state = GameState.FreeRoam;
        };

        SavingSystem.i.Load("saveSlot1");

        menuController.onBack += () =>
        {
            state = GameState.FreeRoam;
        };

        menuController.onMenuSelected += OnMenuSelected;

        DialogueManager.Instance.OnCloseDialogue += () => {
            if (state == GameState.Dialogue)
                state = GameState.FreeRoam;
        };
    }

    /*void StartBattle() {
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

        var playerParty = playerController.GetComponent<PokemonParty>();
        var wildPokemon = FindObjectOfType<MapArea>().GetComponent<MapArea>().GetRandomWildPokemon();

        battleSystem.StartBattle(playerParty, wildPokemon);
    }*/

    /*void EndBattle(bool won) {
        state = GameState.FreeRoam;
        battleSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
    }*/

    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        }
        /*else if (state == GameState.Battle) {
            battleSystem.HandleUpdate()
        }*/
        else if (state == GameState.Dialogue)
        {
            DialogueManager.Instance.HandleUpdate();
        } 

        if (Input.GetKeyDown(KeyCode.Escape))
            {
                menuController.OpenMenu();
                state = GameState.Menu;
            }
        else if (state == GameState.Menu)
        {
            menuController.HandleUpdate();
        }
        
    }

    void OnMenuSelected(int selectedItem)
    {
        if (selectedItem == 0)
        {

        }
        else if (selectedItem == 1)
        {
            SavingSystem.i.Save("saveSlot1");
        }
        else if (selectedItem == 2)
        {
            SavingSystem.i.Load("saveSlot1");
        }
        else if (selectedItem == 3)
        {
            Quit();
        }

        state = GameState.FreeRoam;
    }

    public void Quit()
{
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}


