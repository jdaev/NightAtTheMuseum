using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float PlayerSpeed = 20;
    public float JumpForce = 2;
    public float shootForce = 100;
    public bool isGrounded = true;
    public Camera mainCamera;
    public float sensitivity = 1f;

    public GameObject resourceObj;

    public GameObject toLoad;

    public float timeBetweenSpawn = 1;
    float timeOfNextSpawn;

    Rigidbody PlayerBody;
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerBody = gameObject.GetComponent<Rigidbody>();
        resourceObj = Resources.Load<GameObject>("RPGPP_LT/Prefabs/Props/Furniture/Chairs/rpgpp_lt_chair_01a.prefab");
        timeOfNextSpawn = Time.time + timeBetweenSpawn;
    }

    void Update()
    {
        RotatePlayer();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();

    }

    void MovePlayer()
    {
        if (Input.GetKey(KeyCode.W))
        {

            PlayerBody.AddForce(mainCamera.transform.forward * PlayerSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            PlayerBody.AddForce(-mainCamera.transform.right * PlayerSpeed);

        }
        if (Input.GetKey(KeyCode.S))
        {

            PlayerBody.AddForce(-mainCamera.transform.forward * PlayerSpeed);

        }
        if (Input.GetKey(KeyCode.D))
        {
            PlayerBody.AddForce(mainCamera.transform.right * PlayerSpeed);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (isGrounded)
                PlayerBody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
        if (Input.GetMouseButton(0))
        {
            if (timeOfNextSpawn < Time.time)
            {
                GameObject obj = GameObject.Instantiate(toLoad);
                obj.transform.position = transform.position;
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                rb.AddForce(mainCamera.transform.forward*shootForce,ForceMode.Impulse);
                timeOfNextSpawn = timeBetweenSpawn + Time.time;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        string layer = LayerMask.LayerToName(other.gameObject.layer);
        //Debug.Log(string.Format("Collided with {0}, LAYER: {1}", other.gameObject.name, other.gameObject.layer));
        if (layer == "Ground")
        {

            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        string layer = LayerMask.LayerToName(other.gameObject.layer);
        if (layer == "Ground")
        {
            isGrounded = false;
        }
    }

    void RotatePlayer()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        //Debug.Log(mouseX);
        //Debug.Log(mouseY);
        mainCamera.transform.eulerAngles += new Vector3(mouseY, mouseX, 0);
        // mainCamera.transform.RotateAround(transform.position, Vector3.up ,mouseX * sensitivity);
        // mainCamera.transform.RotateAround(transform.position, -transform.right, mouseY * sensitivity);
    }
}
