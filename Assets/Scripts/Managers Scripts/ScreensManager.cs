﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreensManager : MonoBehaviour
{
    public static ScreensManager instance;

    private GameObject currentScreen;

    public GameObject endScreen;
    public GameObject gameScreen;
    public GameObject mainScreen;
    public GameObject returnScreen;

    public Button lengthButton;
    public Button strengthButton;
    public Button offlineButton;

    public Text gameScreenMoney;
    public Text lengthCostText;
    public Text lengthValueText;
    public Text strengthCostText;
    public Text strengthValueText;
    public Text offlineCostText;
    public Text offlineValueText;
    public Text endScreenMoney;
    public Text returnScreenMoney;

    private int gameCount;

    void Awake()
    {
        if (ScreensManager.instance)
            Destroy(base.gameObject);
        else
            ScreensManager.instance = this;

        currentScreen = mainScreen;
         
    }

    private void Start()
    {
        CheckIdles();
        UpdateTexts();
    }

    public void ChangeScreen(Screens screen )
    {
        currentScreen.SetActive(false);
        switch (screen)
        {
            case Screens.MAIN:
                currentScreen = mainScreen;
                UpdateTexts();
                CheckIdles();
                break;

            case Screens.END:
                currentScreen = endScreen;
                SetEndScreenMoney();
                break;

            case Screens.RETURN:
                currentScreen = returnScreen;
                SetReturnScreenMoney();
                break;

            case Screens.GAME:
                currentScreen = gameScreen;
                gameCount++;
                break;
        }
        currentScreen.SetActive(true);
    }

    public void SetEndScreenMoney()
    {
        endScreenMoney.text = "$" + IdleManager.instance.totalGain;
    }

    public void SetReturnScreenMoney()
    {
        returnScreenMoney.text = "$" + IdleManager.instance.totalGain + " gained while waiting";
    }

    public void CheckIdles()
    {
        int lengthCost = IdleManager.instance.lengthCost;
        int strengthCost = IdleManager.instance.strengthCost;
        int offlineEarningCost = IdleManager.instance.offlineEarningsCost;
        int wallet = IdleManager.instance.wallet;

        if (wallet < lengthCost)
            lengthButton.interactable = false;
        else
            lengthButton.interactable = true;

        if (wallet < strengthCost)
            strengthButton.interactable = false;
        else
            strengthButton.interactable = true;

        if (wallet < offlineEarningCost)
            offlineButton.interactable = false;
        else
            offlineButton.interactable = true;
    }

        public void UpdateTexts()
        {
            gameScreenMoney.text = "$" + IdleManager.instance.wallet;
            lengthCostText.text = "$" + IdleManager.instance.lengthCost;
            lengthValueText.text = "" + -IdleManager.instance.length + "m";
            strengthCostText.text = "$" + IdleManager.instance.strengthCost;
            strengthValueText.text = "" + IdleManager.instance.strength + " fishes.";
            offlineCostText.text = "$" + IdleManager.instance.offlineEarningsCost;
            offlineValueText.text = "$" + IdleManager.instance.offlineEarnings + "/min";
        }
    }
