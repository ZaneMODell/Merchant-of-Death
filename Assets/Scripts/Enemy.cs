using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Weapon.WeaponType weapon;
    public int holdingWeaponTier = 0;
    public GameObject weaponPrefab;
    public bool hasWeapon = false;
    public bool soldWeapon = false;
    public EnemySpawner enemySpawner;
    public Material green;
    public MeshRenderer meshRenderer;
    public SpriteRenderer objectSprite;
    public SpriteRenderer secondaryObjectSprite;
    public Sprite axeSprite;
    public Sprite swordSprite;
    public Sprite spearSprite;
    public Sprite maceSprite;
    public Sprite wantAxeSprite;
    public Sprite wantSwordSprite;
    public Sprite wantSpearSprite;
    public Sprite wantMaceSprite;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (weapon)
        {
            case Weapon.WeaponType.Sword:
                if(hasWeapon)
                {
                    objectSprite.sprite = swordSprite;
                    objectSprite.enabled = true;
                    secondaryObjectSprite.enabled = false;
                }
                else
                {
                    secondaryObjectSprite.sprite = wantSwordSprite;
                    objectSprite.enabled = false;
                    secondaryObjectSprite.enabled = true;
                }
                break;
            case Weapon.WeaponType.Spear:
                if (hasWeapon)
                {
                    objectSprite.sprite = spearSprite;
                    objectSprite.enabled = true;
                    secondaryObjectSprite.enabled = false;
                }
                else
                {
                    secondaryObjectSprite.sprite = wantSpearSprite;
                    objectSprite.enabled = false;
                    secondaryObjectSprite.enabled = true;
                }
                break;
            case Weapon.WeaponType.Axe:
                if (hasWeapon)
                {
                    objectSprite.sprite = axeSprite;
                    objectSprite.enabled = true;
                    secondaryObjectSprite.enabled = false;
                }
                else
                {
                    secondaryObjectSprite.sprite = wantAxeSprite;
                    objectSprite.enabled = false;
                    secondaryObjectSprite.enabled = true;
                }
                break;
            case Weapon.WeaponType.Mace:
                if (hasWeapon)
                {
                    objectSprite.sprite = maceSprite;
                    objectSprite.enabled = true;
                    secondaryObjectSprite.enabled = false;
                }
                else
                {
                    secondaryObjectSprite.sprite = wantMaceSprite;
                    objectSprite.enabled = false;
                    secondaryObjectSprite.enabled = true;
                }
                break;
        }
    }

    public void Die()
    {
        //Put the position back into available spawn points if weaponless
        if(!hasWeapon)
        {
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

        if(hasWeapon)
        {
            GameObject weaponGO = Instantiate<GameObject>(weaponPrefab, gameObject.transform.position, Quaternion.identity);
            weaponGO.GetComponent<Weapon>().weaponType = weapon;
            weaponGO.GetComponent<Weapon>().weaponTier = holdingWeaponTier;
        }

        //Spawn a replacement
        if(enemySpawner)
        {
            enemySpawner.SpawnSingle(gameObject);
        }
        enemySpawner.uiManager.enemiesDead += 1;
        Destroy(this.gameObject);
    }
}
