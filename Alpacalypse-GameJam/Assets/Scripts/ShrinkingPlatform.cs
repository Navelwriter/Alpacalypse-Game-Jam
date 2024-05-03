using System.Collections;
using UnityEngine;

public class ShrinkingPlatform : MonoBehaviour
{
    public GameObject platformPrefab;
    public float shrinkDuration = 60f;
    

    private GameObject platformInstance; 
    public delegate void PlatformShrinkComplete();
    public static event PlatformShrinkComplete OnPlatformShrinkComplete;

    void Start()
    {
        SpawnAndShrinkPlatform();
    }

    void SpawnAndShrinkPlatform()
    {
        if (platformPrefab == null)
        {
            Debug.LogError("Platform prefab is not assigned!");
            return;
        }

        Vector3 spawnPosition = new Vector3(0, 3.5f, 0);
            
        platformInstance = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        StartCoroutine(ShrinkPlatformOverTime(platformInstance, shrinkDuration));
    }

    IEnumerator ShrinkPlatformOverTime(GameObject platform, float duration)
    {
        float time = 0;
        float initialWidth = platform.transform.localScale.x; 
        Vector3 originalScale = platform.transform.localScale;
        Vector3 startPosition = platform.transform.position;

        while (time < duration)
        {
            float scale = initialWidth * (1 - time / duration); 
            platform.transform.localScale = new Vector3(scale, originalScale.y, originalScale.z);
            platform.transform.position = new Vector3(startPosition.x - (originalScale.x - scale) / 2, startPosition.y, startPosition.z);
            time += Time.deltaTime;
            yield return null;
        }

        platform.transform.localScale = new Vector3(0, originalScale.y, originalScale.z);
        Destroy(platform); 
        OnPlatformShrinkComplete?.Invoke();
        SpawnAndShrinkPlatform();
    }
}