using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private GameObject _player;
    private SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start(){
        _player = GameObject.Find("Player");
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update(){
        Vector2 moveDirection = CalculateMovement();
        AdaptImage(moveDirection);
        transform.Translate(moveDirection * _speed * Time.deltaTime);
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

    void AdaptImage(Vector2 direction){
        if (direction.x < 0 || _player.transform.position.x - transform.position.x < .2f){
            _spriteRenderer.flipX = true;
        }
        else if (direction.x > 0){
            _spriteRenderer.flipX = false;
        }
    }
}
