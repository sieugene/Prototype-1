using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ViewMode
{
    Behind,
    Forward
}

public class FollowPlayer : MonoBehaviour
{
    public ViewMode viewMode = ViewMode.Behind;
    public GameObject player;
    private Vector3 offset;
    private float defaultXRotation;
    // Start is called before the first frame update
    void Start()
    {
        if (offset == Vector3.zero)
        {
            // Take current position of camera as offset
            offset = transform.position - player.transform.position;
        }

        // Initialize defaultXRotation based on the current rotation of the camera
        defaultXRotation = transform.rotation.eulerAngles.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            viewMode = viewMode == ViewMode.Forward ? ViewMode.Behind : ViewMode.Forward;
        }
        if (viewMode == ViewMode.Forward)
        {
            Vector3 forwardOffset = new Vector3(0, 2.3f, 0.60f);
            transform.position = player.transform.position + player.transform.rotation * forwardOffset;
            transform.rotation = Quaternion.Lerp(transform.rotation, player.transform.rotation, Time.deltaTime * 5);
        }
        else
        {
            // Offset the camera behind the player by adding to the player's position
            transform.position = player.transform.position + offset;
            transform.LookAt(player.transform);

            // Apply the default X rotation
            Vector3 eulerRotation = transform.rotation.eulerAngles;
            eulerRotation.x = defaultXRotation;
            transform.rotation = Quaternion.Euler(eulerRotation);
        }

    }
}
