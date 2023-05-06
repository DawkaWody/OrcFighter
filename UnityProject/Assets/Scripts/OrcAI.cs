using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class OrcAI : MonoBehaviour
{
    private Transform target;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _nextWayPointDistance;

    private Path _path;
    private int _currentWaypoint = 0;
    private bool directionCheckingStarted;

    private Vector2 direction;

    private bool _isAttacking;

    private Seeker _seeker;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private Player _player;
    private UIController _uiController;
    // Start is called before the first frame update
    void Start(){
        target = GameObject.Find("Player").GetComponent<Transform>();

        _seeker = GetComponent<Seeker>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        _player = GameObject.Find("Player").GetComponent<Player>();
        _uiController = GameObject.Find("Canvas").GetComponent<UIController>();

        InvokeRepeating("UpdatePath", 0f, .5f);   
    }

    private void UpdatePath(){
        if (_seeker.IsDone()){
            _seeker.StartPath(_rigidbody.position, target.position, OnPathCreated);
        }
    }

    // Update is called once per frame
    void FixedUpdate(){
        if (_path != null){
            if (!directionCheckingStarted){
                StartCoroutine(AdjustImageCo());
                directionCheckingStarted = true;
            }
            if (_currentWaypoint >= _path.vectorPath.Count){
                return;
            }
            direction = AdjustVector((Vector2)_path.vectorPath[_currentWaypoint] - _rigidbody.position);
            Vector2 force = direction * _speed * Time.deltaTime;
            transform.Translate(force);

            float distance = Vector2.Distance(_rigidbody.position, _path.vectorPath[_currentWaypoint]);

            if (distance < _nextWayPointDistance && _currentWaypoint + 1 < _path.vectorPath.Count){
                _currentWaypoint ++;
            }

            if (checkIfAttack()){
                _isAttacking = checkIfAttack();
                attack();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (_isAttacking && other.CompareTag("Player")){
            GameManager.instance.DamagePlayer(.5f);
            _player.knockback(transform.position);
            _uiController.updateHearts();
        }
    }

    private void OnPathCreated(Path path){
        if (!path.error){
            _path = path;
            _currentWaypoint = 0;
        }
    }

    Vector2 AdjustVector(Vector2 toMove){
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

    IEnumerator AdjustImageCo(){
        while (true){
            // Sprawdzenie kierunku
            if (direction.x > 0){
                _spriteRenderer.flipX = false;
                _animator.SetBool("turnedRight", true);
            }
            else if (direction.x < 0){
                _spriteRenderer.flipX = true;
                _animator.SetBool("turnedRight", false);
            }
            
            yield return new WaitForSeconds(.5f);
        }
    }

    bool checkIfAttack(){
        if (_currentWaypoint == _path.vectorPath.Count - 1 && Vector3.Distance(transform.position, target.transform.position) < 2){
            return true;
        }
        return false;
    }

    void attack(){
        _isAttacking = true;
        _animator.SetTrigger("attacks");
    }
}
