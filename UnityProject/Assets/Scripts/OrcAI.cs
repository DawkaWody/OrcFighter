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
    [SerializeField]
    private Transform _attackPoint;
    [SerializeField]
    private float _attackRange;
    [SerializeField]
    private LayerMask _playerLayer;

    private Path _path;
    private int _currentWaypoint = 0;

    private Vector2 direction;

    private Seeker _seeker;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private AudioHandler _audioHandler;

    private Player _player;
    private UIController _uiController;
    // Start is called before the first frame update
    void Start(){
        target = GameObject.Find("Player").GetComponent<Transform>();

        _seeker = GetComponent<Seeker>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _audioHandler = GetComponent<AudioHandler>();

        _player = GameObject.Find("Player").GetComponent<Player>();
        _uiController = GameObject.Find("Canvas").GetComponent<UIController>();

        InvokeRepeating("UpdatePath", 0f, .5f);
        InvokeRepeating("AdjustImage", 0f, .5f);
    }

    private void UpdatePath(){
        if (_seeker.IsDone()){
            _seeker.StartPath(_rigidbody.position, target.position, OnPathCreated);
        }
    }

    // Update is called once per frame
    void FixedUpdate(){
        if (_path != null){
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
                attack();
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
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

    void AdjustImage(){
        // Sprawdzenie kierunku
        if (direction.x > 0){
            _spriteRenderer.flipX = false;
            _animator.SetBool("turnedRight", true);
        }
        else if (direction.x < 0){
            _spriteRenderer.flipX = true;
            _animator.SetBool("turnedRight", false);
        }
    }

    bool checkIfAttack(){
        if (_currentWaypoint == _path.vectorPath.Count - 1 && Vector3.Distance(transform.position, target.transform.position) < 2){
            return true;
        }
        return false;
    }

    void attack(){
        _animator.SetTrigger("attacks");
        bool playerWasHit = false;
        Collider2D[] playerHit = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _playerLayer);
        if (playerHit != null && playerHit.Length != 0){
            if (!playerWasHit){
                _audioHandler.PlaySound(0);
                foreach (Collider2D player in playerHit){
                    GameManager.instance.DamagePlayer(.5f);
                    _uiController.updateHearts();
                    player.GetComponent<Player>().knockback(transform.position);
                }
                playerWasHit = true;
            }
            else{
                _audioHandler.PlaySound(1);
                playerWasHit = false;
            }
        }
    }
}
