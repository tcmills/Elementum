using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //[SerializeField]
    //private Text _coinText;
    [SerializeField]
    private TMP_Text _livesText;

    // Start is called before the first frame update
    void Start()
    {
        //_coinText.text = "000";
    }

    //public void UpdateCoinDisplay(int collectedCoins)
    //{
    //    if (collectedCoins < 10)
    //    {
    //        _coinText.text = "00" + collectedCoins;
    //    }
    //    else if (collectedCoins < 100)
    //    {
    //        _coinText.text = "0" + collectedCoins;
    //    }
    //    else
    //    {
    //        _coinText.text = "" + collectedCoins;
    //    }
    //}

    public void UpdateLivesDisplay(int lives)
    {
        _livesText.text = "" + lives;
    }
}
