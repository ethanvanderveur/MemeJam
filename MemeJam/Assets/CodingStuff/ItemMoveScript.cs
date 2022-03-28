using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMoveScript : MonoBehaviour
{
    [SerializeField]
    private List<Transform> positions;
    
    public enum itemType{
        other,
        water,
        yeast,
        sugar,
        bowl,
        mixture,
        gSugar,
        oil,
        salt,
        flour,
        mixer,
        dough,
        oilBowl,
        wrap,
        doughTwo,
        unbakedBread,
        bakedBread
    }
    [SerializeField]
    public itemType type;

    private List<Transform> openPositions;

    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void randomMove()
    {
        openPositions = getOpenPositions();

        //Debug.Log(openPositions);
        // PLAY GHOST SOUND HERE <----------------------------------------------------
        if (openPositions.Count > 0)
        {
            int index = Random.Range(0, openPositions.Count);
            rb.transform.parent = openPositions[index];
            rb.transform.position = openPositions[index].position;
        }
    }

    public List<Transform> getOpenPositions()
    {
        List<Transform> openPositions = new List<Transform>();
        for (int i = 0; i < positions.Count; i++)
        {
            // If position is clear
            if (positions[i].childCount == 0)
            {
                openPositions.Add(positions[i]);
            }
        }
        return openPositions;
    }
}
