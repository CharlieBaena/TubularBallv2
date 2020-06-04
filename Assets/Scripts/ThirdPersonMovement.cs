using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Transform groundCheck;
    public LayerMask groundMask;
    public AudioClip sonidoBoost, sonidoVuelta, sonidoRecord;

    public static List<Vector3> posicionesAlmacenadas = new List<Vector3>();
    public static bool comienzoAReproducir = false;


    public float speed = 6f;
    public float gravity = -9.81f;
    public float turnSmoothTime = 0.1f;
    public float groundDistance = 0.4f;
    public float jumpHeighy = 3f;

    private AudioSource emisorAudio;
    private float turnSmoothVelocity;
    private Vector3 gravityVelocity;
    private bool isGrounded;
    private float defaultSpeed;
    private List<Vector3> posicionesTemporales = new List<Vector3>();

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        defaultSpeed = speed;
        emisorAudio = GetComponent<AudioSource>();
    }



    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = -2f;
        }

        gravityVelocity.y += gravity * Time.deltaTime;

        controller.Move(gravityVelocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            gravityVelocity.y = Mathf.Sqrt(jumpHeighy * -2f * gravity);
        }

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
  
        }
        if(NewLap.comienzoAGrabar)
            posicionesTemporales.Add(gameObject.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("SpeedBoost"))
        {
            emisorAudio.PlayOneShot(sonidoBoost);
            speed = 2f * speed;
            StartCoroutine(Esperar(1f));
        }

        if (other.gameObject.CompareTag("RampBoost"))
        {
            emisorAudio.PlayOneShot(sonidoBoost);
            speed = 1.5f * speed;
            StartCoroutine(Esperar(1f));
        }
    }


    public void NewRecordSound()
    {
        emisorAudio.PlayOneShot(sonidoRecord);
    }

    public void LapSound()
    {
        emisorAudio.PlayOneShot(sonidoVuelta);
    }

    IEnumerator Esperar(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        speed = defaultSpeed;
    }

    public void CopiarNuevoRecord()
    {
        posicionesAlmacenadas.Clear();
        for(int i = 0; i < posicionesTemporales.Count; i++)
        {
            posicionesAlmacenadas.Add(posicionesTemporales[i]);
        }
        posicionesTemporales.Clear();
    }

    public void ResetearTemporal()
    {
        posicionesTemporales.Clear();
    }
}
