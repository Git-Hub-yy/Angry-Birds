using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLevelUI : MonoBehaviour
{
    public GameObject mapListGo; // 地图
    public GameObject levelListGo; // 关卡

    public List<MapUI> mapUIList; // 地图UI集合
    public GameObject levelTempLatePrefab;
    public GameObject levelGridGo;
    // 先选择地图，点击地图后再选择关卡
    public void ShowMapList(MapSO[] mapArray)
    {
        mapListGo.SetActive(true); // 地图可选
        levelListGo.SetActive(false); // 关卡不可选
        // 显示地图列表
        for (int i = 0; i < mapArray.Length; i++)
        {
            mapUIList[i].Show(mapArray[i].starNumberOfMap,this,i+1);
        }
    }
    public void OnMapButtonClick(int mapID)
    {
        LevelSelectManager.Instance.SetSelectedMap(mapID);
        print(mapID);
        ShowLevelGrid();
    }
    private void ShowLevelGrid()
    {
        mapListGo.SetActive(false); // 地图可选
        levelListGo.SetActive(true); // 关卡不可选
        int[] starNumberOfLevel = LevelSelectManager.Instance.GetSelectedMap();


        foreach(Transform child in levelGridGo.transform)
        {
            Destroy(child.gameObject);
        }

        for(int i=0;i<starNumberOfLevel.Length;i++)
        {
            GameObject go =  GameObject.Instantiate(levelTempLatePrefab);
            go.GetComponent<RectTransform>().SetParent(levelGridGo.transform);

            go.GetComponent<LevelUI>().Show(starNumberOfLevel[i],i+1,this);
        }
    }
    public void OnLevelButtonClick(int levelID)
    {
        LevelSelectManager.Instance.SetSelectedLevel(levelID);
    }
    public void OnReturnButtonClick()
    {
        mapListGo.SetActive(true); // 地图可选
        levelListGo.SetActive(false); // 关卡不可选
    }
}
