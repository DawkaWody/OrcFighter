using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private Rigidbody2D _rigidbody;
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        _rigidbody.velocity = new Vector2(_speed * Input.GetAxis("Horizontal"), _speed * Input.GetAxis("Vertical"));
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Coin")){
            Destroy(other.gameObject);
            GameManager.instance.coins += 1;
        }
    }
}
