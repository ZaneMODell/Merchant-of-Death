using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    [SerializeField] private float baseMoveSpeed = 1f;
    [SerializeField] private float timer = 0.0f;
    public bool isDashing = false;
    private Rigidbody rb;
    private GameObject playerSprite;
    public float inventoryWeight = 0.1f;
    public bool isDead = false;
    public bool isInvisible = false;
    public bool isMarked = false;

    public Weapon.WeaponType holdingWeapon = Weapon.WeaponType.None;

    public Inventory inventory;

    void Awake()
    {
        inventory = GetComponent<Inventory>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        playerSprite = GameObject.Find("PlayerSprite");
        Time.timeScale = 1;
        /*
         * axe!!!! we found it!!
        List<Transform> thingList = new List<Transform>(GameObject.FindObjectsOfType<Transform>());
        foreach(Transform thing in thingList)
        {
            thing.gameObject.hideFlags = HideFlags.None;
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        rb.velocity = moveSpeed * new Vector3(xInput, 0, yInput);

        if(!isDashing)
        {
            moveSpeed = baseMoveSpeed * (1 / ((inventoryWeight * inventory.weaponList.Count) + 1));
        }

        //Put all of the sprites into an arry and change the sprites at runtime by accessing indices
        if (xInput > 0) //Going right
        {
            playerSprite.transform.eulerAngles = new Vector3(90, 90, 0);
        }

        if (xInput < 0) //Going left
        {
            playerSprite.transform.eulerAngles = new Vector3(90, 270, 0);
        }

        if (yInput > 0) //Going up
        {
            playerSprite.transform.eulerAngles = new Vector3(90, 0, 0);
        }

        if (yInput < 0) //Going down
        {
            playerSprite.transform.eulerAngles = new Vector3(90, 180, 0);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            PanicButton();

        }
    }

    void Interact()
    {
        //If on a weapon, pick it up
        GameObject[] weaponList = GameObject.FindGameObjectsWithTag("Weapon");
        foreach (GameObject weapon in weaponList)
        {
            if (Vector3.Distance(weapon.transform.position, transform.position) < 0.4f)
            {
                inventory.AddWeapon(weapon.GetComponent<Weapon>());
                break;
            }
        }

        GameObject[] powerupList = GameObject.FindGameObjectsWithTag("Powerup");
        foreach (GameObject powerup in powerupList)
        {
            if (Vector3.Distance(powerup.transform.position, transform.position) < 0.4f)
            {
                inventory.AddPowerup(powerup.GetComponent<Powerup>());
                break;
            }
        }

        //If on an enemy that wants a weapon, remove the weapon from inventory, increase score, and change that enemy to having a weapon
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            //check if the player has a sellable item in the inventory
            if (Vector3.Distance(enemy.transform.position, transform.position) < 0.7f && !enemy.GetComponent<Enemy>().hasWeapon)
            {
                inventory.GiveWeapon(enemy);
                break;
            }
        }
    }

    public void Die()
    {
        Debug.Log("Game is over");
        Time.timeScale = 0;
        isDead = true;
    }

    public void PanicButton()
    {
        if(inventory.weaponList.Count > 0 && !isDashing)
        {
            isDashing = true;
            moveSpeed = baseMoveSpeed * ((inventory.weaponList.Count * 0.5f) + 1);
            inventory.weaponList.Clear();
            inventory.weaponTierList.Clear();
            Invoke("ResetSpeed", timer);
        }
    }
    public void ResetSpeed()
    {
        isDashing = false;
        moveSpeed = baseMoveSpeed;
    }
   
    public void StartCloakTimer()
    {
        isInvisible = true;
        playerSprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        Invoke("ResetCloak", 3);
    }

    void ResetCloak()
    {
        playerSprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        isInvisible = false;
    }

    public void MarkLocation()
    {
        isMarked = true;
        Invoke("ResetMark", 0.1f);
    }

    public void ResetMark()
    {
        isMarked = false;
    }
}
