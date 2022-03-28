using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text textbox;

    int step = 0;
    int time = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time++;
        if(time >= 200){
            time = 0;
            step++;
        }
        switch (step){
            case 0:
                textbox.text = "Mix warm water, yeast, and granulated sugar in mixing bowl";
                break;
            case 1:
                textbox.text = "Add sugar, oil, salt, and flour and mix";
                break;
            case 2:
                textbox.text = "Knead the dough";
                break;
            case 3:
                textbox.text = "Put the dough in an oiled bowl and cover with plastic wrap";
                break;
            case 4:
                textbox.text = "Allow time to rise";
                break;
            case 5:
                textbox.text = "Punch dough to shape it";
                break;
            case 6:
                textbox.text = "Bake for 30 minutes";
                break;
        }
    }
}
