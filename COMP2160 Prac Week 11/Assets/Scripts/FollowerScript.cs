using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerScript : MonoBehaviour
{
    [SerializeField] private Transform followerLoc;
    [SerializeField] private Transform marbleLoc;
    [SerializeField] private GameObject UI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        followerLoc.position = marbleLoc.position;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(followerLoc.position, 0.5f);
    }
}
