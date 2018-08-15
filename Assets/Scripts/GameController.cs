
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GoogleMobileAds.Api;
public class GameController : MonoBehaviour {
    public int score;
    public Text scoreText;
    public LineSetup lineSetup;
    public LineSetupTop lineSetupTop;
    public GameObject coinPrefab;
    public GameObject player;
    public Vector2 checkVector;
    public GameObject redSpikePrefab;
    public GameObject greenSpikePrefab;
    public GameObject ball;
    public GameObject wreckingBallPrefab;
    public GameObject crystalPrefab;
    public GameObject laserSticksPrefab;
    public Material lasersColor;
    public Player ballPlayer;
    public GameObject circlePrefab;
    public float metre;
    public int meter;
    public  GameObject scoreUI;
    public GameObject lastScoreText;
    public GameObject bestScoreText;
    public GameObject collectedCoinText;
    
    public GameObject startUI;
    public GameObject powerUpButton;
    public GameObject coinText;
    public GameObject notificationText;
    public bool gameContinue = true;
    public bool canVibrate;
    public Slider slider;

    public bool isCoroutine=false;
    public bool hasShield = false;

    public GameObject ballPrefab;

    public GameObject lifeText;
    public GameObject shieldText;
    public GameObject magnetText;
    public GameObject lastChanceUI;
    public bool canMagnet = false;
    public GameObject playerPrefab;
    Coroutine co;
    public GameObject magnet;
    public GameObject magnetPrefab;
    public GameObject bottomMenu;

    public GameObject DeathText;
    InterstitialAd interstitial;
    public GameObject reviewPanel;
    public GameObject ingamestartpanel;
    public GameObject playservicenotifyPanel;
    public GameObject brakeButton;
    public RewardBasedVideoAd rewardBasedVideo;

    // Use this for initialization
    private void Awake()
    {

        defaultSettings();
        Application.targetFrameRate = 60;
        canVibrate = PlayerPrefs.GetInt("vibration") == 1 ? true : false;
        updateStocks();
        
    }
    void Start () {

        if (PlayerPrefs.GetInt("playedTime") == 10)
        {

            reviewPanel.SetActive(true);
            ingamestartpanel.SetActive(false);
        }
        if(PlayerPrefs.GetInt("adTime")%3==0){
            AdmobScript.instance.adGive();
            Debug.Log("reklamlarrrr");
        }
       
        score = 0;
        lineSetup = GameObject.Find("LineSetup").GetComponent<LineSetup>();
        lineSetupTop = GameObject.Find("LineSetupTop").GetComponent<LineSetupTop>();
        ball = GameObject.Find("Ball");
        ballPlayer = ball.GetComponent<Player>();
        metre = ball.transform.position.x;
      
    }

