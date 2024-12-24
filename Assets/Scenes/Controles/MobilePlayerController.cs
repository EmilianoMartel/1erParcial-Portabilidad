using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePlayerController : MonoBehaviour
{
    [SerializeField] private GameObject mobileControls;

    [SerializeField] private float speed = 1;
    [SerializeField] private MoveJoystick moveJoystick;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        bool isMobile = false;
#if UNITY_ANDROID
        isMobile = true;
#endif

        mobileControls.SetActive(isMobile);
    }

    void Update()
    {
        Vector3 translation = Vector3.zero;

        if (Input.GetKey(KeyCode.W) || moveJoystick.isGoingUp)
        {
            translation.z = 1;
        }
        if (Input.GetKey(KeyCode.A) || moveJoystick.isGoingLeft)
        {
            translation.x = -1;
        }
        if (Input.GetKey(KeyCode.S) || moveJoystick.isGoingDown)
        {
            translation.z = -1;
        }
        if (Input.GetKey(KeyCode.D) || moveJoystick.isGoingRight)
        {
            translation.x = 1;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Jump(1);
        }

        var speedMult = speed * Time.fixedDeltaTime;
        transform.Translate(translation.x * speedMult, 0, translation.z * speedMult);

        bool isMobile = false;
#if UNITY_ANDROID
        isMobile = true;
#endif
        if (!isMobile && Input.GetMouseButtonDown(0))
            Shoot();
    }

    public void Jump(float impulse)
    {
        transform.Translate(0, impulse * speed * Time.fixedDeltaTime, 0);
    }

    public void Shoot()
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