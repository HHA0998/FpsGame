using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;

    public GameObject projectilePrefab;
    public Transform spawnTransform;
    public float force = 700;

    private int CurrentAmmo;
    public int MaxAmmo = 100;

    void Start()
    {
        CurrentAmmo = MaxAmmo;
    }

    void Update()
    {
        textMeshProUGUI.text = CurrentAmmo.ToString();
        if (Input.GetButtonDown("Fire1")&& CurrentAmmo >= 1)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, spawnTransform.position, spawnTransform.rotation);
            newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * force);
            CurrentAmmo--;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            CurrentAmmo = MaxAmmo;
        }
    }
}
