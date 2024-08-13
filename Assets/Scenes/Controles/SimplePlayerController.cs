using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 1;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        Vector3 translation = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            translation.z = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            translation.x = -1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            translation.z = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            translation.x = 1;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            translation.y = 1;
        }

        var speedMult = speed * Time.fixedDeltaTime;
        transform.Translate(translation.x * speedMult, translation.y * speedMult, translation.z * speedMult);

        if (Input.GetMouseButtonDown(0))
            Shoot();
    }

    private void Shoot()
    {
        if (shootingCoroutine != null)
            StopCoroutine(shootingCoroutine);

        shootingCoroutine = StartCoroutine(ShootAction());
    }

    private Coroutine shootingCoroutine;
    [SerializeField] private GameObject shooting;
    private IEnumerator ShootAction()
    {
        shooting.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        shooting.SetActive(false);
    }
}
