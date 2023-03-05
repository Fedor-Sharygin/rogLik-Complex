using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        OrientGun();
    }

    [SerializeField] private float speed;
    private Vector2 velDir;
    private void Move()
    {
        velDir.x = Input.GetAxis("hor");
        velDir.y = Input.GetAxis("ver");

        rigidbody.velocity = velDir.normalized * speed;
    }

    [SerializeField] private Transform aim;
    private void OrientGun()
    {
        Vector2 mouseToPlayerDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        float curAngle = Mathf.Atan2(mouseToPlayerDir.y, mouseToPlayerDir.x) * 180.0f / Mathf.PI;
        aim.eulerAngles = new Vector3(0, 0, curAngle);
    }
}
