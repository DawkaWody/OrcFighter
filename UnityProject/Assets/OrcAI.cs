using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class OrcAI : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _nextWayPointDistance;

    private SpriteRenderer _spriteRenderer;

    private Path _path;
    private int _currentWaypoint = 0;
    private bool _reachedEndOfPath;

    private Seeker _seeker;
    private Rigidbody2D _rigidbody;
    // Start is called before the first frame update
    void Start(){
        _seeker = GetComponent<Seeker>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

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
            if (_currentWaypoint >= _path.vectorPath.Count){
                _reachedEndOfPath = true;
                return;
            }
            else{
                _reachedEndOfPath = false;
            }
            Vector2 direction = ((Vector2)_path.vectorPath[_currentWaypoint] - _rigidbody.position).normalized;
            Vector2 force = direction * _speed * Time.deltaTime;
            _rigidbody.AddForce(force);

            float distance = Vector2.Distance(_rigidbody.position, _path.vectorPath[_currentWaypoint]);

            if (distance < _nextWayPointDistance){
                _currentWaypoint ++;
            }

            // Sprawdzenie kierunku
            if (force.x >= 0.01f){
                _spriteRenderer.flipX = false;
            }
            else if (force.x <= -0.01f){
                _spriteRenderer.flipX = true;
            }
        }
    }

    private void OnPathCreated(Path path){
        if (!path.error){
            _path = path;
            _currentWaypoint = 0;
        }
    }
}
