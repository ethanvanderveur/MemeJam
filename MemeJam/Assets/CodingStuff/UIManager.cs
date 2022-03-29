using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text textbox;

    [SerializeField]
    public Text typeText;
    [SerializeField]
    public Text extraText;
    [SerializeField]
    public Text timeText;
    private float startTime;
    private float maxTime;
    public float offset = 0;

    [SerializeField]
    public GameObject bowl;

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
    //step 2
    public int mixNum = 0;
    private int mixMaxNum = 10;
    //step 3
    public int kneadNum = 0;
    private int kneadMaxNum = 50;
    public bool onBoard = false;
    //step 5
    public bool risen = false;
    //step 6
    public int punchNum = 0;
    private int punchMaxNum = 10;
    //step 7
    public bool inOven = false;
    public bool ovenOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        maxTime = 120;
    }

    // Update is called once per frame
    void Update()
    {
        float t = maxTime - offset - Time.time - startTime;
        if(t <= 0){
            SceneManager.LoadScene("LoseScene");
        }
        string minutes = ((int) t / 60).ToString();
        string seconds = ((int) t % 60).ToString("00");
        timeText.text = minutes + ":" + seconds;

        
        switch (step){//changes instructions and changes state
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
                textbox.text = "Mix (keep pressing space on the bowl with the whisk)!";
                if(mixNum >= mixMaxNum)
                    step = 3;
                break;
            case 3:
                textbox.text = "Place dough on cutting board and knead (mash space on board to knead)";
                if(onBoard)
                    extraText.text = "Dough on board!";
                if(kneadNum >= kneadMaxNum){
                    step = 4;
                    bowlOil = false;
                }
                break;
            case 4:
                textbox.text = "Put the dough in an oiled bowl";
                if(!onBoard)
                    extraText.text = "Dough in bowl!";
                if(bowlOil && !onBoard)
                    step = 5;
                break;
            case 5:
                textbox.text = "Use timer to rise (costs 20 seconds)";
                if(risen)
                    step = 6;
                break;
            case 6:
                textbox.text = "Place dough on cutting board and punch to shape it (mash space on board to punch)";
                if(onBoard)
                    extraText.text = "Dough on board!";
                if(punchNum >= punchMaxNum)
                    step = 7;
                break;
            case 7:
                textbox.text = "Throw it in the oven and hit the timer";
                extraText.text = "Dough in oven!";
                break;
        }
    }
}
