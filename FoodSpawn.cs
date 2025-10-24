using System.Collections;
using UnityEngine;

// I followed this tutorial for this script:
// 
public class FoodSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] foodPrefab;
    [SerializeField] float secondSpawn = 30f; 
    [SerializeField] float minDistance = 20f; 

    private Camera mainCamera;
    private Vector2 lastSpawnPos;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(nomSpawn());
    }

    IEnumerator nomSpawn()
    {
        while (true)
        {
            Vector2 spawnPos;

            do
            {

                Vector3 minScreen = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
                Vector3 maxScreen = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

                float x = Random.Range(minScreen.x, maxScreen.x);
                float y = Random.Range(minScreen.y, maxScreen.y);

                spawnPos = new Vector2(x, y);
            }
            while (Vector2.Distance(spawnPos, lastSpawnPos) < minDistance);


            lastSpawnPos = spawnPos;

            GameObject prefab = foodPrefab[Random.Range(0, foodPrefab.Length)];
            GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity);
            obj.name = prefab.name; 

            yield return new WaitForSeconds(secondSpawn);

            Destroy(obj, 15f);

        }
    }
}