    public void closeWindow()
    {
        playservicenotifyPanel.SetActive(false);
        
    }
    public void closeReview()
    {
        reviewPanel.SetActive(false);
        ingamestartpanel.SetActive(true);
    }
    public void makeReview()
    {
        PlayerPrefs.SetInt("totalCoin", PlayerPrefs.GetInt("totalCoin") + 300);
        Application.OpenURL("market://details?id=" + Application.productName);
        updateStocks();
    }
    void Update () {
        if(gameContinue)
        if ((int)metre < (int)ball.transform.position.x)
        {
            increaseMeter();
            metre = ball.transform.position.x;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
    public GameObject notificationTire()
    {
        notificationText.SetActive(true);
        return notificationText;
    }
    public void spawnWreckingBall(int indexOfArray)
    {

        if (lineSetupTop.lineArray[indexOfArray].GetComponent<LineRenderer>().enabled)
        {
            EdgeCollider2D col = lineSetupTop.lineArray[indexOfArray].GetComponent<EdgeCollider2D>();
            int thatPos = Random.Range(1, col.pointCount);
            Vector3 obj2 = col.points[thatPos];
            Vector3 pos = new Vector3(obj2.x, Random.Range(obj2.y, 0), 0f);
            GameObject wreckingBall = Instantiate(wreckingBallPrefab, pos, Quaternion.identity);
            wreckingBall.GetComponent<SpringJoint2D>().connectedAnchor = obj2;
            wreckingBall.GetComponent<LineRenderer>().startWidth = 0.1f;
            wreckingBall.GetComponent<LineRenderer>().endWidth = 0.1f;

            wreckingBall.GetComponent<LineRenderer>().SetPosition(0, obj2);
            wreckingBall.GetComponent<LineRenderer>().SetPosition(1, wreckingBall.transform.position);
            wreckingBall.GetComponent<LineRenderer>().numCornerVertices = 50;
        }
        
        
    }
    public void increaseScore(int scoreNumber)
    {
        score+=scoreNumber;
        coinText.GetComponent<Text>().text = score + "";
    }
    public void increaseMeter()
    {
         meter = (int)(ball.transform.position.x-ballPlayer.firstPos.x)/2;
        scoreText.text = meter+" m";
        if (meter % 100 == 0 && meter!=0) {
            lineSetup.enemyObjectsRate+=0.05f;
            lineSetup.coinRate+=0.02f;
            lineSetup.crystalRate += 0.02f;
            lineSetup.circleRate += 0.1f;
            player.GetComponent<Player>().limitSpeed +=0.8f;
        }
    }
    public void spawnCoin(int indexOfArray,int howMany)
    {
        EdgeCollider2D col1 = lineSetup.lineArray[indexOfArray].GetComponent<EdgeCollider2D>();
        int thatPos = Random.Range(1, col1.pointCount);
        Vector3 obj1 = col1.points[thatPos];
        EdgeCollider2D col2 = lineSetupTop.lineArray[indexOfArray].GetComponent<EdgeCollider2D>();
        Vector3 obj2 = col2.points[thatPos];

       
        Vector3 pos = obj1 + (obj2 - obj1) * 0.23f;
        Vector3 direction = (lineSetup.lineArray[indexOfArray].GetComponent<Line>().endPoint - lineSetup.lineArray[indexOfArray].GetComponent<Line>().startPoint).normalized;
        for (int i = 0; i < howMany; i++)
        {
            Instantiate(coinPrefab, pos, Quaternion.identity);  
            pos += direction * 1;
        }
    }
    public void spawnCrystal(int indexOfArray)
    {
        EdgeCollider2D col1 = lineSetup.lineArray[indexOfArray].GetComponent<EdgeCollider2D>();
        int thatPos = Random.Range(1, col1.pointCount);
        Vector3 obj1 = col1.points[thatPos];
        EdgeCollider2D col2 = lineSetupTop.lineArray[indexOfArray].GetComponent<EdgeCollider2D>();
        Vector3 obj2 = col2.points[ thatPos];
        Vector3 pos = new Vector3(obj1.x, Random.Range(obj1.y+0.5f, obj2.y-0.5f), 0f);
        Instantiate(crystalPrefab, pos, Quaternion.identity);
    }
    public void spawnRedSpike(int indexOfArray)
    {
        EdgeCollider2D col1 = lineSetup.lineArray[indexOfArray].GetComponent<EdgeCollider2D>();
        int thatPos = Random.Range(0, col1.pointCount);
        Vector3 obj1 = col1.points[ thatPos];
        EdgeCollider2D col2 = lineSetupTop.lineArray[indexOfArray].GetComponent<EdgeCollider2D>();
        Vector3 obj2 = col2.points[thatPos];
        Vector3 pos = new Vector3(obj1.x, Random.Range(obj1.y+1, obj2.y-1), 0f);
        Instantiate(redSpikePrefab,pos, Quaternion.identity);
    }
    public void spawnCircleAttackers(int indexOfArray,int howMany)
    {
        StartCoroutine(spawnCoroutine(indexOfArray,howMany));   
    }

    IEnumerator spawnCoroutine(int indexOfArray, int howMany)
    {
        EdgeCollider2D col1 = lineSetup.lineArray[indexOfArray].GetComponent<EdgeCollider2D>();
        Vector3 obj1 = col1.points[col1.pointCount / 2];
        EdgeCollider2D col2 = lineSetupTop.lineArray[indexOfArray].GetComponent<EdgeCollider2D>();
        Vector3 obj2 = col2.points[col2.pointCount / 2];
        int i = 0;
        Vector3 pos;
        while (i<howMany)
        {
            pos = new Vector3(obj1.x, Random.Range(obj1.y, obj2.y), 0f);
            Instantiate(circlePrefab, pos, Quaternion.identity);
            i++;
            yield return new WaitForSeconds(Random.value);
            
        }
    }
    public void spawnGreenSpike(int indexOfArray)
    {
        EdgeCollider2D col1 = lineSetup.lineArray[indexOfArray].GetComponent<EdgeCollider2D>();
        int thatPos = Random.Range(1, col1.pointCount);
        Vector3 obj1 = col1.points[thatPos];
        EdgeCollider2D col2 = lineSetupTop.lineArray[indexOfArray].GetComponent<EdgeCollider2D>();
        Vector3 obj2 = col2.points[thatPos];
        Vector3 pos = new Vector3(obj1.x, Random.Range(obj1.y + 2, obj2.y - 2), 0f);
        Instantiate(greenSpikePrefab, pos, Quaternion.identity);
    }
    public void restart()
    {

        
        Time.timeScale = 1;
   


        PlayerPrefs.SetInt("playedTime", PlayerPrefs.GetInt("playedTime") + 1);
        SceneManager.LoadScene(0);
        
    }
    
    public void lastChance()
    {

        lastChanceUI.SetActive(true);
        co=  StartCoroutine(SliderDecrease());
    }
    public void chanceGone()
    {
        PlayerPrefs.SetInt("adTime", PlayerPrefs.GetInt("adTime") + 1);
        int cause=PlayerPrefs.GetInt("deathCause");
       
        if(co!=null)
        StopCoroutine(co);
        switch (cause)
        {
            case 1:
                DeathText.SetActive(true);
                DeathText.GetComponent<Text>().text = "Dont touch RED things";
                break;

            case 2:
                DeathText.SetActive(true);
                DeathText.GetComponent<Text>().text = "You left the area.";
                break;
        }
        lastChanceUI.SetActive(false);
        powerUpButton.SetActive(false);
        scoreUI.SetActive(true);
        bottomMenu.SetActive(true);
        lastScoreText.GetComponent<Text>().text = "score : " + scoreText.text;
        recordBest();
        bestScoreText.GetComponent<Text>().text = "best : " + PlayerPrefs.GetInt("best");
        collectedCoinText.GetComponent<Text>().text = "coin : " + score;
        totalCoin();
        
    }
   

    public void lost()
    {
        if(canVibrate)
        Handheld.Vibrate();
        //Time.timeScale = 0;
        gameContinue = false;
        if (PlayerPrefs.GetInt("lifePowerUpCount") > 0)
        {
            lastChance();
            
           
            updateStocks();
        }
        else
        {
            chanceGone();
        }
    }
    IEnumerator SliderDecrease()
    {
        if (GameObject.Find("LosingText") != null)
        {
           Text losingtext = GameObject.Find("LosingText").GetComponent<Text>();
            int c = 3;
            while (true)
            {
                losingtext.text="Last Chance Missing In "+c+"";
                yield return new WaitForSeconds(1f);
                c--;
                if (c <= 0)
                {

                    chanceGone();
                    
                    break;
                }
                   
            }
           
        }
        yield return null;
    }
    public void showLeaderBoards()
    {

        if (Social.localUser.authenticated)
        {
            GooglePlayScript.AddScoreToLeaderBoard(GPGSIds.leaderboard_leaderboard);
            GooglePlayScript.ShowLeaderBoardUI();

        }
        else{
            playservicenotifyPanel.SetActive(true);
        }
    }
   
    public void recordBest()
    {
        if (PlayerPrefs.GetInt("best") <= meter) {
            PlayerPrefs.SetInt("best", meter);
            GooglePlayScript.AddScoreToLeaderBoard(GPGSIds.leaderboard_leaderboard);
                }
    }
    public void defaultSettings()
    {
        // Save boolean using PlayerPrefs
        if (!PlayerPrefs.HasKey("vibration"))
            PlayerPrefs.SetInt("vibration", 1);
        if (!PlayerPrefs.HasKey("lifePowerUpCount"))
            PlayerPrefs.SetInt("lifePowerUpCount", 3);
        if (!PlayerPrefs.HasKey("shieldCount"))
            PlayerPrefs.SetInt("shieldCount", 3);
        if (!PlayerPrefs.HasKey("magnetPowerUpCount"))
            PlayerPrefs.SetInt("magnetPowerUpCount", 3);
        if (!PlayerPrefs.HasKey("adFree"))
            PlayerPrefs.SetInt("adFree", 0);
        if (!PlayerPrefs.HasKey("shieldPrice"))
            PlayerPrefs.SetInt("shieldPrice", 60);
        if (!PlayerPrefs.HasKey("lifePrice"))
            PlayerPrefs.SetInt("lifePrice", 100);
        if (!PlayerPrefs.HasKey("magnetPrice"))
            PlayerPrefs.SetInt("magnetPrice", 60);
        if (!PlayerPrefs.HasKey("ballColorPrice"))
            PlayerPrefs.SetInt("ballColorPrice", 100);
        if (!PlayerPrefs.HasKey("totalCoin"))
            PlayerPrefs.SetInt("totalCoin", 100);
        if (!PlayerPrefs.HasKey("canAd"))
        {
            PlayerPrefs.SetInt("canAd", 1);
        }
        if (!PlayerPrefs.HasKey("totalCoin"))
        {
            PlayerPrefs.SetInt("totalCoin", 400);
        }
        if (!PlayerPrefs.HasKey("playedTime"))
        {
            PlayerPrefs.SetInt("playedTime",0);
        }
       
    }
    public void totalCoin()
    {
        PlayerPrefs.SetInt("totalCoin", score + PlayerPrefs.GetInt("totalCoin"));
    }
    public void shieldTime()
    {
       
        if (!Player.findChildTechnique(player, "shield").activeInHierarchy && gameContinue)
        {
            
            if (PlayerPrefs.GetInt("shieldCount") > 0)
            {
                Debug.Log("giremedim");
                hasShield = true;
                Player.findChildTechnique(player, "shield").SetActive(true);
                PlayerPrefs.SetInt("shieldCount", PlayerPrefs.GetInt("shieldCount") - 1);
                updateStocks();
            }
            else
            {
                Debug.Log("hayda");
            }
        }
    }
    public void magnetTime()
    {
        if (!isCoroutine)
        {
            if (PlayerPrefs.GetInt("magnetPowerUpCount") > 0 && gameContinue)
            {
                PlayerPrefs.SetInt("magnetPowerUpCount", PlayerPrefs.GetInt("magnetPowerUpCount") - 1);
                updateStocks();
                magnet = Instantiate(magnetPrefab, player.transform.position, Quaternion.Euler(0f, 0f, -90f));
                notificationText.SetActive(true);
                notificationText.GetComponent<Text>().text = "Coins will be with you";

                canMagnet = true;
                StartCoroutine(magnetTimer());
               
            }
        }
    }
    IEnumerator magnetTimer()
    {
        int magnetCounter = 25;
        while (magnetCounter > 0)
        {
            if (gameContinue == false)
            {
                break;
            }
            isCoroutine = true;
            if (magnetCounter == 20) { notificationText.GetComponent<Text>().text=""; }
            
            if (magnetCounter < 11)
            {
                notificationText.GetComponent<Text>().text = "Magnet Goes In " + magnetCounter;
            }
         
            yield return new WaitForSeconds(1);
            magnetCounter--;

        }
        isCoroutine = false;
        notificationText.SetActive(false);
        stopMagnet();
    }
    public void stopMagnet()
    {
        canMagnet = false;
        Destroy(magnet);
        
    }
    public void updateStocks()
    {
        lifeText.GetComponent<Text>().text = "x" + PlayerPrefs.GetInt("lifePowerUpCount");
        shieldText.GetComponent<Text>().text = "x" + PlayerPrefs.GetInt("shieldCount");
        magnetText.GetComponent<Text>().text = "x" + PlayerPrefs.GetInt("magnetPowerUpCount");
    }
    public void turnBackToLife()
    {
        destroyOnRespawn();
        PlayerPrefs.SetInt("lifePowerUpCount", PlayerPrefs.GetInt("lifePowerUpCount") - 1);
        updateStocks();
        StopCoroutine(co);
        lastChanceUI.SetActive(false);
        Vector3 pos = Camera.main.transform.position;
        pos.z = 0;
        ball=Instantiate(playerPrefab, pos, Quaternion.identity);
        player = ball;
        noTouchFirst();
        
        gameContinue = true;
        CancelInvoke();
       
    }
    public void destroyOnRespawn()
    {
        GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        float deathPos = Camera.main.transform.position.x + 30f;
        foreach (GameObject enemy in enemyArray)
        {
            if (enemy.transform.position.x < deathPos)
            {
                Destroy(enemy);

            }
        }
    }
    public void noTouchFirst()
    {
        notificationText.SetActive(true);
        co=StartCoroutine(countdown());
       
        ball.GetComponent<Rigidbody2D>().isKinematic = true;
    }
    IEnumerator countdown()
    {
        int secondCounter = 3;
        while (secondCounter>0)
        {

            notificationText.GetComponent<Text>().text =""+ secondCounter;
            yield return new WaitForSeconds(1);
            secondCounter--;
        }
        notificationText.SetActive(false);
        ball.GetComponent<Rigidbody2D>().isKinematic = false;
    }
    public static GameObject findChildTechnique(GameObject ballType)
    {
        foreach (Transform child in ballType.transform)
        {
            return child.gameObject;
        }
        return null;
    }
}
