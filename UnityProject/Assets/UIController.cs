using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Image _heart1, _heart2, _heart3;
    [SerializeField]
    private Sprite heartFull, heartHalf, heartEmpty;
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void updateHearts(){
        switch (GameManager.instance.playerHearts){
            case 3:
                _heart1.sprite = heartFull;
                _heart2.sprite = heartFull;
                _heart3.sprite = heartFull;
                break;
            case 2.5f:
                _heart1.sprite = heartHalf;
                _heart2.sprite = heartFull;
                _heart3.sprite = heartFull;
                break;
            case 2:
                _heart1.sprite = heartEmpty;
                _heart2.sprite = heartFull;
                _heart3.sprite = heartFull;
                break;
            case 1.5f:
                _heart1.sprite = heartEmpty;
                _heart2.sprite = heartHalf;
                _heart3.sprite = heartFull;
                break;
            case 1:
                _heart1.sprite = heartEmpty;
                _heart2.sprite = heartEmpty;
                _heart3.sprite = heartFull;
                break;
            case 0.5f:
                _heart1.sprite = heartEmpty;
                _heart2.sprite = heartEmpty;
                _heart3.sprite = heartHalf;
                break;
            case 0:
                _heart1.sprite = heartEmpty;
                _heart2.sprite = heartEmpty;
                _heart3.sprite = heartEmpty;
                break;
            default:
                Debug.Log("Heart value is incorrect");
                break;
        }
    }
}
