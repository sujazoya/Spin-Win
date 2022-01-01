using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScratchCardEffect : MonoBehaviour
{
    [SerializeField] GameObject maskPrefab;
    [SerializeField] GameObject cardFront;
    private bool isPressed = false;
    bool scratchStarted;

    [SerializeField] Button addButton;
    [SerializeField] Text   ammountText;
    [SerializeField] Text   rewardAmmount;
    [SerializeField] Text   scratchCount;
    string scratchkey = "scratchkey";   
    [HideInInspector] public List<GameObject> maskObjects;
    int currentRewardAmmount;
    private void Awake()
    {      
    }
    void StartPlay()
    {
        currentRewardAmmount = Random.Range(0, 50);
        rewardAmmount.text = currentRewardAmmount.ToString();
        ammountText.text = Game.TotalCoins.ToString();
        scratchCount.text = Game.TotalScratch.ToString();
        if (maskObjects.Count > 0)
        {
            for (int i = 0; i < maskObjects.Count; i++)
            {
                Destroy(maskObjects[i]);
            }
            maskObjects.Clear();
        }        
        cardFront.SetActive(true);
        addButton.gameObject.SetActive(false);
        scratchStarted = false;
        Game.gameStatus = Game.GameStatus.isPlaying;

    }
    // Start is called before the first frame update
    void Start()
    {
        addButton.onClick.AddListener(AddToBalance);
        if (!PlayerPrefs.HasKey(scratchkey))
        {
            Game.TotalScratch = 30;
            PlayerPrefs.SetString(scratchkey, scratchkey);
        }
        StartPlay();
    }
    void AddToBalance()
    {
        Game.TotalCoins += currentRewardAmmount;
        StartPlay();
    }
    IEnumerator OffCardFrnt()
    {
        yield return new WaitForSeconds(10f);
        if (cardFront)
        {
            cardFront.SetActive(false);
        }
        addButton.gameObject.SetActive(true);
        Game.gameStatus = Game.GameStatus.isGameWin;
        Game.TotalScratch--;
    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 2;
        if(isPressed == true)
        {
            
            GameObject maskSprite = Instantiate(maskPrefab);
            maskSprite.transform.position = mousePos;
            maskSprite.transform.rotation = Quaternion.identity;
            maskSprite.transform.parent = gameObject.transform;
            maskObjects.Add(maskSprite);
        }
        else
        {
            
        }
        if (Input.GetMouseButtonDown(0) && Game.gameStatus == Game.GameStatus.isPlaying)
        {           
            isPressed = true;
            if (!scratchStarted)
            {
                StartCoroutine(OffCardFrnt());
                scratchStarted = true;
            }
        }
        else
             if (Input.GetMouseButtonUp(0))
        {
            isPressed = false;
        }
        
    }
}
