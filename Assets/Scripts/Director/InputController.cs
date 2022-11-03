using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public GameObject player;

    private PlayerMovement playerMovement;

    private PlayerAttack playerAtack;

    public float timeToDetectJump = 1f;

    public DynamicJoystick joystick;
    public float timeToDetectShoot = 0.2f;
    private float elapsedTimeJumpingDetection, elapsedTimeShootingDetection;

    private void Awake()
    {
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
            playerAtack = player.GetComponent<PlayerAttack>();
        }
        else
        {
            Debug.Log("NO PLAYER ASSIGNED");
        }
    }

    /*#if UNITY_EDITOR

        void Start(){
            Debug.Log("UNITY MODE.");

              joystick.gameObject.SetActive(false);
        }
        // Update is called once per frame
        void Update()
        {
            playerMovement.Move(Input.GetAxis("Horizontal"));

            if ( elapsedTimeJumpingDetection > timeToDetectJump && Input.GetKey(KeyCode.Space)) {
                playerMovement.Jump();

                elapsedTimeJumpingDetection = 0;
            } else
            {
                elapsedTimeJumpingDetection += Time.deltaTime;
            }

            if ( ( elapsedTimeShootingDetection > timeToDetectShoot ) && (Input.GetButton("Fire1"))){
                //playerAtack.Attack();

                elapsedTimeShootingDetection = 0;
            } else{
                elapsedTimeShootingDetection += Time.deltaTime;
            }
        }

    #else
    */

    private void Start()
    {
        joystick.gameObject.SetActive(true);

        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    private void Update()
    {
#if UNITY_EDITOR
        playerMovement.Move(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftControl)) playerAtack.Attack();

#else
        playerMovement.Move(new Vector3(joystick.Horizontal,joystick.Vertical));
#endif
    }
}