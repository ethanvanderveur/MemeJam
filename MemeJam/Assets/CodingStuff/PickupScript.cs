using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    [SerializeField]
    public GameObject bowl;

    [SerializeField]
    public GameObject board;

    [SerializeField]
    public GameObject timer;

    [SerializeField]
    public GameObject oven;

    [SerializeField]
    public GameObject stepManager;
    
    public Transform destination;

    public bool full = false;

    public GameObject held;

    Rigidbody rb;

    private int mixCD = 0;
    private int mixMaxCD = 120;

    private int kneadCD = 0;
    private int kneadMaxCD = 45;

    private int punchCD = 0;
    private int punchMaxCD = 100;


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
            if((GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.oil && !stepManager.GetComponent<UIManager>().bowlOil) || GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.salt|| GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.flour){
                placablePositions.Add(bowl.GetComponent<Transform>());
            }   
        }
        if(GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.bowl && (stepManager.GetComponent<UIManager>().step == 3 || stepManager.GetComponent<UIManager>().step == 6)){
            placablePositions.Add(board.GetComponent<Transform>());
        }
        if(GetComponent<ItemMoveScript>().type != ItemMoveScript.itemType.bowl && stepManager.GetComponent<UIManager>().step == 4){
            if((GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.oil && !stepManager.GetComponent<UIManager>().bowlOil) || (stepManager.GetComponent<UIManager>().bowlOil && stepManager.GetComponent<UIManager>().onBoard && GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.board)){
                placablePositions.Add(bowl.GetComponent<Transform>());
            }   
        }
        if(GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.board && stepManager.GetComponent<UIManager>().step == 7){
            placablePositions = null;
            placablePositions.Add(oven.GetComponent<Transform>());
        }
    

        GetComponent<Transform>().position = getClosestOpenPosition(placablePositions).position;
        GetComponent<Transform>().parent = getClosestOpenPosition(placablePositions).transform;
        
        //These next 3 checks check if an ingredient in step 0 is added to the bowl
        if(GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.water && getClosestOpenPosition(placablePositions).transform == bowl.transform){
            stepManager.GetComponent<UIManager>().bowlWater = true;
            
            //add water to bowl
            if(gameObject != null)
                Destroy(gameObject);
        }
        if(GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.yeast && getClosestOpenPosition(placablePositions).transform == bowl.transform){
            stepManager.GetComponent<UIManager>().bowlYeast = true;
            //add yeast to bowl
            if(gameObject != null)
                Destroy(gameObject);
        }
        if(GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.sugar && getClosestOpenPosition(placablePositions).transform == bowl.transform){
            stepManager.GetComponent<UIManager>().bowlSugar = true;
            //add sugar to bowl
            if(gameObject != null)
                Destroy(gameObject);
        }
        //These next 4 check for step 1's ingredients
        if(GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.flour && getClosestOpenPosition(placablePositions).transform == bowl.transform){
            stepManager.GetComponent<UIManager>().bowlFlour = true;
            //add flour to bowl
            if(gameObject != null)
                Destroy(gameObject);
        }
        if(GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.oil && getClosestOpenPosition(placablePositions).transform == bowl.transform && !stepManager.GetComponent<UIManager>().bowlOil && (stepManager.GetComponent<UIManager>().step == 1 || stepManager.GetComponent<UIManager>().step == 4)){
            stepManager.GetComponent<UIManager>().bowlOil = true;
            //add oil to bowl, happens twice
            if(gameObject != null)
                Destroy(gameObject);
        }
         if(GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.salt && getClosestOpenPosition(placablePositions).transform == bowl.transform){
            stepManager.GetComponent<UIManager>().bowlSalt = true;
            //add salt to bowl
            if(gameObject != null)
                Destroy(gameObject);
        }
        //checks for step 3, putting dough on board before kneading also step 6
        if(GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.bowl && getClosestOpenPosition(placablePositions).transform == board.transform){
            //place dough on top of board, empty bowl

            
            stepManager.GetComponent<UIManager>().onBoard = true;
            GetComponent<Transform>().position = getClosestOpenPosition(GetComponent<ItemMoveScript>().getOpenPositions()).position;
            GetComponent<Transform>().parent = getClosestOpenPosition(GetComponent<ItemMoveScript>().getOpenPositions()).transform;
        }
        //checks for step 4, oiling bowl
        if(GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.board && getClosestOpenPosition(placablePositions).transform == bowl.transform){
            //place dough in bowl, empty board
            
            stepManager.GetComponent<UIManager>().onBoard = false;
            GetComponent<Transform>().position = getClosestOpenPosition(GetComponent<ItemMoveScript>().getOpenPositions()).position;
            GetComponent<Transform>().parent = getClosestOpenPosition(GetComponent<ItemMoveScript>().getOpenPositions()).transform;
        }
        //step 7 places in oven
        if(GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.board && getClosestOpenPosition(placablePositions).transform == oven.transform && stepManager.GetComponent<UIManager>().ovenOpen){
            //place dough in oven, empty board
            stepManager.GetComponent<UIManager>().onBoard = false;
            stepManager.GetComponent<UIManager>().inOven = true;
            GetComponent<Transform>().position = getClosestOpenPosition(GetComponent<ItemMoveScript>().getOpenPositions()).position;
            GetComponent<Transform>().parent = getClosestOpenPosition(GetComponent<ItemMoveScript>().getOpenPositions()).transform;
        }
    }

    private void Update(){
        if(mixCD > 0){//step 2, mix
            mixCD--;
        }
        if (full && GetComponent<ItemMoveScript>().type == ItemMoveScript.itemType.mixer && Input.GetKeyDown(KeyCode.Space) && mixCD <= 0 && stepManager.GetComponent<UIManager>().step == 2)
        {
            List<Transform> placablePositions = GetComponent<ItemMoveScript>().getOpenPositions();
            placablePositions.Add(bowl.GetComponent<Transform>());
            if(getClosestOpenPosition(placablePositions).transform == bowl.transform){
                mixCD = mixMaxCD;
                stepManager.GetComponent<UIManager>().mixNum++;
            }
        }
        if(kneadCD > 0){//step 3, knead
            kneadCD--;
        }
        if (!full && Input.GetKeyDown(KeyCode.Space) && kneadCD <= 0 && stepManager.GetComponent<UIManager>().step == 3){
            List<Transform> placablePositions = GetComponent<ItemMoveScript>().getOpenPositions();
            placablePositions.Add(board.GetComponent<Transform>());
            if(getClosestOpenPosition(placablePositions).transform == board.transform){
                kneadCD = kneadMaxCD;
                stepManager.GetComponent<UIManager>().kneadNum++;
            }
        }
         if(punchCD > 0){//step 6, punch
            punchCD--;
        }
        if (!full && Input.GetKeyDown(KeyCode.Space) && punchCD <= 0 && stepManager.GetComponent<UIManager>().step == 6){
            List<Transform> placablePositions = GetComponent<ItemMoveScript>().getOpenPositions();
            placablePositions.Add(board.GetComponent<Transform>());
            if(getClosestOpenPosition(placablePositions).transform == board.transform){
                punchCD = punchMaxCD;
                stepManager.GetComponent<UIManager>().punchNum++;
            }
        }
        if(Input.GetKeyDown(KeyCode.Space)){//step 5, timer
            List<Transform> placablePositions = GetComponent<ItemMoveScript>().getOpenPositions();
            placablePositions.Add(timer.GetComponent<Transform>());
            if(getClosestOpenPosition(placablePositions).transform == timer.transform){
                Debug.Log("timer");
                if(stepManager.GetComponent<UIManager>().step == 5){
                    stepManager.GetComponent<UIManager>().risen = true;
                    stepManager.GetComponent<UIManager>().offset += 20;
                }
                if(stepManager.GetComponent<UIManager>().step == 7){
                    //go to win screen?
                    Debug.Log("victory");
                }
            }
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
