using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private GameObject currentTeleporter;

    public Fader fader;

    public PlayerController player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Portal"))
        {
            currentTeleporter = collision.gameObject;

            player.justTeleported = true;

            StartCoroutine(TPRoutine(currentTeleporter));
            
        }
    }

    private IEnumerator TPRoutine(GameObject currentTeleporter){

            yield return fader.FadeIn(1);
            PortalController portal = currentTeleporter.GetComponent<PortalController>();
            Vector2 destination = portal.GetDestination().position;
            transform.position = new Vector2(destination.x + portal.teleportOffset.x, destination.y + portal.teleportOffset.y);
            Debug.Log("test");
            player.canMove = true;
            yield return fader.FadeOut(1);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Portal"))
        {
            if (collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
            }
        }
    }
}
