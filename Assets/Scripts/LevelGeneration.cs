using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public GameObject platform;
    public float platformTimer = 0;
    public float platformGenerationTimeInSeconds = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        platformTimer += Time.deltaTime;
        if(platformTimer >= platformGenerationTimeInSeconds)
        {
            GeneratePlatform();
            platformTimer = 0;
        }

    }

    public void GeneratePlatform()
    {
        float randomY = Random.Range(platform.transform.position.y-2, platform.transform.position.y + 2);
        GameObject newPlatform = Instantiate(platform, new Vector3(platform.transform.position.x +15, randomY, platform.transform.position.z), Quaternion.identity);
        newPlatform.transform.SetParent(transform);
        platform = newPlatform;

    }

}
