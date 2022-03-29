using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButtonClicked()
    {
        SceneManager.LoadScene("SampleScene");
        Debug.Log("Start!");
    }

    public void QuitButtonClicked()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

}
