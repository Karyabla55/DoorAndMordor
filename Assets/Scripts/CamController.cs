using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public GameObject Player;
    public Transform pTransform;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float targetYFix;

    Rigidbody2D rb;
    Vector3 desiredPosition;

    private void Start()
    {
        rb = Player.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            float targetX =pTransform.position.x;
            if (rb.velocity.x > 0f)
            {
                targetX = pTransform.position.x + offset.y+1 ;
            }
            else if (rb.velocity.x < 0f)
            {
                targetX = pTransform.position.x + offset.y - 2;
            }

            float targetY = (float)(targetYFix);
            desiredPosition = new Vector3(targetX, targetY, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
