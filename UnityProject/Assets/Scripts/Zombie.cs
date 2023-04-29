using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private float _speed;
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        Vector2 toMove = player.transform.position - transform.position;
        _rigidbody.velocity = new Vector2(Mathf.Clamp(_speed, 0, toMove.x), Mathf.Clamp(_speed, 0, toMove.y));
    }
}
