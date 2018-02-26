using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraTrigger : MonoBehaviour
{

    public Camera insideAreaCamera;
    public Camera outsideAreaCamera;

    public Transform sharedAudioListener;

    // Defined by unity
    void OnTriggerEnter2D(Collider2D other)
    {
        // If the other object is a player, then switch camera
        if (other.gameObject.tag == "Player")
        {
            
            outsideAreaCamera.enabled = false;
            insideAreaCamera.enabled = true;
            handOffAudioListener(insideAreaCamera);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            insideAreaCamera.enabled = false;
            outsideAreaCamera.enabled = true;
            handOffAudioListener(outsideAreaCamera);
        }
    }

    // pass the one and only AudioListener to the new active camera
    private void handOffAudioListener(Camera newParent)
    {
        sharedAudioListener.parent = newParent.transform;
        sharedAudioListener.localPosition = Vector3.zero;
        sharedAudioListener.localRotation = Quaternion.identity;
    }
}
