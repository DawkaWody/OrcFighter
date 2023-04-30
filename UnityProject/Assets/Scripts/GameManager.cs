using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public int coins;
    public int wave;
    public int zombiesKilled;

    private void Awake() {
        if (instance != null){
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start(){
        wave = 1;
    }

    // Update is called once per frame
    void Update(){
        
    }
}
