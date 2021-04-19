using System.Collections.Generic;
using Base;
using UnityEngine;
using UnityEngine.UI;

public class DailyItem : MonoBehaviour
{
    [SerializeField] private List<Sprite> itemImgs;
    [SerializeField] private GameObject coinBuy;
    [SerializeField] private GameObject gemBuy;
    [SerializeField] private GameObject rareText;
    [SerializeField] private GameObject lockImg;
    [SerializeField] private GameObject canNotBuy;
    [SerializeField] private GameObject confirmBtn;
    [SerializeField] private Text coinBuyCost;
    [SerializeField] private Text gemBuyCost;
    [SerializeField] private Text title;
    [SerializeField] private Text isFree;
    [SerializeField] private Sprite freeBg;
    [SerializeField] private Sprite cardBg;
    [SerializeField] private Image bgImg;
    [SerializeField] private Image contentBg;
    [SerializeField] private Image cardImg;

    // Init每个商品；
    public void Init(bool islock, DailyProduct dailyProduct = default(DailyProduct))
    {
        if (islock)
        {
            lockImg.SetActive(true);
        }
        
        ShowCost(dailyProduct.costGold, dailyProduct.costGem);
        ShowTitleAndImg((RewardType)dailyProduct.type, dailyProduct.subType);
    }

    // 显示每个商品的价格；
    private void ShowCost(int costGold, int costGem)
    {
        if (costGold <= 0 && costGem <= 0)
        {
            isFree.text = "FREE!";
            bgImg.sprite = freeBg;
        }

        else if (costGold <= 0 && costGem > 0)
        {
            gemBuyCost.text = costGem.ToString();
            gemBuy.SetActive(true);
        }

        else if (costGold > 0 && costGem <= 0)
        {
            coinBuyCost.text = costGold.ToString();
            coinBuy.SetActive(true);
        }
    }

    // 获取商品的名字和图片；
    private void ShowTitleAndImg(RewardType type, int subType)
    {
        switch (type)
        {
            case RewardType.None:
                break;
            case RewardType.Trophy:
                break;
            case RewardType.Coins:
                title.text = "Coins";
                cardImg.sprite = itemImgs[0];
                contentBg.sprite = cardBg;
                break;
            case RewardType.Diamonds:
                title.text = "Gems";
                cardImg.sprite = itemImgs[1];
                contentBg.sprite = cardBg;
                break;
            case RewardType.Cards:
                title.text = "Cards";
                rareText.SetActive(true);
                switch (subType)
                {
                    case 7:
                        cardImg.sprite = itemImgs[2];
                        break;
                    case 13:
                        cardImg.sprite = itemImgs[3];
                        break;
                    case 18:
                        cardImg.sprite = itemImgs[4];
                        break;
                    case 20:
                        cardImg.sprite = itemImgs[5];
                        break;
                }
                break;
            case RewardType.Chest:
                break;
            case RewardType.BattlePassExp:
                break;
            case RewardType.MasterLV:
                break;
            case RewardType.MasterEXP:
                break;
        }
    }
    
    // 点击购买时触发的事件；
    public void OnBuyClick()
    {
        confirmBtn.SetActive(false);
        canNotBuy.SetActive(true);
    }

    // 刷新商品；
    public void Refresh()
    {
        canNotBuy.SetActive(false);
        confirmBtn.SetActive(true);
    }
}