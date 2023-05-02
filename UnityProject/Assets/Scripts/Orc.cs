using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private GameObject _player;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private bool _isAttacking;
    // Start is called before the first frame update
    void Start(){
        _player = GameObject.Find("Player");
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update(){
        Vector2 moveDirection = CalculateMovement();
        AdaptImage(moveDirection);
        transform.Translate(moveDirection * _speed * Time.deltaTime);
        if (checkIfAttack()){
            attack();
        }
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Orc_walk")){
            _isAttacking = false;
        }
    }

    Vector2 CalculateMovement(){
        Vector2 toMove = _player.transform.position - transform.position;
        Debug.Log(toMove);
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
            _animator.SetBool("turnedRight", false);
        }
        else if (direction.x > 0){
            _spriteRenderer.flipX = false;
            _animator.SetBool("turnedRight", true);
        }
    }

    bool checkIfAttack(){
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 playerPos = new Vector2(_player.transform.position.x, _player.transform.position.y);
        Vector2 distance = playerPos - currentPos;
        float distanceX = distance.x;
        float distanceY = distance.y;
        float generalDistance = distanceX + distanceY;
        if (generalDistance <= 2 && generalDistance >= 0){
            return true;
        }
        else if (generalDistance >= -2 && generalDistance <= 0){
            return true;
        }
        return false;
    }

    void attack(){
        _isAttacking = true;
        _animator.SetTrigger("attacks");
    }
}
