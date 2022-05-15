using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIMainMenu : MonoBehaviour
{
    public GameObject howToPlayScreen;
    public GameObject buttonContainer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene("Level1Jumping 1");
    }

    public void HowToPlay()
    {
        howToPlayScreen.SetActive(true);
        buttonContainer.SetActive(false);
    }

    public void BackButton()
    {
        buttonContainer.SetActive(true);
        howToPlayScreen.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
