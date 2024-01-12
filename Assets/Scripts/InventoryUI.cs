using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public List<Image> inventoryListUI;
    public Image slotOne;
    public Image slotTwo;
    public Image slotThree;
    public Image slotFour;
    public Sprite sword1;
    public Sprite sword2;
    public Sprite sword3;
    public Sprite spear1;
    public Sprite spear2;
    public Sprite spear3;
    public Sprite axe1;
    public Sprite axe2;
    public Sprite axe3;
    public Sprite mace1;
    public Sprite mace2;
    public Sprite mace3;
    public Sprite boots;
    public Sprite wings;
    public Sprite cloak;
    public Sprite bell;

    public Inventory playerInventory;

    private void Start()
    {
        playerInventory = GameObject.Find("Player").GetComponent<Inventory>();
        inventoryListUI.Add(slotOne);
        inventoryListUI.Add(slotTwo);
        inventoryListUI.Add(slotThree);
        inventoryListUI.Add(slotFour);
        slotOne.enabled = false;
        slotTwo.enabled = false;
        slotThree.enabled = false;
        slotFour.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        for(int i = 0; i < playerInventory.weaponList.Count; i++)
        {
            inventoryListUI[i].enabled = true;

            if (playerInventory.weaponList[i] == Weapon.WeaponType.Sword)
            {
                if(playerInventory.weaponTierList[i] == 1)
                {
                    inventoryListUI[i].sprite = sword1;
                }
                if (playerInventory.weaponTierList[i] == 2)
                {
                    inventoryListUI[i].sprite = sword2;
                }
                if (playerInventory.weaponTierList[i] == 3)
                {
                    inventoryListUI[i].sprite = sword3;
                }
            }
            else if (playerInventory.weaponList[i] == Weapon.WeaponType.Spear)
            {
                if (playerInventory.weaponTierList[i] == 1)
                {
                    inventoryListUI[i].sprite = spear1;
                }
                if (playerInventory.weaponTierList[i] == 2)
                {
                    inventoryListUI[i].sprite = spear2;
                }
                if (playerInventory.weaponTierList[i] == 3)
                {
                    inventoryListUI[i].sprite = spear3;
                }
            }
            else if (playerInventory.weaponList[i] == Weapon.WeaponType.Axe)
            {
                if (playerInventory.weaponTierList[i] == 1)
                {
                    inventoryListUI[i].sprite = axe1;
                }
                if (playerInventory.weaponTierList[i] == 2)
                {
                    inventoryListUI[i].sprite = axe2;
                }
                if (playerInventory.weaponTierList[i] == 3)
                {
                    inventoryListUI[i].sprite = axe3;
                }
            }
            else if (playerInventory.weaponList[i] == Weapon.WeaponType.Mace)
            {
                if (playerInventory.weaponTierList[i] == 1)
                {
                    inventoryListUI[i].sprite = mace1;
                }
                if (playerInventory.weaponTierList[i] == 2)
                {
                    inventoryListUI[i].sprite = mace2;
                }
                if (playerInventory.weaponTierList[i] == 3)
                {
                    inventoryListUI[i].sprite = mace3;
                }
            }
            else if (playerInventory.weaponList[i] == Weapon.WeaponType.Boots)
            {
                inventoryListUI[i].sprite = boots;
            }
            else if (playerInventory.weaponList[i] == Weapon.WeaponType.Wings)
            {
                inventoryListUI[i].sprite = wings;
            }
            else if (playerInventory.weaponList[i] == Weapon.WeaponType.Cloak)
            {
                inventoryListUI[i].sprite = cloak;
            }
            else if (playerInventory.weaponList[i] == Weapon.WeaponType.Bell)
            {
                inventoryListUI[i].sprite = bell;
            }
        }

        if(playerInventory.weaponList.Count < 4)
        {
            for(int i = playerInventory.weaponList.Count; i < 4; i++)
            {
                inventoryListUI[i].enabled = false;
            }
        }

        /*
        //this code is an affront to god. I apologize.

        if(playerInventory.weaponList.Count < 1)
        {
            slotOne.enabled = false;
            slotTwo.enabled = false;
            slotThree.enabled = false;
            slotFour.enabled = false;
            return;
        }

        if (playerInventory.weaponList.Count < 2)
        {
            slotOne.enabled = true;
            slotTwo.enabled = false;

            if (playerInventory.weaponList[0] == Weapon.WeaponType.Sword)
            {
                slotOne.sprite = sword;
            }
            else if (playerInventory.weaponList[0] == Weapon.WeaponType.Spear)
            {
                slotOne.sprite = spear;
            }
            else if (playerInventory.weaponList[0] == Weapon.WeaponType.Axe)
            {
                slotOne.sprite = axe;
            }
            else if (playerInventory.weaponList[0] == Weapon.WeaponType.Mace)
            {
                slotOne.sprite = mace;
            }

            return;
        }

        if (playerInventory.weaponList.Count < 3)
        {
            slotOne.enabled = true;
            slotTwo.enabled = true;
            slotThree.enabled = false;

            if (playerInventory.weaponList[0] == Weapon.WeaponType.Sword)
            {
                slotOne.sprite = sword;
            }
            else if (playerInventory.weaponList[0] == Weapon.WeaponType.Spear)
            {
                slotOne.sprite = spear;
            }
            else if (playerInventory.weaponList[0] == Weapon.WeaponType.Axe)
            {
                slotOne.sprite = axe;
            }
            else if (playerInventory.weaponList[0] == Weapon.WeaponType.Mace)
            {
                slotOne.sprite = mace;
            }

            if (playerInventory.weaponList[1] == Weapon.WeaponType.Sword)
            {
                slotTwo.sprite = sword;
            }
            else if (playerInventory.weaponList[1] == Weapon.WeaponType.Spear)
            {
                slotTwo.sprite = spear;
            }
            else if (playerInventory.weaponList[1] == Weapon.WeaponType.Axe)
            {
                slotTwo.sprite = axe;
            }
            else if (playerInventory.weaponList[1] == Weapon.WeaponType.Mace)
            {
                slotTwo.sprite = mace;
            }

            return;
        }

        if (playerInventory.weaponList.Count < 4)
        {
            slotOne.enabled = true;
            slotTwo.enabled = true;
            slotThree.enabled = true;
            slotFour.enabled = false;
            if (playerInventory.weaponList[0] == Weapon.WeaponType.Sword)
            {
                slotOne.sprite = sword;
            }
            else if (playerInventory.weaponList[0] == Weapon.WeaponType.Spear)
            {
                slotOne.sprite = spear;
            }
            else if (playerInventory.weaponList[0] == Weapon.WeaponType.Axe)
            {
                slotOne.sprite = axe;
            }
            else if (playerInventory.weaponList[0] == Weapon.WeaponType.Mace)
            {
                slotOne.sprite = mace;
            }

            if (playerInventory.weaponList[1] == Weapon.WeaponType.Sword)
            {
                slotTwo.sprite = sword;
            }
            else if (playerInventory.weaponList[1] == Weapon.WeaponType.Spear)
            {
                slotTwo.sprite = spear;
            }
            else if (playerInventory.weaponList[1] == Weapon.WeaponType.Axe)
            {
                slotTwo.sprite = axe;
            }
            else if (playerInventory.weaponList[1] == Weapon.WeaponType.Mace)
            {
                slotTwo.sprite = mace;
            }
            if (playerInventory.weaponList[2] == Weapon.WeaponType.Sword)
            {
                slotThree.sprite = sword;
            }
            else if (playerInventory.weaponList[2] == Weapon.WeaponType.Spear)
            {
                slotThree.sprite = spear;
            }
            else if (playerInventory.weaponList[2] == Weapon.WeaponType.Axe)
            {
                slotThree.sprite = axe;
            }
            else if (playerInventory.weaponList[2] == Weapon.WeaponType.Mace)
            {
                slotThree.sprite = mace;
            }

            return;
        }

        if (playerInventory.weaponList.Count == 4)
        {
            slotOne.enabled = true;
            slotTwo.enabled = true;
            slotThree.enabled = true;
            slotFour.enabled = true;
            if (playerInventory.weaponList[0] == Weapon.WeaponType.Sword)
            {
                slotOne.sprite = sword;
            }
            else if (playerInventory.weaponList[0] == Weapon.WeaponType.Spear)
            {
                slotOne.sprite = spear;
            }
            else if (playerInventory.weaponList[0] == Weapon.WeaponType.Axe)
            {
                slotOne.sprite = axe;
            }
            else if (playerInventory.weaponList[0] == Weapon.WeaponType.Mace)
            {
                slotOne.sprite = mace;
            }

            if (playerInventory.weaponList[1] == Weapon.WeaponType.Sword)
            {
                slotTwo.sprite = sword;
            }
            else if (playerInventory.weaponList[1] == Weapon.WeaponType.Spear)
            {
                slotTwo.sprite = spear;
            }
            else if (playerInventory.weaponList[1] == Weapon.WeaponType.Axe)
            {
                slotTwo.sprite = axe;
            }
            else if (playerInventory.weaponList[1] == Weapon.WeaponType.Mace)
            {
                slotTwo.sprite = mace;
            }
            if (playerInventory.weaponList[2] == Weapon.WeaponType.Sword)
            {
                slotThree.sprite = sword;
            }
            else if (playerInventory.weaponList[2] == Weapon.WeaponType.Spear)
            {
                slotThree.sprite = spear;
            }
            else if (playerInventory.weaponList[2] == Weapon.WeaponType.Axe)
            {
                slotThree.sprite = axe;
            }
            else if (playerInventory.weaponList[2] == Weapon.WeaponType.Mace)
            {
                slotThree.sprite = mace;
            }
            if (playerInventory.weaponList[3] == Weapon.WeaponType.Sword)
            {
                slotFour.sprite = sword;
            }
            else if (playerInventory.weaponList[3] == Weapon.WeaponType.Spear)
            {
                slotFour.sprite = spear;
            }
            else if (playerInventory.weaponList[3] == Weapon.WeaponType.Axe)
            {
                slotFour.sprite = axe;
            }
            else if (playerInventory.weaponList[3] == Weapon.WeaponType.Mace)
            {
                slotFour.sprite = mace;
            }

            return;
        }
        */
    }
}
