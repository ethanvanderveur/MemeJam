using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public Transform destination;

    public bool full = false;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        rb.isKinematic = true;
        destination = GameObject.Find("ItemBeingHeldPosition").transform;
        rb.transform.position = destination.position;
        rb.transform.parent = GameObject.Find("ItemBeingHeldPosition").transform;
        full = true;
    }

    private void OnMouseUp()
    {
        rb.isKinematic = false;
        full = false;
        GetComponent<Transform>().position = getClosestOpenPosition(GetComponent<ItemMoveScript>().getOpenPositions()).position;
        GetComponent<Transform>().parent = getClosestOpenPosition(GetComponent<ItemMoveScript>().getOpenPositions()).transform;


    }

    public Transform getClosestOpenPosition(List<Transform> openPositions)
    {
        Transform bestPosition = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = GetComponent<Transform>().position;
        foreach (Transform potentialPosition in openPositions)
        {
            Vector3 directionToTarget = potentialPosition.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestPosition = potentialPosition;
            }
        }
        return bestPosition;
    }
}
