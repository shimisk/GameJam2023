using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    //Vertical distance between subsequent platforms
    public float VerticalPlatformDistance = 2.0f;

    //Min random X for platform
    public float PlatformRandomMinX = -1.0f;

    //Max random X for platform
    public float PlatformRandomMaxX = 1.0f;

    //How many platforms to create
    public int MaxPlatformCount = 10;

    //Main platform prefab
    public GameObject PlatformPrefab;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlatforms();
    }

    void SpawnPlatforms()
    {
        Vector3 position = transform.position;

        for (int i = 0; i < MaxPlatformCount; i++)
        {
            float randomHorizontalOffset = Random.Range(PlatformRandomMinX, PlatformRandomMaxX);
            Vector3 newPosition = new Vector3(position.x + randomHorizontalOffset, position.y + VerticalPlatformDistance * i);
            Instantiate<GameObject>(PlatformPrefab, newPosition, Quaternion.identity);
        }
    }
}
