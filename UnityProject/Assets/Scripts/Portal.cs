using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private float _portalEnterDuration;
    [SerializeField]
    private string _sceneToLoad;
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")){
            _animator.SetTrigger("portalEnter");
            StartCoroutine(PortalEnterCo(other.gameObject));
        }
    }

    IEnumerator PortalEnterCo(GameObject player){
        player.SetActive(false);
        yield return new WaitForSeconds(_portalEnterDuration + 2f);
        SceneManager.LoadScene(_sceneToLoad);
    }
}
