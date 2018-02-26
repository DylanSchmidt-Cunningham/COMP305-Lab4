using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// inherit from CameraFollowWithBuffer
public class ZoomScript : CameraFollowWithBuffer {

    public Transform zoomStartPos;
    public Transform zoomEndPos;
    public float scaleStart;
    public float scaleEnd;

    protected float scaleProgress; // progress from start to end, in 0-1
    protected float originalScale;
    protected float adjustedY;
    protected float originalY;
    
    // Use this for initialization
    protected new void Start () {
        base.Start();
        originalScale = GetComponent<Camera>().orthographicSize;
        originalY = this.transform.position.y;
        adjustedY = originalY;
        scaleProgress = 0;
    }

    // follow the player in the x-axis if they move outside the buffer zone, and in the Y-axis inasmuch as the camera is zoomed in on them
    protected new Vector3 followPlayer() {
        Vector3 newPos = base.followPlayer(); // x-axis
        // update the y-axis
        adjustedY = Mathf.Lerp(originalY, playerPosition.position.y, scaleProgress);
        // adjust the y-axis to its most recently updated value
        newPos.y = adjustedY;

        return newPos;
    }

    // if the player is in the zoom area, calculate how far they've progressed from the start to the end
    private void updateScaleProgress()
    {
        if (this.transform.position.x > zoomStartPos.position.x && this.transform.position.x < zoomEndPos.position.x)
        {
            scaleProgress = Mathf.InverseLerp(zoomStartPos.position.x, zoomEndPos.position.x, transform.position.x);
        }
    }

    // calculate the size of the zoomed camera
    private float calculateCameraSize()
    {
        return originalScale * Mathf.Lerp(scaleStart, scaleEnd, scaleProgress);
    }

    // Update is called once per frame
    protected new void Update()
    {
        updateScaleProgress();
        this.transform.position = followPlayer();
        GetComponent<Camera>().orthographicSize = calculateCameraSize();
    }
    
    // Draw all Gizmos
    protected new void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        drawThresholdLine(zoomStartPos);
        drawThresholdLine(zoomEndPos);
    }
}
