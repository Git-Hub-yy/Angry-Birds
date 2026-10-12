using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
    public GameObject lockUI;
    public GameObject starUI;
    public TextMeshProUGUI starCountTextUI;

    private MapLevelUI mapLevelUI;
    private int mapID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Show(int starCount,MapLevelUI mapLevelUI,int mapID)
    {
        this.mapLevelUI = mapLevelUI;
        this.mapID = mapID;
        if (starCount < 0)
        { // 默认-1，锁定状态
            GetComponent<Button>().enabled = false;
            lockUI.SetActive(true);
            lockUI.GetComponent<Image>().sprite = GetComponent<Image>().sprite;
            starUI.SetActive(false);
        }
        else
        { // 解锁状态
            lockUI.SetActive(false);
            starUI.SetActive(true);
            starCountTextUI.text = starCount.ToString();
        }
    }
    public void OnClick()
    { // 绑定到Button组件
        // TODO
        mapLevelUI.OnMapButtonClick(mapID);
    }

}
