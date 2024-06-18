using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float m_DampTime = 0.3f;
    public float menuMoveSpeed = 1.0f;
    public float unitsMaxMove = 5.0f;

    private Vector3 m_movingvelocity;
    private Vector3 m_Togoto;

    private void FixedUpdate()
    {
        MoveInMenu();
    }

    private void MoveInMenu()
    {
        float Movement = Mathf.Sin(Time.time * menuMoveSpeed) * unitsMaxMove;
        float Offset = Mathf.Cos(Time.time * menuMoveSpeed) * unitsMaxMove;

        m_Togoto = new Vector3(Movement, transform.position.y ,Movement * Offset);
        transform.position = Vector3.SmoothDamp(transform.position, m_Togoto, ref m_movingvelocity, m_DampTime);
    }

}
