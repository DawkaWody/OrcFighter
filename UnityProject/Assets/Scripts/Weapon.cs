using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private Transform _attackPoint;
    [SerializeField]
    private float _attackRange;
    [SerializeField]
    private LayerMask _enemyLayer;
    
    private float attackRate = 2f;

    private AudioHandler _audioHandler;
    private Animator _animator;
    // Start is called before the first frame update
    void Start(){
        _audioHandler = GetComponent<AudioHandler>();
        _animator = GetComponent<Animator>();

        _audioHandler.soundPlayCooldownTime = attackRate - .05f;
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)){
            Attack();
        }
    }

    public void Attack(){
        _animator.SetTrigger("attack");

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayer);

        if (enemiesHit != null && enemiesHit.Length != 0){
            _audioHandler.PlaySound(0);
            foreach (Collider2D enemy in enemiesHit){
                AudioSource.PlayClipAtPoint(enemy.GetComponent<AudioHandler>()._clips[1], enemy.transform.position, 5);
                Destroy(enemy.gameObject);
            }
        }
        else{
            _audioHandler.PlaySound(1);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
