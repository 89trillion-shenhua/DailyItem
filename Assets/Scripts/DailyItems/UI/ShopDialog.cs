using System.Collections.Generic;
using Base;
using UnityEngine;
using UnityEngine.UI;

public class ShopDialog : MonoBehaviour
{
    [SerializeField] private GameObject shopDialog;
    [SerializeField] private DailyItem itemPrefab;
    [SerializeField] private Text countDownText;
    [SerializeField] private Transform itemContainer;
    
    private DailyItemConfig _dailyItemConfig;
    private List<DailyProduct> _dailyProducts = new List<DailyProduct>();
    private List<DailyItem> dailyItems = new List<DailyItem>();
    private int countDownTime;
    private int timeCountDown;

    private const int maxShow = 6;
    
    // 点击开始按钮读取数据并显示Dialog
    public void OnShowClick()
    {
        _dailyItemConfig = new DailyItemConfig();
        _dailyProducts = _dailyItemConfig.DailyProducts;
        countDownTime = _dailyItemConfig.dailyProductCountDown;
        Inititems();
        shopDialog.SetActive(true);
        InitTimeCountDown();
    }
    
    // 点击背景关闭ShopDialog
    public void OnCloseClick()
    {
        int childCount = itemContainer.childCount;
        for (int i = childCount - 1; i >= 0 ; i--)
        {
            Destroy(itemContainer.GetChild(i).gameObject);
        }
        dailyItems.Clear();
        shopDialog.SetActive(false);
    }
    
    // Init所有每日精选商品
    private void Inititems()
    {
        bool islock = false;
        if (_dailyProducts.Count < maxShow)
        {
            for (int i = 0; i < _dailyProducts.Count; i++)
            {
                DailyProduct dailyProduct = _dailyProducts[i];
                DailyItem item = Instantiate(itemPrefab, itemContainer);
                item.Init(islock, dailyProduct);
                dailyItems.Add(item);
            }

            for (int i = _dailyProducts.Count; i < maxShow; i++)
            {
                islock = true;
                DailyItem item = Instantiate(itemPrefab, itemContainer);
                item.Init(islock);
                dailyItems.Add(item);
            }
        }
        if (_dailyProducts.Count >= maxShow)
        {
            for (int i = 0; i < maxShow; i++)
            {
                DailyProduct dailyProduct = _dailyProducts[i];
                DailyItem item = Instantiate(itemPrefab, itemContainer);
                item.Init(islock, dailyProduct);
                dailyItems.Add(item);
            }
        }
    }

    // Init倒计时
    private void InitTimeCountDown()
    {
        timeCountDown = countDownTime;
        string timeStart = TimeUtil.GetTime(timeCountDown);
        countDownText.text = "Refresh time:" + timeStart;
        StartCoroutine(TimeUtil.CountDown(timeCountDown, countDownText, () => RefreshDialog()));
    }

    // 刷新购买状态
    private void RefreshDialog()
    {
        timeCountDown = countDownTime;
        string timeStart = TimeUtil.GetTime(timeCountDown);
        countDownText.text = "Refresh time:" + timeStart;
        foreach (var item in dailyItems)
        {
            item.Refresh();
        }
        StartCoroutine(TimeUtil.CountDown(timeCountDown, countDownText, () => RefreshDialog()));
    }
}