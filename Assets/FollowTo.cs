using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTo : MonoBehaviour
{
    [SerializeField] Transform playerTransform;

    private void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (playerTransform != null){
            transform.position = playerTransform.position;
        }else{
            Debug.LogWarning("Null reference to Plater Transform");
        }
    }
}
