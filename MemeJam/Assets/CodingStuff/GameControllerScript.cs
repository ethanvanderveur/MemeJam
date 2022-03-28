using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public List<GameObject> moveableItems;

    private int GHOST_MOVE_RATE = 500;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Move a random item randomly item randomly
        if (Random.Range(0, GHOST_MOVE_RATE) == 1)
        {
            int index = Random.Range(0, moveableItems.Count);
            moveableItems[index].GetComponent<ItemMoveScript>().randomMove();
        }

    }
}
