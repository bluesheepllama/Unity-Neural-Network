using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public GameObject platform;
    public GameObject background;
    public float backgroundTimer = 0;
    public float platformTimer = 0;
    public float platformGenerationTimeInSeconds = 2f;
    public float backgroundGenerationTimeInSeconds = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        platformTimer += Time.deltaTime;
        backgroundTimer += Time.deltaTime;
        if(platformTimer >= platformGenerationTimeInSeconds)
        {
            GeneratePlatform();
            platformTimer = 0;
        }

        if(backgroundTimer >= backgroundGenerationTimeInSeconds)
        {
            GenerateBackGround();
            backgroundTimer = 0;
        }
    }

    public void GeneratePlatform()
    {
        float randomY = Random.Range(platform.transform.position.y-2, platform.transform.position.y + 2);
        GameObject newPlatform = Instantiate(platform, new Vector3(platform.transform.position.x +20, randomY, platform.transform.position.z), Quaternion.identity);
        newPlatform.transform.SetParent(transform);
        platform = newPlatform;
        //Debug.Log("Generated Platform");

    }

    public void GenerateBackGround()
    {
        //TODO: get offset perfect
            // make make the chunck higher and lower in the editor
        GameObject newBkg = Instantiate(background, new Vector3(background.transform.position.x +120, background.transform.position.y, background.transform.position.z), Quaternion.identity);
        newBkg.transform.SetParent(transform);

        background = newBkg;
        //Debug.Log("Generated Background");

    }

    

}
