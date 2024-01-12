using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WeaponType
    {
        None,
        Sword,
        Axe,
        Spear,
        Mace,
        Boots,
        Wings,
        Cloak,
        Bell
    }

    public int weaponTier = 0;
    public EnemySpawner enemySpawner;
    public WeaponType weaponType;
    public float weaponDespawnTimer = 10f;

    public Sprite axe1;
    public Sprite axe2;
    public Sprite axe3;
    public Sprite mace1;
    public Sprite mace2;
    public Sprite mace3;
    public Sprite spear1;
    public Sprite spear2;
    public Sprite spear3;
    public Sprite sword1;
    public Sprite sword2;
    public Sprite sword3;
    public GameObject spriteObject;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        Invoke(nameof(Die), weaponDespawnTimer);

        switch(weaponType)
        {
            case (Weapon.WeaponType.Sword):
                if (weaponTier == 1)
                {
                    spriteObject.GetComponent<SpriteRenderer>().sprite = sword1;
                }
                if (weaponTier == 2)
                {
                    spriteObject.GetComponent<SpriteRenderer>().sprite = sword2;
                }
                if (weaponTier == 3)
                {
                    spriteObject.GetComponent<SpriteRenderer>().sprite = sword3;
                }
                break;
            case (Weapon.WeaponType.Axe):
                if (weaponTier == 1)
                {
                    spriteObject.GetComponent<SpriteRenderer>().sprite = axe1;
                }
                if (weaponTier == 2)
                {
                    spriteObject.GetComponent<SpriteRenderer>().sprite = axe2;
                }
                if (weaponTier == 3)
                {
                    spriteObject.GetComponent<SpriteRenderer>().sprite = axe3;
                }
                break;
            case (Weapon.WeaponType.Spear):
                if (weaponTier == 1)
                {
                    spriteObject.GetComponent<SpriteRenderer>().sprite = spear1;
                }
                if (weaponTier == 2)
                {
                    spriteObject.GetComponent<SpriteRenderer>().sprite = spear2;
                }
                if (weaponTier == 3)
                {
                    spriteObject.GetComponent<SpriteRenderer>().sprite = spear3;
                }
                break;
            case (Weapon.WeaponType.Mace):
                if (weaponTier == 1)
                {
                    spriteObject.GetComponent<SpriteRenderer>().sprite = mace1;
                }
                if (weaponTier == 2)
                {
                    spriteObject.GetComponent<SpriteRenderer>().sprite = mace2;
                }
                if (weaponTier == 3)
                {
                    spriteObject.GetComponent<SpriteRenderer>().sprite = mace3;
                }
                break;
        }

    }

    private void Update()
    {
        if(enemySpawner.availableSpawnPoints.Count < 2)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        //Whenever a weapon is spawned, you must take its position out of EnemySpawner.availableSpawnPoints! This puts that position back.
        foreach(GameObject spawnPoint in enemySpawner.usedSpawnPoints)
        {
            if(spawnPoint.transform.position == gameObject.transform.position)
            {
                enemySpawner.usedSpawnPoints.Remove(spawnPoint);
                enemySpawner.availableSpawnPoints.Add(spawnPoint);
                break;
            }
        }
    }
}
