using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;



public class GameManager : MonoBehaviour
{
    [SerializeField] Image[] steps;

    private Player player;
    [SerializeField] public GameObject playButton;

    int platformCount;
    public int passedPlatform;
    public bool isLevelFinished;

    public GameObject[] checkPoints;

    GameObject[] gateways;

    [Header("Controller")]
    private LevelController levelController;
    void Awake()
    {

        StartingEvents();
        passedPlatform = 0;
        player = GameObject.FindObjectOfType<Player>();
        levelController = GameObject.FindObjectOfType<LevelController>();
    }
    public void StartingEvents()
    {
        Array.Clear(checkPoints, 0, checkPoints.Length);
        checkPoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        isLevelFinished = false;
        playButton.SetActive(true);
        GetLevelSteps();
    }
    public void TapToPlay()
    {
        platformCount = GameObject.FindGameObjectsWithTag("Platform").Length;
        if (checkPoints != null)
        {
            checkPoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        }
        gateways = null;
        gateways = GameObject.FindGameObjectsWithTag("Gateway");
        player.isMoving = true;
        playButton.SetActive(false);
    }


    public void GetLevelSteps()
    {
        switch (passedPlatform)
        {
            case 0:
                for (int i = 0; i < platformCount; i++)
                {
                    steps[i].color = Color.white;
                }
                break;
            case 1:
                steps[0].color = Color.yellow;
                break;
            case 2:
                steps[1].color = Color.yellow;
                break;
            case 3:
                steps[2].color = Color.yellow;
                break;
        }
    }

    public void SendToCharacterStart()
    {
        passedPlatform = 0;
        StartingEvents();
        player.BackToTheCheckPoint(checkPoints[passedPlatform].transform);
    }
    public void SendToCharacterPoint()
    {    
        StartingEvents();
        
        player.BackToTheCheckPoint(checkPoints[passedPlatform].transform);
    }

    public IEnumerator Finish()
    {
        isLevelFinished = false;
        passedPlatform = 0;
        yield return new WaitForSeconds(1f);
        player.transform.DOMove(levelController.currentLevel.transform.position, Time.deltaTime * 100f);
        foreach (var gecit in gateways)
        {
            gecit.GetComponent<Animator>().enabled = true;
        }
        yield return new WaitForSeconds(1.5f);
        levelController.DestroyLastLevel();
        player.ResetSize();
        StartingEvents();
    }
}