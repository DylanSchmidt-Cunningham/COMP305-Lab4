using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowWithBuffer : MonoBehaviour {

    public Transform playerPosition;
    public Transform playerRightThreshold;
    public Transform playerLeftThreshold;

    protected float rightOffset;
    protected float leftOffset;

    // Use this for initialization
    protected void Start () {
        //playerRightThreshold = transform.GetChild(0);
        //playerLeftThreshold = transform.GetChild(1);

        // calculate offsets of the forward and backward thresholds
        rightOffset = playerRightThreshold.position.x - this.transform.position.x; // positive if right threshold is > camera position
        leftOffset = this.transform.position.x - playerLeftThreshold.position.x; // positive if left threshold is < camera position
	}
	
	// I refactored the logic into its own function so inheriting classes don't have to re-make the Vector3 to adjust y.
    // follow the player in the x-axis if they move outside the buffer zone
	protected Vector3 followPlayer() {
		if(playerPosition.position.x > playerRightThreshold.position.x) // move right
        {
            return new Vector3(playerPosition.position.x - rightOffset, this.transform.position.y, this.transform.position.z);
        }
        else if (playerPosition.position.x < playerLeftThreshold.position.x) // move left
        {
            return new Vector3(playerPosition.position.x + leftOffset, this.transform.position.y, this.transform.position.z);
        }
        else // player remained in buffer zone
        {
            return this.transform.position;
        }
	}

    // Update is called once per frame
    protected void Update()
    {
        this.transform.position = followPlayer();
    }

    // Draw all Gizmos
    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        drawThresholdLine(playerRightThreshold);
        drawThresholdLine(playerLeftThreshold);
    }

    // Draw a vertical line at a threshold
    // TODO support horizontal thresholds for future camera that can pan up or down
    protected void drawThresholdLine(Transform threshold)
    {
        Gizmos.DrawLine(threshold.position, new Vector3(threshold.position.x, threshold.position.y + 100, threshold.position.z));
        Gizmos.DrawLine(threshold.position, new Vector3(threshold.position.x, threshold.position.y - 100, threshold.position.z));
    }
}
