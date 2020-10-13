using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GrappleScript : MonoBehaviour
{
    [SerializeField]
    LineRenderer grappleRenderer;
    SpringJoint joint;
    Vector3 grapplepoint;
    Camera mainCamera;
    public LayerMask playerLayerMask;

    float grappleSpringForce = 100f;
    float grappleMinDistance = 0.1f;
    float grappleMaxDistance = 0.2f;

    float grappleDamper = 0.25f;
    float grappleDistance = 50f;



    void Awake()
    {
        grappleRenderer = GetComponent<LineRenderer>();
        grappleRenderer.enabled = false;
        grappleRenderer.positionCount = 2;
        mainCamera = Camera.main;
    }

    void Grapple(Vector3 grapplePoint, Rigidbody otherBody)
    {
        joint = gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        if (otherBody)
        {
            joint.connectedBody = otherBody;
            joint.connectedAnchor = grapplePoint - otherBody.transform.position;
        }
        else
        {
            joint.connectedAnchor = grapplepoint;
        }

        joint.maxDistance = grappleMaxDistance;
        joint.minDistance = grappleMinDistance;
        joint.spring = grappleSpringForce;
        joint.damper = grappleDamper;
        grappleRenderer.enabled = true;

    }

    void UnGrapple()
    {
        if (joint)
        {
            grappleRenderer.enabled = false;
            GameObject.Destroy(joint);
        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (Physics.Raycast(ray, out hit, grappleDistance, ~playerLayerMask))
            {
                if (hit.collider.tag == "Enemy")
                {
                    KillEnemy(hit.collider);
                    return;
                }

                Transform objectHit = hit.transform;
                grapplepoint = hit.point;

                Grapple(hit.point, hit.rigidbody);
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            UnGrapple();
        }
    }

    void KillEnemy(Collider collider)
    {
        //collider.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * grappleSpringForce, ForceMode.Impulse);
        GameObject.Destroy(collider.gameObject);
    }



    void LateUpdate()
    {
        if (grappleRenderer.enabled)
        {
            Vector3 grappleStart = transform.position;
            grappleRenderer.SetPositions(new Vector3[] { grapplepoint, grappleStart });
        }
    }
}