using System.Collections;
using UnityEngine;

public class FireSpawner : MonoBehaviour
{
    [Header("Fire Settings")]
    public GameObject firePrefab;       
    public float spawnInterval = 3f;    
    public float fireLifetime = 5f;     

    private Vector2 screenMin;
    private Vector2 screenMax;

    private void Start()
    {
        if (firePrefab == null)
        {
            Debug.LogError("Fire prefab not assigned!");
            return;
        }

        // Get camera bounds in world space
        Camera cam = Camera.main;
        if (cam != null)
        {
            screenMin = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
            screenMax = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));
        }
        else
        {
            screenMin = new Vector2(-8, -4);
            screenMax = new Vector2(8, 4);
        }

        StartCoroutine(SpawnFires());
    }

    private IEnumerator SpawnFires()
    {
        while (true)
        {
            SpawnFire();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnFire()
    {
        Vector2 spawnPos = new Vector2(
            Random.Range(screenMin.x, screenMax.x),
            Random.Range(screenMin.y, screenMax.y)
        );

        GameObject fire = Instantiate(firePrefab, spawnPos, Quaternion.identity);

        Fire fireScript = fire.GetComponent<Fire>();
        if (fireScript != null)
            fireScript.lifetime = fireLifetime;
    }
}
