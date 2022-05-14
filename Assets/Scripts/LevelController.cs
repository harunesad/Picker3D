using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LevelController : MonoBehaviour
{
    public GameObject currentLevel;
    public GameObject lastLevel;
    [SerializeField] private GameObject[] levels;
    [SerializeField] GameObject continueBut, replayBut;
    GameManager gameManager;
    Transform levelFinal;
    Transform lastCheck;
    int curLevel;

    [SerializeField]
    TextMeshProUGUI levelText, nextLevelText;
    List<GameObject> sceneLevel = new List<GameObject>();

    void Awake()
    {
        SetButtons(false);
        if (!PlayerPrefs.HasKey("level"))
        {
            PlayerPrefs.SetInt("level", 1);
        }
        gameManager = FindObjectOfType<GameManager>();
        CreateSameLevel();
        curLevel = PlayerPrefs.GetInt("level");
        PlayerPrefs.SetInt("level", curLevel);
        levelText.text = "" + PlayerPrefs.GetInt("level");
        nextLevelText.text = "" + (PlayerPrefs.GetInt("level") + 1);
    }

    public void SetButtons(bool tr)
    {
        continueBut.SetActive(tr);
        replayBut.SetActive(tr);
    }
    public void NextLevel() 
    {
        curLevel = PlayerPrefs.GetInt("level") + 1;
        PlayerPrefs.SetInt("level", curLevel);
        levelText.text = curLevel.ToString();
        nextLevelText.text = (curLevel + 1).ToString();
        lastLevel = currentLevel;
        currentLevel = levels[curLevel - 1];

        if (lastCheck != null)
        {
            Destroy(lastCheck.gameObject);
        }

        levelFinal = GameObject.FindGameObjectWithTag("LevelFinal").transform;
        lastCheck = levelFinal;

        if (PlayerPrefs.GetInt("level") > 4)
        {
            currentLevel = Instantiate(levels[PlayerPrefs.GetInt("level") - 5], lastCheck.position, Quaternion.identity);
            curLevel = PlayerPrefs.GetInt("level") - 4;
            PlayerPrefs.SetInt("level", curLevel);
            levelText.text = "" + PlayerPrefs.GetInt("level");
            nextLevelText.text = "" + (PlayerPrefs.GetInt("level") + 1);
        }
        else
        {
            currentLevel = Instantiate(levels[PlayerPrefs.GetInt("level") - 1], lastCheck.position, Quaternion.identity);
        }

        sceneLevel.Add(currentLevel);

        if (sceneLevel.Count >= 2)
        {
            sceneLevel.Remove(sceneLevel[0]);
        }
        SetButtons(false);
    }

    public void DestroyCurrentLevel()
    {
        Destroy(currentLevel);
    }

    public void DestroyLastLevel()
    {
        Destroy(lastLevel);
    }

    public void CreateSameLevel()
    {
        
        if (currentLevel != null)
        {
            DestroyCurrentLevel();
        }
        currentLevel = Instantiate(levels[PlayerPrefs.GetInt("level") - 1]);
        gameManager.SendToCharacterStart();
        SetButtons(false);
    }

    public void CreateSameLevelWithCheckPoint()
    {
        DestroyCurrentLevel();
        currentLevel = Instantiate(levels[PlayerPrefs.GetInt("level")-1]);
        gameManager.SendToCharacterPoint();
        SetButtons(false);
    }
}
