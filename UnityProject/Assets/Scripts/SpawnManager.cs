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

    [SerializeField]
    private float _timer;

    private float maxX, maxY, minX, minY;
    private bool spawn;
    
    public int orcLimit;
    private int orcCount;

    // Start is called before the first frame update
    void Start(){
        maxX = rightPoint.transform.position.x;
        minX = leftPoint.transform.position.x;
        maxY = topPoint.transform.position.y;
        minY = bottomPoint.transform.position.y;
        spawn = false;
        _timer += Time.deltaTime;
        StartCoroutine(OrcSpawnCo());
    }

    // Update is called once per frame
    void Update() {
        if (orcCount >= orcLimit) {
            spawn = false;
        }
        if (_timer >= 3)
        {
            spawn = true;
        }

        if (_spawnRate > 1)
        {
            if (_timer > 12)
            {
                _spawnRate -= 1;
            }
        }

        if (orcCount == 0)
        {
        if (_timer == 30){
            spawn = true;
        }

        if (_timer > 30){
            _timer = 0;
        }

        if (orcCount == 0){
            _timer += Time.deltaTime;
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
            newOrc.transform.parent = _enemyContainer.transform;

            orcCount += 1;
        }
    }

