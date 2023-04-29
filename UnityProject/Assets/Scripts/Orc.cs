using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private GameObject _player;
    // Start is called before the first frame update
    void Start(){
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update(){
        transform.Translate(CalculateMovement() * _speed * Time.deltaTime);
    }

    Vector2 CalculateMovement(){
        Vector2 toMove = _player.transform.position - transform.position;
        if (toMove.x < 0){
            toMove.x = -1;
        }
        else if (toMove.x > 0){
            toMove.x = 1;
        }
        if (toMove.y < 0){
            toMove.y = -1;
        }
        else if (toMove.y > 0){
            toMove.y = 1;
        }

        return toMove;
    }
}
