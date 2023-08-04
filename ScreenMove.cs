using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenMove : MonoBehaviour
{
    public Vector2 initPos;
    public Vector2 curPos;
    float speed;
    public bool onMove;
    public float offsetSpeed;
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                onMove = true;
                initPos = Input.mousePosition;
            }
        }
        if (Input.GetMouseButton(0) && onMove)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                curPos = Input.mousePosition;
                speed = ((initPos - curPos).x / Time.deltaTime) * offsetSpeed;
                curPos = initPos;
            }
        }
        else
        {
            initPos = Vector2.zero;
            curPos = Vector2.zero;
            speed = 0;
            onMove = false;
            return;
        }

        Vector2 moveVec = Vector2.right * speed * Time.deltaTime;
        if (transform.position.x <= -4 && speed < 0 
                              ||
            transform.position.x >= 4 && speed > 0) return;
        transform.Translate(moveVec);
    }
}
