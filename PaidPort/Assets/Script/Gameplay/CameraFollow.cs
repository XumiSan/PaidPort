using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float offset;

    [SerializeField]
    private Vector2 dynamicOffset;

    [SerializeField]
    private float smoothSpeed;

    [SerializeField]
    private float minX, maxX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Memanggil fungsi FollowTarget
        FollowTarget();
    }
    private void FollowTarget()
    {
        //player follow basic
        /*
        Vector2 position = new Vector2(target.position.x, target.position.y + offset);
        transform.position = position;
        */

        //dynamic + smooth follow
        Vector2 vector2TargetPos = new Vector2(target.position.x, target.position.y);
        Vector2 desiredPos = vector2TargetPos + dynamicOffset;

        float clampedX = Mathf.Clamp(desiredPos.x, minX, maxX);
        desiredPos = new Vector2(clampedX, desiredPos.y);

        Vector2 smoothPosition = Vector2.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.position = smoothPosition;
    }
}
