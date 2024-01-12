using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public Weapon.WeaponType powerupType = Weapon.WeaponType.None;
    public GameObject powerupSprite;
    public Sprite boots;
    public Sprite wings;
    public Sprite cloak;
    public Sprite bell;
    private EnemySpawner enemySpawner;

    // Start is called before the first frame update
    void Start()
    {
        int randInt = Random.Range(0, 3);

        switch (randInt)
        {
            case 0:
                powerupType = Weapon.WeaponType.Boots;
                powerupSprite.GetComponent<SpriteRenderer>().sprite = boots;
                powerupSprite.transform.localScale = new Vector3(0.007f, 0.007f, 1);
                break;
            case 1:
                powerupType = Weapon.WeaponType.Bell;
                powerupSprite.GetComponent<SpriteRenderer>().sprite = bell;
                powerupSprite.transform.localScale = new Vector3(0.015f, 0.015f, 1);
                break;
            case 2:
                powerupType = Weapon.WeaponType.Cloak;
                powerupSprite.GetComponent<SpriteRenderer>().sprite = cloak;
                powerupSprite.transform.localScale = new Vector3(0.04f, 0.03f, 1);
                break;

        }

        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();

        Destroy(gameObject, 20);
    }

    private void OnDestroy()
    {
        //Whenever a powerup is spawned, you must take its position out of EnemySpawner.availableSpawnPoints! This puts that position back.
        foreach (GameObject spawnPoint in enemySpawner.usedSpawnPoints)
        {
            if (spawnPoint.transform.position == gameObject.transform.position)
            {
                enemySpawner.usedSpawnPoints.Remove(spawnPoint);
                enemySpawner.availableSpawnPoints.Add(spawnPoint);
                break;
            }
        }
    }
}
