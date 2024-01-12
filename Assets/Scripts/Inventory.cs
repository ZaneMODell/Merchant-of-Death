using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{

    public Weapon weapon;
    public PlayerMovement playerMovement;

    public int maxSize = 4;
    public List<Weapon.WeaponType> weaponList = new List<Weapon.WeaponType>(4);
    public List<int> weaponTierList = new List<int>(4);

    public EnemySpawner enemySpawner;
    public UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    public void AddWeapon(Weapon weapon)
    {
        Weapon.WeaponType tempWeaponType;
        tempWeaponType = weapon.weaponType;

        if (weaponList.Count < maxSize)
        {
            weaponList.Add(tempWeaponType);
            weaponTierList.Add(weapon.weaponTier);
            Destroy(weapon.gameObject);
        }

        if(weaponList.Contains(Weapon.WeaponType.Axe) && weaponList.Contains(Weapon.WeaponType.Spear) && weaponList.Contains(Weapon.WeaponType.Mace) && weaponList.Contains(Weapon.WeaponType.Sword))
        {
            foreach(int tier in weaponTierList)
            {
                if(tier != 3)
                {
                    return;
                }
            }
            uiManager.longBeforeTimeHadAName = true;
        }
    }

    public void AddPowerup(Powerup powerup)
    {
        Weapon.WeaponType tempPowerupType;
        tempPowerupType = powerup.powerupType;

        if (weaponList.Count < maxSize)
        {
            weaponList.Add(tempPowerupType);
            weaponTierList.Add(0);
            Destroy(powerup.gameObject);
        }
    }

    public Weapon.WeaponType GiveWeapon(GameObject enemy)
    {
        Weapon.WeaponType enemyWeapon = enemy.GetComponent<Enemy>().weapon;

        int i = 0;
        foreach (Weapon.WeaponType weapon in weaponList)
        {
            if (weapon == enemyWeapon)
            {
                uiManager.IncreaseScore(weaponTierList[i]);
                enemy.GetComponent<Enemy>().hasWeapon = true;
                enemy.GetComponent<Enemy>().weapon = weapon;
                enemy.GetComponent<Enemy>().holdingWeaponTier = weaponTierList[i];
                weaponList.Remove(weapon);
                weaponTierList.RemoveAt(i);
                enemySpawner.SpawnSingle();
                uiManager.weaponsSold += 1;
                return weapon;
            }
            i++;
        }

        return Weapon.WeaponType.None;
    }

    void ActivateBoots()
    {
        uiManager.powerupsUsed += 1;
        playerMovement.isDashing = true;
        playerMovement.moveSpeed += 5;
        Invoke("ResetPlayerSpeed", 3);
    }

    void ResetPlayerSpeed()
    {
        playerMovement.ResetSpeed();
    }

    void ActivateWings()
    {
        //no.
    }

    void ActivateCloak()
    {
        uiManager.powerupsUsed += 1;
        playerMovement.StartCloakTimer();
    }

    void ActivateBell()
    {
        uiManager.powerupsUsed += 1;
        playerMovement.MarkLocation();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (weaponList.Count == 0)
            {
                return;
            }
            if (weaponList[0] == Weapon.WeaponType.Boots)
            {
                ActivateBoots();
                weaponList.RemoveAt(0);
                weaponTierList.RemoveAt(0);
            }
            else if (weaponList[0] == Weapon.WeaponType.Wings)
            {
                ActivateWings();
                weaponList.RemoveAt(0);
                weaponTierList.RemoveAt(0);
            }
            else if (weaponList[0] == Weapon.WeaponType.Cloak)
            {
                ActivateCloak();
                weaponList.RemoveAt(0);
                weaponTierList.RemoveAt(0);
            }
            else if (weaponList[0] == Weapon.WeaponType.Bell)
            {
                ActivateBell();
                weaponTierList.RemoveAt(0);
                weaponList.RemoveAt(0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(weaponList.Count < 2)
            {
                return;
            }
            if (weaponList[1] == Weapon.WeaponType.Boots)
            {
                ActivateBoots();
                weaponTierList.RemoveAt(1);
                weaponList.RemoveAt(1);
            }
            else if (weaponList[1] == Weapon.WeaponType.Wings)
            {
                ActivateWings();
                weaponTierList.RemoveAt(1);
                weaponList.RemoveAt(1);
            }
            else if (weaponList[1] == Weapon.WeaponType.Cloak)
            {
                ActivateCloak();
                weaponTierList.RemoveAt(1);
                weaponList.RemoveAt(1);
            }
            else if (weaponList[1] == Weapon.WeaponType.Bell)
            {
                ActivateBell();
                weaponTierList.RemoveAt(1);
                weaponList.RemoveAt(1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (weaponList.Count < 3)
            {
                return;
            }
            if (weaponList[2] == Weapon.WeaponType.Boots)
            {
                ActivateBoots();
                weaponTierList.RemoveAt(2);
                weaponList.RemoveAt(2);
            }
            else if (weaponList[2] == Weapon.WeaponType.Wings)
            {
                ActivateWings();
                weaponTierList.RemoveAt(2);
                weaponList.RemoveAt(2);
            }
            else if (weaponList[2] == Weapon.WeaponType.Cloak)
            {
                ActivateCloak();
                weaponTierList.RemoveAt(2);
                weaponList.RemoveAt(2);
            }
            else if (weaponList[2] == Weapon.WeaponType.Bell)
            {
                ActivateBell();
                weaponTierList.RemoveAt(2);
                weaponList.RemoveAt(2);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (weaponList.Count < 4)
            {
                return;
            }
            else if (weaponList[3] == Weapon.WeaponType.Boots)
            {
                ActivateBoots();
                weaponTierList.RemoveAt(3);
                weaponList.RemoveAt(3);
            }
            else if (weaponList[3] == Weapon.WeaponType.Wings)
            {
                ActivateWings();
                weaponTierList.RemoveAt(3);
                weaponList.RemoveAt(3);
            }
            else if (weaponList[3] == Weapon.WeaponType.Cloak)
            {
                ActivateCloak();
                weaponTierList.RemoveAt(3);
                weaponList.RemoveAt(3);
            }
            else if (weaponList[3] == Weapon.WeaponType.Bell)
            {
                ActivateBell();
                weaponTierList.RemoveAt(3);
                weaponList.RemoveAt(3);
            }
        }
    }
}
