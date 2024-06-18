using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovmentBasic : MonoBehaviour
{
    public Transform m_Target;

    public Vector3 PatrolPos1 = new Vector3(10, 2, -10);
    public Vector3 PatrolPos2 = new Vector3(10, 0, 10);

    public float movementSpeed = 2.0f; 

    private Vector3 currentTarget;

    void Start()
    {
        currentTarget = PatrolPos1;
    }

    void Update()
    {
        m_Target.position = Vector3.Lerp(m_Target.position, currentTarget, movementSpeed * Time.deltaTime);
        if (Vector3.Distance(m_Target.position, currentTarget) < 0.05f)
        {
            if (currentTarget == PatrolPos1)
                currentTarget = PatrolPos2;
            else
                currentTarget = PatrolPos1;
        }
    }
}
