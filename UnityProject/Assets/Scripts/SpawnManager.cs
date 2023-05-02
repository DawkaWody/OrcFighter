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
    private GameObject _enemyContainer;

    [SerializeField]
    private float _spawnRate;

    private float maxX, maxY, minX, minY;
    private bool spawn = true;
    
    public int orcLimit;
    private int orcCount;

    // Start is called before the first frame update
    void Start(){
        maxX = rightPoint.transform.position.x;
        minX = leftPoint.transform.position.x;
        maxY = topPoint.transform.position.y;
        minY = bottomPoint.transform.position.y;
        StartCoroutine(OrcSpawnCo());
    }

    // Update is called once per frame
    void Update(){
        if (orcCount >= orcLimit){
            spawn = false;
            Debug.Log("Stopped Spawning");
        }
    }

    IEnumerator OrcSpawnCo(){
        while (spawn){
            yield return new WaitForSeconds(_spawnRate);
            if (!spawn){
                yield break;
            }
            int wave = GameManager.instance.wave;
            GameObject newOrc = Instantiate(_orcPrefab, transform.position + new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0), transform.rotation);
            Debug.Log("New orc spawned");
            newOrc.transform.parent = _enemyContainer.transform;

            orcCount += 1;
        }
    }
}
