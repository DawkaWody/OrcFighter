using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _orcPrefab;
    [SerializeField]
    private GameObject topPoint, bottomPoint, leftPoint, rightPoint;
    [SerializeField]
    private float _spawnRate;

    private float maxX, maxY, minX, minY;
    private bool spawn = true;

    // Start is called before the first frame update
    void Start(){
        maxX = rightPoint.transform.position.x;
        minX = leftPoint.transform.position.x;
        maxY = topPoint.transform.position.y;
        minY = bottomPoint.transform.position.y;
        StartCoroutine(ZombieSpawnCo());
    }

    // Update is called once per frame
    void Update(){
        
    }

    IEnumerator ZombieSpawnCo(){
        while (spawn){
            yield return new WaitForSeconds(_spawnRate);
            int wave = GameManager.instance.wave;
            Instantiate(_orcPrefab, transform.position + new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0), transform.rotation);
        }
    }
}
