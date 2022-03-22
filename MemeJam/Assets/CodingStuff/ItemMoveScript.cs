using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMoveScript : MonoBehaviour
{
    [SerializeField]
    private List<Transform> positions;

    private List<Transform> openPositions;

    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        openPositions = new List<Transform>();

        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // Clear openPositions
            openPositions.Clear();

            // Add all currently open positions to openPositions
            for (int i = 0; i < positions.Count; i++)
            {
                // If position is clear
                if (positions[i].childCount == 0)
                {
                    openPositions.Add(positions[i]);
                }
            }

            Debug.Log(openPositions);

            if (openPositions.Count > 0)
            {
                int index = Random.Range(0, openPositions.Count);
                rb.transform.parent = openPositions[index];
                rb.transform.position = openPositions[index].position;
            }
        }
    }
}
