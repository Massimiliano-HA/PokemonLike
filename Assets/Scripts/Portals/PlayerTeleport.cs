using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private GameObject currentTeleporter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Portal"))
        {
            currentTeleporter = collision.gameObject;

            PlayerController playerController = GetComponent<PlayerController>();
                if (playerController != null)
                    {
                        playerController.justTeleported = true;
                    }

            PortalController portal = currentTeleporter.GetComponent<PortalController>();
            Vector2 destination = portal.GetDestination().position;
            transform.position = new Vector2(destination.x + portal.teleportOffset.x, destination.y + portal.teleportOffset.y);
            Debug.Log("test");
        }
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
