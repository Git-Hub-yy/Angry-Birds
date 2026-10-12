using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private FollowTarget cameraFollowTarget;
    public static GameManager Instance
    {
        get;private set;
    }
    public GameSO gameSO;
    public Bird[] birdList;
    private int index = -1;
    public int pigTotal;
    public int deadPigNum;
    public GameOverUI gameOverUI;
    private void Awake()
    {
        Instance = this;
        deadPigNum = 0;
        LoadSelectLevel();
    }

    // Start is called before the first frame update
    void Start()
    {
        birdList = FindObjectsByType<Bird>(FindObjectsSortMode.None);
        birdList = birdList.OrderBy(bird => -bird.transform.position.x).ToArray();
        cameraFollowTarget = Camera.main.GetComponent<FollowTarget>();
        LoadNextBird();
        pigTotal = FindObjectsByType<Pig>(FindObjectsSortMode.None).Length;
    }
    private void LoadSelectLevel()
    {
        Time.timeScale = 1;
        int mapID = gameSO.selectedMapID;
        int levelID = gameSO.selectLevelID;
        GameObject levelPrefab =  Resources.Load<GameObject>("Map" + mapID + "/" + "level" + levelID);
        GameObject.Instantiate(levelPrefab);
    }
    public void OnPigDead()
    {
        deadPigNum++;
        if(deadPigNum>=pigTotal)
        {
            GameOver();
        }
    }
    public void LoadNextBird()
    {
        index++;
        if (index >= birdList.Length)
        {
            GameOver();
        }
        else
        {
            birdList[index].GoStage(Slingshot.Instance.getCenterPosition());
            cameraFollowTarget.SetTarget(birdList[index].transform);
        }
    }

    private void GameOver()
    {
        int starCount = 0;
        float pigDeadPercent = deadPigNum * 1f / pigTotal;
        if (pigDeadPercent > 0.99f)
        {
            starCount = 3;
        }
        else if (pigDeadPercent > 0.66f)
        {
            starCount = 2;
        }
        else if (pigDeadPercent > 0.33f)
        {
            starCount = 1;
        }
        gameOverUI.Show(starCount);

        gameSO.UpdateLevel(starCount);
    }
    public void RestartLevel()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // 关卡列表
    public void LevelList()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
