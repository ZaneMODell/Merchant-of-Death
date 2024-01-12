using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> spawnPoints;
    public List<GameObject> availableSpawnPoints;
    public List<GameObject> usedSpawnPoints;

    public GameObject enemyPrefab;
    public GameObject weaponPrefab;
    public GameObject powerupPrefab;
    public UIManager uiManager;

    [Tooltip("This is the distance that the spawner allows two enemies that have/want the same weapon to spawn.")]
    [SerializeField] private float enemySpawnDistanceRange = 5f;

    public GameObject player;

    public HeroSpawner heroSpawner;

    // Start is called before the first frame update
    void Start()
    {
        heroSpawner = GameObject.Find("HeroSpawner").GetComponent<HeroSpawner>();
        player = GameObject.Find("Player");
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        availableSpawnPoints = spawnPoints;
        SpawnPair();
        SpawnPair();
        SpawnPair();
        SpawnPair();
        SpawnWeapon();
        SpawnPowerup();
        Invoke("SpawnPowerup", 15);
    }

    void SpawnPair()
    {
        GameObject enemyWithWeapon = Instantiate<GameObject>(enemyPrefab);
        GameObject enemyWithoutWeapon = Instantiate<GameObject>(enemyPrefab);

        //Pick a spawn point for each, and remove their locations from the available spawns
        int index = Random.Range(0, availableSpawnPoints.Count);
        enemyWithWeapon.transform.position = availableSpawnPoints[index].transform.position;

        usedSpawnPoints.Add(availableSpawnPoints[index]);
        availableSpawnPoints.RemoveAt(index);

        //Remove all spawn points nearby temporarily so that two enemies that have/want the same weapon arent right next to each other
        List<GameObject> tempAvailableSpawnPoints = new List<GameObject>();

        foreach(GameObject spawnPoint in availableSpawnPoints)
        {
            if(Vector3.Distance(spawnPoint.transform.position, enemyWithWeapon.transform.position) > enemySpawnDistanceRange)
            {
                tempAvailableSpawnPoints.Add(spawnPoint);
            }
        }

        index = Random.Range(0, tempAvailableSpawnPoints.Count);
        enemyWithoutWeapon.transform.position = tempAvailableSpawnPoints[index].transform.position;

        //Find where the enemy was spawned and remove that spawn
        foreach(GameObject spawnPoint in availableSpawnPoints)
        {
            if(spawnPoint.transform.position == enemyWithoutWeapon.transform.position)
            {
                usedSpawnPoints.Add(spawnPoint);
                availableSpawnPoints.Remove(spawnPoint);
                break;
            }
        }

        //Give them a Weapon enum type, and set their bool value for having a weapon
        Enemy weaponEnemy = enemyWithWeapon.GetComponent<Enemy>();
        Enemy weaponlessEnemy = enemyWithoutWeapon.GetComponent<Enemy>();
        weaponEnemy.hasWeapon = true;

        //Randomize weapon types.
        int randInt = Random.Range(1,5);
        switch (randInt)
        {
            case 1:
                weaponEnemy.weapon = Weapon.WeaponType.Sword;
                weaponlessEnemy.weapon = Weapon.WeaponType.Sword;
                break;
            case 2:
                weaponEnemy.weapon = Weapon.WeaponType.Axe;
                weaponlessEnemy.weapon = Weapon.WeaponType.Axe;
                break;
            case 3:
                weaponEnemy.weapon = Weapon.WeaponType.Spear;
                weaponlessEnemy.weapon = Weapon.WeaponType.Spear;
                break;
            case 4:
                weaponEnemy.weapon = Weapon.WeaponType.Mace;
                weaponlessEnemy.weapon = Weapon.WeaponType.Mace;
                break;
        }

        //Randomize the tier of the weaponEnemy's weapon
        randInt = Random.Range(0,4);
        if(randInt == 0)
        {
            weaponEnemy.holdingWeaponTier = 3;
        }
        else
        {
            weaponEnemy.holdingWeaponTier = 2;
        }

    }

    public void SpawnSingle(GameObject deadEnemyGO = null)
    {
        //This function is used to spawn an enemy after one dies or after an item is sold

        if(deadEnemyGO)
        {
            /*
            Enemy deadEnemy = deadEnemyGO.GetComponent<Enemy>();

            if (!deadEnemy.hasWeapon)
            {
                //Spawn an enemy that wants the same weapon, using the same method as SpawnPair()
                GameObject enemyWithoutWeapon = Instantiate<GameObject>(enemyPrefab);

                List<GameObject> tempAvailableSpawnPoints = new List<GameObject>();

                foreach (GameObject spawnPoint in availableSpawnPoints)
                {
                    tempAvailableSpawnPoints.Add(spawnPoint);
                }

                int index = Random.Range(0, tempAvailableSpawnPoints.Count);
                enemyWithoutWeapon.transform.position = tempAvailableSpawnPoints[index].transform.position;

                //Find where the enemy was spawned and remove that spawn
                foreach (GameObject spawnPoint in availableSpawnPoints)
                {
                    if (spawnPoint.transform.position == enemyWithoutWeapon.transform.position)
                    {
                        usedSpawnPoints.Add(spawnPoint);
                        availableSpawnPoints.Remove(spawnPoint);
                        break;
                    }
                }

                Enemy weaponlessEnemy = enemyWithoutWeapon.GetComponent<Enemy>();

                int randInt = Random.Range(1, 5);
                switch (randInt)
                {
                    case 1:
                        weaponlessEnemy.weapon = Weapon.WeaponType.Sword;
                        break;
                    case 2:
                        weaponlessEnemy.weapon = Weapon.WeaponType.Axe;
                        break;
                    case 3:
                        weaponlessEnemy.weapon = Weapon.WeaponType.Spear;
                        break;
                    case 4:
                        weaponlessEnemy.weapon = Weapon.WeaponType.Mace;
                        break;
                }

                weaponlessEnemy.hasWeapon = deadEnemy.hasWeapon;
            }

            //An enemy with a weapon has died
            else
            {
                //Do nothing lol
            }
            */
        }

        //If not given an enemy parameter, spawn a weaponless enemy with a random weapon request (far from the player)
        else
        {
            //Spawn an enemy that wants the same weapon, using the same method as SpawnPair()
            GameObject enemyWithoutWeapon = Instantiate<GameObject>(enemyPrefab);

            List<GameObject> tempAvailableSpawnPoints = new List<GameObject>();

            foreach (GameObject spawnPoint in availableSpawnPoints)
            {
                if(Vector3.Distance(spawnPoint.transform.position, player.transform.position) > enemySpawnDistanceRange)
                {
                    tempAvailableSpawnPoints.Add(spawnPoint);
                }
            }

            int index = Random.Range(0, tempAvailableSpawnPoints.Count);
            enemyWithoutWeapon.transform.position = tempAvailableSpawnPoints[index].transform.position;

            //Find where the enemy was spawned and remove that spawn
            foreach (GameObject spawnPoint in availableSpawnPoints)
            {
                if (spawnPoint.transform.position == enemyWithoutWeapon.transform.position)
                {
                    usedSpawnPoints.Add(spawnPoint);
                    availableSpawnPoints.Remove(spawnPoint);
                    break;
                }
            }

            //Set the weapon of this enemy randomly
            Enemy weaponlessEnemy = enemyWithoutWeapon.GetComponent<Enemy>();

            int randInt = Random.Range(1, 5);
            switch (randInt)
            {
                case 1:
                    weaponlessEnemy.weapon = Weapon.WeaponType.Sword;
                    break;
                case 2:
                    weaponlessEnemy.weapon = Weapon.WeaponType.Axe;
                    break;
                case 3:
                    weaponlessEnemy.weapon = Weapon.WeaponType.Spear;
                    break;
                case 4:
                    weaponlessEnemy.weapon = Weapon.WeaponType.Mace;
                    break;
            }

            //We're spawning an enemy that wants to buy a weapon, so this is always false.
            weaponlessEnemy.hasWeapon = false;
        }

        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemyList.Length < (heroSpawner.numHeroes * 2) + 4)
        {
            SpawnPair();
        }
    }

    void SpawnWeapon()
    {
        //Spawn a low tier weapon on the ground

        GameObject weaponGO = Instantiate(weaponPrefab);
        Weapon weaponComponent = weaponGO.GetComponent<Weapon>();

        //Randomly decide which weapon type
        int randInt = Random.Range(1, 5);
        switch (randInt)
        {
            case 1:
                weaponComponent.weaponType = Weapon.WeaponType.Sword;
                break;
            case 2:
                weaponComponent.weaponType = Weapon.WeaponType.Axe;
                break;
            case 3:
                weaponComponent.weaponType = Weapon.WeaponType.Spear;
                break;
            case 4:
                weaponComponent.weaponType = Weapon.WeaponType.Mace;
                break;
        }

        List<GameObject> tempAvailableSpawnPoints = new List<GameObject>();

        foreach (GameObject spawnPoint in availableSpawnPoints)
        {
            if (Vector3.Distance(spawnPoint.transform.position, player.transform.position) > enemySpawnDistanceRange)
            {
                tempAvailableSpawnPoints.Add(spawnPoint);
            }
        }

        //Pick a random spawn point
        int index = Random.Range(0, tempAvailableSpawnPoints.Count);
        weaponGO.transform.position = tempAvailableSpawnPoints[index].transform.position;

        //Find where the weapon was spawned and remove that spawn
        foreach (GameObject spawnPoint in availableSpawnPoints)
        {
            if (spawnPoint.transform.position == weaponGO.transform.position)
            {
                usedSpawnPoints.Add(spawnPoint);
                availableSpawnPoints.Remove(spawnPoint);
                break;
            }
        }

        randInt = Random.Range(0, 4);
        if(randInt == 0)
        {
            weaponComponent.weaponTier = 2;
        }
        else
        {
            weaponComponent.weaponTier = 1;
        }

        Invoke("SpawnWeapon", 30);
    }

    void SpawnPowerup()
    {
        //Spawn a powerup on a valid position

        GameObject powerupGO = Instantiate(powerupPrefab);

        List<GameObject> tempAvailableSpawnPoints = new List<GameObject>();

        foreach (GameObject spawnPoint in availableSpawnPoints)
        {
            if (Vector3.Distance(spawnPoint.transform.position, player.transform.position) > enemySpawnDistanceRange)
            {
                tempAvailableSpawnPoints.Add(spawnPoint);
            }
        }

        //Pick a random spawn point
        int index = Random.Range(0, tempAvailableSpawnPoints.Count);
        powerupGO.transform.position = tempAvailableSpawnPoints[index].transform.position;

        //Find where the weapon was spawned and remove that spawn
        foreach (GameObject spawnPoint in availableSpawnPoints)
        {
            if (spawnPoint.transform.position == powerupGO.transform.position)
            {
                usedSpawnPoints.Add(spawnPoint);
                availableSpawnPoints.Remove(spawnPoint);
                break;
            }
        }

        Invoke("SpawnPowerup", 30);
    }
}
