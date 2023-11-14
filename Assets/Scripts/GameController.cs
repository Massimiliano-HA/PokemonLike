using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Dialogue, Battle }

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Camera worldCamera;
    //[SerializeField] BattleSystem battleSystem;

    //GameState gameState;

    /*private void Awake() {
        ConditionsDB.Init();
    }*/

    /*private void Start() {
        //player.OnEncountered += StartBattle;
        //battleSystem.OnBattleOver += EndBattle;

        DialogueManager.Instance.OnShowDialogue += () => {
            state = GameState.Dialogue;
        };

        DialogueManager.Instance.OnCloseDialogue += () => {
            if (state == GameState.Dialogue)
                state = GameState.FreeRoam;
        };
    }*/

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

    /*private void Update() {
        if(gameState == GameState.FreeRoam) {
            playerController.HandleUpdate();
        }
        else if (state == GameState.Battle) {
            battleSystem.HandleUpdate()
        }
        else if (state == GameState.Dialogue) {
            DialogueManager.Instance.HandleUpdate();
        } 
    }*/
}
