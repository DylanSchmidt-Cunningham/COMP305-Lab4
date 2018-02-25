using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowWithBuffer : MonoBehaviour {

    public Transform playerPosition;
    public Transform playerForwardThreshold;
    public Transform playerBackwardThreshold;

    // Use this for initialization
    void Start () {
        playerForwardThreshold = transform.GetChild(0);
        playerBackwardThreshold = transform.GetChild(1);
	}
	
	// Update is called once per frame
	void Update () {
		if(playerPosition.position.x > playerForwardThreshold.position.x || playerPosition.position.x < playerBackwardThreshold.position.x)
        {
            this.transform.position = new Vector3(playerPosition.position.x, this.transform.position.y, this.transform.position.z);
        }
	}

    // Draw all Gizmos
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        drawThresholdLine(playerForwardThreshold);
        drawThresholdLine(playerBackwardThreshold);
    }

    // Draw a vertical line at a threshold
    // TODO support horizontal thresholds for future camera that can pan up or down
    private void drawThresholdLine(Transform threshold)
    {
        Gizmos.DrawLine(threshold.position, new Vector3(threshold.position.x, threshold.position.y + 100, threshold.position.z));
        Gizmos.DrawLine(threshold.position, new Vector3(threshold.position.x, threshold.position.y - 100, threshold.position.z));
    }
}
