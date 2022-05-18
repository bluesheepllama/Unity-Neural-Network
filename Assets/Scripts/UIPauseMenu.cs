using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPauseMenu : MonoBehaviour
{

    //public GameObject pauseScreen;
    public GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Restart()
    {
        //Time.timeScale = 1;

        SceneManager.LoadScene("Level1Jumping 1");
    }

    public void MainMenu()
    {
        //Time.timeScale = 1;

        SceneManager.LoadScene("MainMenu");
    }

    public void Die()
    {
        gameOverScreen.SetActive(true);
    }


}
