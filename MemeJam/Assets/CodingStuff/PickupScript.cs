using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    [SerializeField]
    public GameObject bowl;

    [SerializeField]
    public GameObject stepManager;
    
    public Transform destination;

    public bool full = false;

    public GameObject held;

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
        transform.rotation = Quaternion.identity;
        List<Transform> placablePositions = GetComponent<ItemMoveScript>().getOpenPositions();
        if(GetComponent<ItemMoveScript>().type != ItemMoveScript.itemType.bowl && stepManager.GetComponent<UIManager>().step == 0){
            if(GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.water || GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.yeast || GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.sugar){
                placablePositions.Add(bowl.GetComponent<Transform>());
            }   
        }
        if(GetComponent<ItemMoveScript>().type != ItemMoveScript.itemType.bowl && stepManager.GetComponent<UIManager>().step == 1){
            if(GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.oil || GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.salt|| GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.flour){
                placablePositions.Add(bowl.GetComponent<Transform>());
            }   
        }

        GetComponent<Transform>().position = getClosestOpenPosition(placablePositions).position;
        GetComponent<Transform>().parent = getClosestOpenPosition(placablePositions).transform;
        
        //These next 3 checks check if an ingredient in step 0 is added to the bowl
        if(GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.water && getClosestOpenPosition(placablePositions).transform == bowl.transform){
            stepManager.GetComponent<UIManager>().bowlWater = true;
            if(gameObject != null)
                Destroy(gameObject);
        }
        if(GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.yeast && getClosestOpenPosition(placablePositions).transform == bowl.transform){
            stepManager.GetComponent<UIManager>().bowlYeast = true;
            if(gameObject != null)
                Destroy(gameObject);
        }
        if(GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.sugar && getClosestOpenPosition(placablePositions).transform == bowl.transform){
            stepManager.GetComponent<UIManager>().bowlSugar = true;
            if(gameObject != null)
                Destroy(gameObject);
        }
        //These next 4 check for step 1's ingredients
        if(GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.flour && getClosestOpenPosition(placablePositions).transform == bowl.transform){
            stepManager.GetComponent<UIManager>().bowlFlour = true;
            if(gameObject != null)
                Destroy(gameObject);
        }
        if(GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.oil && getClosestOpenPosition(placablePositions).transform == bowl.transform){
            stepManager.GetComponent<UIManager>().bowlOil = true;
            if(gameObject != null)
                Destroy(gameObject);
        }
         if(GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.salt && getClosestOpenPosition(placablePositions).transform == bowl.transform){
            stepManager.GetComponent<UIManager>().bowlSalt = true;
            if(gameObject != null)
                Destroy(gameObject);
        }


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
