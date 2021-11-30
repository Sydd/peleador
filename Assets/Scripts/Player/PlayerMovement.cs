using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Unidades por segundo.")]
    [Tooltip("Cuantas unidades se mueve por segundo en X & en Z")]
    public Vector3 speed;
    [Header("Cuantos segundos tienen que pasar para que termine corriendo a la velocidad full")]
    public float TimeToAchieveFullSpeed;
    [Header("En funcion de _timeMoving / TimeToAchieveFullSpeed TimeToAchieveFullSpeed")]
    public AnimationCurve speedCurve;
    public float PercentOfVelocityToRun;


    bool facingRight = true;
    Vector3 vecMovementFactor = Vector3.zero;

    // TODO: Esto hay que mandarlo al player
    // y desde aca solo hay que ejecutar el evento onMoving.
    public Action onPlayerStop;
    public Action<bool> onFlip;

    public Vector3 Speed;


    private float _timeMoving;

    private void Update()
    {
        if (vecMovementFactor.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (vecMovementFactor.x < 0 && facingRight)
        {
            Flip();
        }
    }
    void FixedUpdate()
    {

        if (vecMovementFactor.magnitude > 0)
        {

            _timeMoving += Time.fixedDeltaTime;

            Vector3 actualSpeed = Vector3.Scale(vecMovementFactor, speed * speedCurve.Evaluate(_timeMoving / TimeToAchieveFullSpeed));

            Vector3 newPosition = transform.position + (new Vector3(actualSpeed.x,actualSpeed.z,actualSpeed.y) * Time.fixedDeltaTime);

            transform.position = newPosition;

            Speed =actualSpeed;
        }
        else
        {
            Speed = Vector3.zero;
            _timeMoving = 0;
            if (onPlayerStop != null)
            {
                onPlayerStop();
            }
        }
    }

    public void Move(Vector3 vecMovementFactor)
    {
        this.vecMovementFactor = vecMovementFactor;
    }

    public void StopMove()
    {
        this.vecMovementFactor = Vector3.zero;
    }

    private void Flip()
    {
        facingRight = !facingRight;

        onFlip(facingRight);
    }
}
