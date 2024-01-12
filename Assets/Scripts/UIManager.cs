using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameObject gameOverScreen;
    public float score = 0;
    public Text scoreText;
    public bool isMainMenu = false;
    public GameObject HowToPlay1;
    public GameObject HowToPlay2;
    public GameObject HowToPlay3;
    public GameObject HowToPlay4;
    public GameObject ControlsPage;
    public GameObject CreditsPage;
    public HeroSpawner heroSpawner;

    public Text gameOverStatsText;
    public int weaponsSold = 0;
    public bool longBeforeTimeHadAName = false;
    public int enemiesDead = 0;
    public int powerupsUsed = 0;
    public int numHeroes = 0;
    public string randomHeroName;

    public List<string> heroPrefixes = new List<string>();
    public List<string> heroNames = new List<string>();
    public List<string> heroSuffixes = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("Player"))
        {
            playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        }

        if (GameObject.Find("HeroSpawner"))
        {
            heroSpawner = GameObject.Find("HeroSpawner").GetComponent<HeroSpawner>();
        }

        if(GameObject.Find("GameOverStats"))
        {
            gameOverStatsText = GameObject.Find("GameOverStats").GetComponent<Text>();
        }
        randomHeroName = GenerateHeroName();
    }

    // Update is called once per frame
    void Update()
    {
        if(scoreText)
        {
            scoreText.text = score.ToString();
        }

        numHeroes = heroSpawner.numHeroes;

        if(playerMovement)
        {
            if (playerMovement.isDead)
            {
                gameOverScreen.SetActive(true);
                gameOverStatsText.text = "Caught by: " + randomHeroName + "\n \n" + "Weapons Sold: " + weaponsSold + "\n" + "Powerups Used: " + powerupsUsed + "\n"
                    + "Customers Dead: " + enemiesDead + "\n" + "Number of 'Heroes': " + numHeroes + "\n" + "Final Score: " + score;
            }
        }
    }

    public string GenerateHeroName()
    {
        int prefixIndex = Random.Range(0, heroPrefixes.Count);
        int nameIndex = Random.Range(0, heroNames.Count);
        int suffixIndex = Random.Range(0, heroSuffixes.Count);

        string heroName = heroPrefixes[prefixIndex] + " " + heroNames[nameIndex] + " " + heroSuffixes[suffixIndex];

        return heroName;
    }

    public void GoToMain()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void IncreaseScore(int weaponTier = 0)
    {
        if(weaponTier == 1)
        {
            score += 100;
        }
        if (weaponTier == 2)
        {
            score += 300;
        }
        if (weaponTier == 3)
        {
            score += 500;
        }
    }

    public void OpenPage(GameObject page)
    {
        page.SetActive(true);
    }

    public void PageChangeNext(string pageName)
    {
        //Checks which button was pressed on a page, denoted by pageName
        //Depending on which button was pressed, will make different pages appear/disappear
        if(pageName == "HowToPlay1")
        {
            
            HowToPlay1.SetActive(false);
            HowToPlay2.SetActive(true);
        }

        else if (pageName == "HowToPlay2")
        {
            
            HowToPlay2.SetActive(false);
            HowToPlay3.SetActive(true);
        }

        else if (pageName == "HowToPlay3")
        {
            
            HowToPlay3.SetActive(false);
            HowToPlay4.SetActive(true);
        }

        else if (pageName == "HowToPlay4")
        {
            
            HowToPlay4.SetActive(false);
            HowToPlay1.SetActive(true);
        }

    }

    public void PageChangePrevious(string pageName)
    {
        //Checks which button was pressed on a page, denoted by pageName
        //Depending on which button was pressed, will make different pages appear/disappear
        if (pageName == "HowToPlay1")
        {

            HowToPlay1.SetActive(false);
            HowToPlay4.SetActive(true);
        }

        else if (pageName == "HowToPlay2")
        {

            HowToPlay2.SetActive(false);
            HowToPlay1.SetActive(true);
        }

        else if (pageName == "HowToPlay3")
        {

            HowToPlay3.SetActive(false);
            HowToPlay2.SetActive(true);
        }

        else if (pageName == "HowToPlay4")
        {

            HowToPlay4.SetActive(false);
            HowToPlay3.SetActive(true);
        }
    }

    public void ClosePage(GameObject page)
    {
        page.SetActive(false);
    }
}
