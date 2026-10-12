using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    public static LevelSelectManager Instance {  get; private set; }
    public GameSO gameSO;
    public MapLevelUI mapLevelUI;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        mapLevelUI.ShowMapList(gameSO.mapArray);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetSelectedMap(int mapID)
    {
        gameSO.selectedMapID= mapID; 

    }
    public void SetSelectedLevel(int levelID)
    {
        gameSO.selectLevelID = levelID;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public int[] GetSelectedMap()
    {
        return gameSO.mapArray[gameSO.selectedMapID - 1].starNumberOfLevel;
    }
}
