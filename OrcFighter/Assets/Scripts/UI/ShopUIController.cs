using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIController : MonoBehaviour
{
    [SerializeField]
    private Text _coinsText;
    // Start is called before the first frame update
    void Start(){
        if (GameManager.instance != null){
            updateCoins();
        }
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void updateCoins(){
        _coinsText.text = "Coins: " + GameManager.instance.coins.ToString();
    }
}
