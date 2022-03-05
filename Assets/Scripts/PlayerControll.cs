using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public float downForceValue;
    public float centerMass = 3.9f;
    public float power = 200.0f;
    public float rot = 45.0f;
    private Rigidbody rb;

    public WheelCollider[] wheels = new WheelCollider[2];
    GameObject[] wheelMesh = new GameObject[2];

    RaycastHit hitForward;
    RaycastHit hitDown;

    private float maxDistance = 2.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, centerMass, 0);


        wheelMesh = GameObject.FindGameObjectsWithTag("WHEELMESH");
        for (int i = 0; i < wheelMesh.Length; i++)
        {
            wheels[i].transform.position = wheelMesh[i].transform.position;
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            // for���� ���ؼ� ���ݶ��̴� ��ü�� Vertical �Է¿� ���� power��ŭ�� ������ �����̰��Ѵ�.
            if (Input.GetKey(KeyCode.W))
            {
                wheels[i].brakeTorque = 0;
                wheels[i].motorTorque = power;
            }

            if (Input.GetKey(KeyCode.S))
            {
                wheels[i].motorTorque = 0;
                wheels[i].brakeTorque = power * 3;
            }
        }

        for (int i = 0; i < wheels.Length / 2; i++)
        {
            wheels[i].steerAngle = Input.GetAxis("Horizontal") * rot;//�չ��� ����ȸ��
        }

        //WheelPosAndAni();
        AddDownForce();
        rotateFixed();
    }

    void WheelPosAndAni()
    {
        Vector3 wheelPosition = Vector3.zero;
        Quaternion wheelRotation = Quaternion.identity;

        for (int i = 0; i < 2; i++)
        {
            wheels[i].GetWorldPose(out wheelPosition, out wheelRotation);
            wheelMesh[i].transform.position = wheelPosition;
            wheelMesh[i].transform.rotation = wheelRotation;
        }
    }

    void AddDownForce()
    {
        //rb.velocity.magnitude�� ��ü�� �����ӷ��� ���밪���� ǥ���� ��. ��, �ӵ��� �������� �з��� ������
        rb.AddForce(-transform.up * downForceValue * rb.velocity.magnitude);
    }

    void rotateFixed()
    {
        Debug.DrawRay(transform.position - new Vector3(0,3.9f,0), -transform.up, Color.blue);
        if (Physics.Raycast(transform.position, -transform.up, out hitDown, maxDistance))
        {
            Debug.Log(hitDown.point);
        }
        else
            Debug.Log("hitDown is nothing");

        Debug.DrawRay(transform.position - new Vector3(0, 3.9f, 0), transform.forward, Color.blue);
        if (Physics.Raycast(transform.position, transform.forward, out hitForward, maxDistance))
        {
            Debug.Log(hitForward.point);
        }
        else
            Debug.Log("hitForward is nothing");


        //Debug.Log("x: " + transform.eulerAngles.x + " z: " + transform.eulerAngles.z);

    }

}




