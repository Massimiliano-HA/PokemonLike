using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField]
    private Transform destination;

    public Vector2 teleportOffset;

    public Transform GetDestination()
    {
        return destination;
    }
}
