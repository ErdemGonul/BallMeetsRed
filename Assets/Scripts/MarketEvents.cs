

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MarketEvents : MonoBehaviour {
    public GameObject coinText;
    public GameObject ballShow;
    public Sprite playerPrefabSprite;
    public Material playerColorMaterial;
    public int price;
    SavePlayer x;
    whatPlayerHas has;
    public GameObject ask;
    public GameObject clicked;
    public GameObject marketPanel;
    public GameObject buyCoinPanel;
    public GameObject lifeText;
    public GameObject shieldText;
    public GameObject magnetText;


    public GameObject lifePriceText;
    public GameObject shieldPriceText;
    public GameObject magnetPriceText;
  
    
    
    public int shieldPrice, lifePrice, magnetPrice, ballColorPrice;
    public void savePrices()
    {
        shieldPrice= PlayerPrefs.GetInt("shieldPrice");
        lifePrice= PlayerPrefs.GetInt("lifePrice");
        magnetPrice= PlayerPrefs.GetInt("magnetPrice");
        ballColorPrice= PlayerPrefs.GetInt("ballColorPrice");
    }
    // Use this for initialization
    private void Awake()
    {
        firstOpening();
        
    }
    public void setTextsOnMarket()
    {
        lifePriceText.GetComponent<Text>().text =lifePrice +"";
        shieldPriceText.GetComponent<Text>().text = shieldPrice + "";
        magnetPriceText.GetComponent<Text>().text = magnetPrice + "";
        
    }
    void Start () {
        
        ballShow = GameObject.Find("BallShow");
        x = readDataFromFile();
        playerColorMaterial = Resources.Load<Material>("PlayerMaterials/" + x.color);
        ballShow.GetComponent<SpriteRenderer>().sharedMaterial = playerColorMaterial;
        savePrices();
        setTextsOnMarket();

        updateStocks();
    }
   
    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonUp(0) &&(EventSystem.current.currentSelectedGameObject!=null))
        {
            clicked = EventSystem.current.currentSelectedGameObject;
            Debug.Log(clicked);
            if (clicked.tag == "PlayerButtons")
            {
                has = readItemsFromFile();

               
                if (!has.listPlayer.Contains(clicked.GetComponentInChildren<SpriteRenderer>().sharedMaterial.name))
                    buyItem();
                else
                    ballShow.GetComponent<SpriteRenderer>().material = Resources.Load<Material>("PlayerMaterials/" + clicked.GetComponentInChildren<SpriteRenderer>().sharedMaterial.name);
            }
            else if (clicked.name == "LifePowerUps" || clicked.name == "ShieldPowerUps" || clicked.name == "MagnetPowerUps")
                buyPowerUp();
            
        }
    }
    public void showLeaderBoards()
    {
        GooglePlayScript.ShowLeaderBoardUI();
    }
    public void loadGame(int index)
    {
        equipThem();
        SceneManager.LoadScene(index);

    }
    public void buyCoin()
    {
        ballShow.SetActive(false);
        marketPanel.SetActive(false);
        buyCoinPanel.SetActive(true);
    }


    public void equipThem()
    {
        
        int totalCoin = PlayerPrefs.GetInt("totalCoin");
       
          
            SavePlayer saved = new SavePlayer();
            if (ballShow.GetComponent<SpriteRenderer>().sharedMaterial != null)
            {
                string x = ballShow.GetComponent<SpriteRenderer>().sharedMaterial.name;
                saved.color = x;
            }
            else
            {
                saved.color = "whitePlayer";
            }
            string jsonVersion = JsonUtility.ToJson(saved);
            writeDataToFile(jsonVersion);

        
      
        

    }
    public void buyItem()
    {
        int totalCoin = PlayerPrefs.GetInt("totalCoin");
        has =readItemsFromFile();
        if (clicked.tag == "PlayerButtons")
        {
           
            if (totalCoin >= ballColorPrice && totalCoin > 0)
            {
                List<string> temp = has.listPlayer;
                temp.Add(clicked.GetComponentInChildren<SpriteRenderer>().sharedMaterial.name);
                has.listPlayer = temp;
                PlayerPrefs.SetInt("totalCoin", totalCoin - ballColorPrice);
            }
        }
        updateStocks();
        string jsonVersion = JsonUtility.ToJson(has);
            writeItemToFile(jsonVersion);
    }
   
    public void buyPowerUp()
    {
        int totalCoin = PlayerPrefs.GetInt("totalCoin");
        if (clicked.tag == "shieldPowerUp")
        {
            Debug.Log(totalCoin);
            if (totalCoin >= shieldPrice && totalCoin>0)
            {
                PlayerPrefs.SetInt("shieldCount", PlayerPrefs.GetInt("shieldCount") + 1);
                PlayerPrefs.SetInt("totalCoin", totalCoin - shieldPrice);
            }
        }
        else if (clicked.tag == "lifePowerUp" )
        {
            if (totalCoin >= lifePrice && totalCoin > 0)
            {
                PlayerPrefs.SetInt("lifePowerUpCount", PlayerPrefs.GetInt("lifePowerUpCount") + 1);
                PlayerPrefs.SetInt("totalCoin", totalCoin -lifePrice);
            }

        }
        else if (clicked.tag == "magnetPowerUp")
        {
            if (totalCoin >= magnetPrice && totalCoin > 0)
            {
                PlayerPrefs.SetInt("magnetPowerUpCount", PlayerPrefs.GetInt("magnetPowerUpCount") + 1);
                PlayerPrefs.SetInt("totalCoin",totalCoin-magnetPrice);
            }
        }
        updateStocks();
    }
    public void updateStocks()
    {
        lifeText.GetComponent<Text>().text = "x" + PlayerPrefs.GetInt("lifePowerUpCount");
        shieldText.GetComponent<Text>().text = "x" + PlayerPrefs.GetInt("shieldCount");
        magnetText.GetComponent<Text>().text = "x" + PlayerPrefs.GetInt("magnetPowerUpCount");

        coinText.GetComponent<Text>().text = PlayerPrefs.GetInt("totalCoin") + "";

    }
    public static void firstOpening()
    {
        

        string path = Application.persistentDataPath + "/saved.json";

        string path2 = Application.persistentDataPath + "/whatPlayerHas.json";
        if (!System.IO.File.Exists(path))
        {

            SavePlayer newSave = new SavePlayer();
            newSave.hat = null;
            newSave.color = "whitePlayer";
            newSave.glass = null;
            string jsonVersion1 = JsonUtility.ToJson(newSave);
            System.IO.File.WriteAllText(path, jsonVersion1);
        }
        if (!System.IO.File.Exists(path2))
        {

            whatPlayerHas newFile = new whatPlayerHas();
            newFile.listPlayer = new List<string>();
            newFile.listPlayer.Add("whitePlayer");
            newFile.listHat = new List<string>();
            newFile.listGlass = new List<string>();
            string jsonVersion = JsonUtility.ToJson(newFile);
            System.IO.File.WriteAllText(path2, jsonVersion);
        }
    }
    public GameObject findChildTechnique(GameObject ballType, string tag)
{
    foreach (Transform child in ballType.transform)
    {
        if (child.tag == tag)
        {

            return child.gameObject;


        }
    }
    return null;
}
    public static void writeDataToFile(string jsonString)
    {
        string path = Application.persistentDataPath + "/saved.json";
        if (System.IO.File.Exists(path))
            System.IO.File.WriteAllText(path, jsonString);
        else
            System.IO.File.Create(path);
    }
    public static void writeItemToFile(string jsonString)
    {
        string path = Application.persistentDataPath + "/whatPlayerHas.json";
        if (System.IO.File.Exists(path))
            System.IO.File.WriteAllText(path, jsonString);
        else
        {
            System.IO.File.Create(path);
        }
    }

    public static SavePlayer readDataFromFile()
    {
        string jsonString;
        string full_path = Application.persistentDataPath + "/saved.json";
        if (full_path.Contains("://") || full_path.Contains(":///"))
        {
            WWW www = new WWW(full_path);
            while (!www.isDone) { }
            jsonString = www.text.Trim();
        }
        else
            jsonString = System.IO.File.ReadAllText(full_path);
        return JsonUtility.FromJson<SavePlayer>(jsonString);
    }
    public whatPlayerHas readItemsFromFile()
    {
        string jsonString;
        string full_path = Application.persistentDataPath + "/whatPlayerHas.json";
        if (full_path.Contains("://") || full_path.Contains(":///"))
        {
            WWW www = new WWW(full_path);
            while (!www.isDone) { }
            jsonString = www.text.Trim();
        }
        else
            jsonString = System.IO.File.ReadAllText(full_path);
        return JsonUtility.FromJson<whatPlayerHas>(jsonString);
    }
    public void removeAdsSet()
    {
        PlayerPrefs.SetInt("canAd", 0);
    }
}


[SerializeField]
public class SavePlayer
{

    public string glass;
    public string hat;
    public string color;

    
}
[SerializeField]
public class whatPlayerHas
{
    public List<string> listPlayer;
    public List<string> listGlass;
    public List<string> listHat;

}
