using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpawner : MonoBehaviour
{
    public List<GameObject> heroSpawnPoints;
    public GameObject heroPrefab;
    public GameObject player;
    public int numHeroes = 1;

    private void Start()
    {
        SpawnHero();
    }

    public void SpawnHero()
    {
        float farthestPointDistance = 0;
        GameObject farthestPoint = null;

        foreach (GameObject spawnPoint in heroSpawnPoints)
        {
            if (Vector3.Distance(spawnPoint.transform.position, player.transform.position) > farthestPointDistance)
            {
                farthestPoint = spawnPoint;
                farthestPointDistance = Vector3.Distance(spawnPoint.transform.position, player.transform.position);
            }
        }

        if (farthestPoint == null)
        {
            Debug.Log("Found no farthest point on spawning hero! Panic!");
            return;
        }

        GameObject heroGO = Instantiate(heroPrefab, farthestPoint.transform.position, Quaternion.identity);
        heroGO.GetComponent<Hero>().destinationPoints = heroSpawnPoints;
        numHeroes += 1;
    }
}
