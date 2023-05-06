using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private float strength = 6, knockbackDelay = .15f, stunnedDelay = .05f;
    
    private bool takeInput = true;

    private UIController _uiController;
    // Start is called before the first frame update
    void Start(){
        _uiController = GameObject.Find("Canvas").GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update(){
        if (takeInput){
            _rigidbody.velocity = new Vector2(_speed * Input.GetAxis("Horizontal"), _speed * Input.GetAxis("Vertical"));
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Coin")){
            Destroy(other.gameObject);
            GameManager.instance.coins += 1;
        }
    }

    public void knockback(Vector3 senderPos){
        StopAllCoroutines();
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce((transform.position - senderPos).normalized * strength, ForceMode2D.Impulse);
        StartCoroutine(EndKnockbackCo());
    }

    IEnumerator EndKnockbackCo(){
        takeInput = false;
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, .5f);
        yield return new WaitForSeconds(knockbackDelay);
        _rigidbody.velocity = Vector2.zero;
        yield return new WaitForSeconds(stunnedDelay);
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 1f);
        takeInput = true;
    }
}
