using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private float damageCooldown = .5f;

    public int coins;
    public float playerHearts;
    public int wave;
    public int zombiesKilled;

    private bool canDamage = true;

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
        playerHearts = 3;
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void DamagePlayer(float strength){
        if (canDamage){
            playerHearts -= strength;
            canDamage = false;
            StartCoroutine(DamageCooldownCo());
        }
    }

    IEnumerator DamageCooldownCo(){
        yield return new WaitForSeconds(damageCooldown);
        canDamage = true;
    }
}
