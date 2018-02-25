using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowFixedY : MonoBehaviour {

    public Transform playerPosition;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        // camera is always 3D
        this.transform.position = new Vector3(playerPosition.position.x, transform.position.y, transform.position.z);
	}
}
