using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastScript : MonoBehaviour
{
    [SerializeField]
    public GameObject stepManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;//for updating item text at top of screen

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask)){
            
            if(hit.transform.gameObject.GetComponent<ItemMoveScript>() != null){
                stepManager.GetComponent<UIManager>().typeText.text = hit.transform.gameObject.GetComponent<ItemMoveScript>().type.ToString();
            } else {
                stepManager.GetComponent<UIManager>().typeText.text = "";
                Debug.Log(hit.transform.gameObject.tag.ToString());
                if(hit.transform.gameObject.tag == "Cabinet" && Input.GetKeyDown(KeyCode.Space)){
                    Debug.Log("DOOR");
                    hit.transform.gameObject.tag = "CabinetOpen";
                    Transform door = hit.transform.GetChild(6);
                    Vector3 newRotation = new Vector3(0,90,0);
                    door.eulerAngles = newRotation;
                    Vector3 newPosition = new Vector3(hit.transform.position.x + 1, hit.transform.position.y, hit.transform.position.z);
                    door.position = newPosition;   
                } else if (hit.transform.gameObject.tag == "CabinetOpen" && Input.GetKeyDown(KeyCode.Space)){
                    hit.transform.gameObject.tag = "Cabinet";
                    Transform door = hit.transform.GetChild(6);
                    Vector3 newRotation = new Vector3(0,0,0);
                    door.eulerAngles = newRotation;
                    Vector3 newPosition = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z);
                    door.position = newPosition; 
                }
                if(hit.transform.gameObject.tag == "Oven" && Input.GetKeyDown(KeyCode.Space)){
                    Debug.Log("DOOR");
                    hit.transform.gameObject.tag = "OvenOpen";
                    stepManager.GetComponent<UIManager>().ovenOpen = true;
                    Transform door = hit.transform.GetChild(5);
                    Vector3 newRotation = new Vector3(0,-90,90);
                    door.eulerAngles = newRotation;
                    Vector3 newPosition = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z + 1);
                    door.position = newPosition;   
                } else if (hit.transform.gameObject.tag == "OvenOpen" && Input.GetKeyDown(KeyCode.Space)){
                    hit.transform.gameObject.tag = "Oven";
                    stepManager.GetComponent<UIManager>().ovenOpen = false;
                    Transform door = hit.transform.GetChild(5);
                    Vector3 newRotation = new Vector3(0,-90,0);
                    door.eulerAngles = newRotation;
                    Vector3 newPosition = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z);
                    door.position = newPosition; 
                }
            }
        } else {
            stepManager.GetComponent<UIManager>().typeText.text = "";
        }


    }
}
