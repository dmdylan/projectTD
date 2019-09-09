using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Waypoint> path;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        print("Starting patrol....");
        foreach (Waypoint waypoint in path)
        {
            Vector3 position = new Vector3 (waypoint.transform.position.x, waypoint.transform.position.y + 10f, waypoint.transform.position.z);
            transform.position = position;
            print("visiting " + waypoint);
            yield return new WaitForSeconds(1f);
        }
        print("ending patrol...");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
