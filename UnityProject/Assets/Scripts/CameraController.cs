using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerToFollow;
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        transform.position = new Vector3(_playerToFollow.transform.position.x, _playerToFollow.transform.position.y, transform.position.z);
    }
}
