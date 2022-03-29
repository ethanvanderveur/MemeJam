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
            }
        } else {
            stepManager.GetComponent<UIManager>().typeText.text = "";
        }


    }
}
