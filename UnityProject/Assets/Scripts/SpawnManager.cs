using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _orcPrefab;
    [SerializeField]
    private float _spawnRate;

    private bool spawn = true;
    // Start is called before the first frame update
    void Start(){
        StartCoroutine(ZombieSpawnCo());
    }

    // Update is called once per frame
    void Update(){
        
    }

    IEnumerator ZombieSpawnCo(){
        while (spawn){
            yield return new WaitForSeconds(_spawnRate);
            Instantiate(_orcPrefab, transform.position + new Vector3(Random.Range(32, -32), Random.Range(27, -26), 0), transform.rotation);
        }
    }
}
