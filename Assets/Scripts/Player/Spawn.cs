using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Transform m_player;

    public Vector3 SpawnPoint = new Vector3(0, 0, 0);
    
    private void OnEnable()
    {
        m_player.position = SpawnPoint;
    }
}
