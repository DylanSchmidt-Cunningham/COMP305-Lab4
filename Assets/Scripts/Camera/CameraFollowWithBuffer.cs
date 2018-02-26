using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowWithBuffer : MonoBehaviour {

    public Transform playerPosition;
    public Transform playerRightThreshold;
    public Transform playerLeftThreshold;

    private float rightOffset;
    private float leftOffset;

    // Use this for initialization
    void Start () {
        playerRightThreshold = transform.GetChild(0);
        playerLeftThreshold = transform.GetChild(1);

        // calculate offsets of the forward and backward thresholds
        rightOffset = playerRightThreshold.position.x - this.transform.position.x; // positive if right threshold is > camera position
        leftOffset = this.transform.position.x - playerLeftThreshold.position.x; // positive if left threshold is < camera position
	}
	
	// Update is called once per frame
	void Update () {
		if(playerPosition.position.x > playerRightThreshold.position.x)
        {
            this.transform.position = new Vector3(playerPosition.position.x - rightOffset, this.transform.position.y, this.transform.position.z);
        }
        else if (playerPosition.position.x < playerLeftThreshold.position.x)
        {
            this.transform.position = new Vector3(playerPosition.position.x + leftOffset, this.transform.position.y, this.transform.position.z);
        }
	}

    // Draw all Gizmos
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        drawThresholdLine(playerRightThreshold);
        drawThresholdLine(playerLeftThreshold);
    }

    // Draw a vertical line at a threshold
    // TODO support horizontal thresholds for future camera that can pan up or down
    private void drawThresholdLine(Transform threshold)
    {
        Gizmos.DrawLine(threshold.position, new Vector3(threshold.position.x, threshold.position.y + 100, threshold.position.z));
        Gizmos.DrawLine(threshold.position, new Vector3(threshold.position.x, threshold.position.y - 100, threshold.position.z));
    }
}
