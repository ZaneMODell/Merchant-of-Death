using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hero : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    public List<GameObject> destinationPoints;
    public bool goingToPlayer;
    public Vector3 goingTo;
    public float exp = 0;
    public int level = 1;
    public float levelSpeedIncrease = 0.75f;
    public float chasingHeroSpeedIncrease = 0.25f;
    public float heroSpeedCap = 5;
    public bool isOriginalHero = false;
    public HeroSpawner heroSpawner;
    public GameObject heroSprite;
    public PlayerMovement playerMove;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        playerMove = player.GetComponent<PlayerMovement>();
        agent.destination = destinationPoints[Random.Range(0, destinationPoints.Count)].transform.position;
        goingTo = agent.destination;
        heroSpawner = GameObject.Find("HeroSpawner").GetComponent<HeroSpawner>();
        if(!isOriginalHero)
        {
            agent.speed = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isOriginalHero)
        {
            if(playerMove.isInvisible)
            {
                agent.destination = gameObject.transform.position;
            }
            else
            {
                agent.destination = player.transform.position;
            }
        }

        else if((Vector3.Distance(transform.position, agent.destination) < 0.75f && !goingToPlayer) || (goingToPlayer && Vector3.Distance(transform.position, agent.destination) < 0.75f))
        {
            agent.destination = destinationPoints[Random.Range(0, destinationPoints.Count)].transform.position;
            goingToPlayer = false;
            goingTo = agent.destination;
        }
        else if(playerMove.isMarked)
        {
            agent.destination = player.transform.position;
            goingTo = agent.destination;
            goingToPlayer = true;
        }

        Vector3 agentMoveVector = agent.velocity;

        if (Mathf.Abs(agentMoveVector.x) > Mathf.Abs(agentMoveVector.z))
        {
            //Going mostly horizontal

            if (agentMoveVector.x >= 0) //Going up
            {
                heroSprite.transform.eulerAngles = new Vector3(90, 90, 0);
            }
            else //Going down
            {
                heroSprite.transform.eulerAngles = new Vector3(90, 270, 0);
            }
        }
        else
        {
            //Going mostly vertical
            if (agentMoveVector.z >= 0) //Going right
            {
                heroSprite.transform.eulerAngles = new Vector3(90, 0, 0);
            }
            else //Going left
            {
                heroSprite.transform.eulerAngles = new Vector3(90, 180, 0);
            }
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            /*
             //* DEBUG TOOL! LEAVE OFF UNLESS DEBUGGING!
            if(player)
            {
                agent.destination = player.transform.position;
                goingTo = agent.destination;
                goingToPlayer = true;
            }
            */
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(!player.GetComponent<PlayerMovement>().isInvisible)
            {
                other.gameObject.GetComponent<PlayerMovement>().Die();
            }
        }
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().Die();
            exp += 25;
        }

        //This is where the difficulty scaling comes from
        if(exp >= 100)
        {
            exp = 0;
            level += 1;

            if (isOriginalHero)
            {
                agent.speed += chasingHeroSpeedIncrease;
                if (agent.speed > 2.1f)
                {
                    agent.speed = 1.5f;
                    if(GameObject.FindGameObjectsWithTag("Hero").Length < 6)
                    {
                        heroSpawner.SpawnHero();
                    }
                }
            }
            else
            {
                agent.speed += levelSpeedIncrease;
                if (agent.speed > heroSpeedCap)
                {
                    if (GameObject.FindGameObjectsWithTag("Hero").Length < 6)
                    {
                        agent.speed = 2f;
                        heroSpawner.SpawnHero();
                    }
                    else
                    {
                        agent.speed = heroSpeedCap;
                    }
                }
            }

        }
    }
}
