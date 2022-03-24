using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNearestPositionScript : MonoBehaviour
{

    public Transform GetClosestOpenPosition(List<Transform> openPositions)
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
