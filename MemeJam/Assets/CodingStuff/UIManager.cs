using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text textbox;

    [SerializeField]
    GameObject bowl;

    public int step = 0;
    public int time = 0;
    //step 0
    public bool bowlWater = false;
    public bool bowlYeast = false;
    public bool bowlSugar = false;
    //step 1
    public bool bowlOil = false;
    public bool bowlSalt = false;
    public bool bowlFlour = false;
    public bool mixed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (step){
            case 0:
                textbox.text = "Mix warm water, yeast, and sugar in mixing bowl";
                if(bowlWater && bowlYeast && bowlSugar)
                    step = 1;
                break;
            case 1:
                textbox.text = "Add oil, salt, and flour";
                if(bowlFlour && bowlOil && bowlSalt)
                    step = 2;
                break;
            case 2: 
                textbox.text = "Mix!";
                break;
            case 3:
                textbox.text = "Knead the dough";
                break;
            case 4:
                textbox.text = "Put the dough in an oiled bowl and cover with plastic wrap";
                break;
            case 5:
                textbox.text = "Allow time to rise";
                break;
            case 6:
                textbox.text = "Punch dough to shape it";
                break;
            case 7:
                textbox.text = "Bake for 30 minutes";
                break;
        }
    }
}
