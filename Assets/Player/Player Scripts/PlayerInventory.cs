using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int m_coinCount = 0;
    [SerializeField] private Text coinCountUI;

    private int CoinCount
    {
        get
        {
            return m_coinCount;
        }
        set
        {
            m_coinCount = value;
            coinCountUI.text = value.ToString();
        }
    }

    private void OnEnable()
    {
        CoinCount = m_coinCount;
        EventManager.OnCoinCollected += PickUpCoin;
    }

    private void OnDisable()
    {
        EventManager.OnCoinCollected -= PickUpCoin;
    }

    private void PickUpCoin()
    {
        CoinCount++;
    }
}
