using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] ballons;
    [Range(0.2f, 1f)] public float spawnTime = 1f;
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float spawnReductionRate = 0.05f; // Rate of spawn time reduction per spawn
    private float minSpawnTime = 0.2f; // Minimum spawn time limit

    void Start()
    {
        StartCoroutine(SpawnBallons());
    }

    public IEnumerator SpawnBallons()
    {
        while (true)
        {
            float spawnRange = Random.Range(minX, maxX);
            Vector3 position = new Vector3(spawnRange, transform.position.y);
            Instantiate(ballons[Random.Range(0, ballons.Length)], position, Quaternion.identity);

            yield return new WaitForSeconds(spawnTime);

            // Reduce spawn time but ensure it doesn't go below the minimum
            spawnTime = Mathf.Max(minSpawnTime, spawnTime - spawnReductionRate);
        }
    }
}
