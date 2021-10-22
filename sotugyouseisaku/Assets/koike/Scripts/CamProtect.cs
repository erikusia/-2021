using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamProtect : MonoBehaviour
{
    private GameObject Parent;
    [SerializeField]
    private GameObject gameObject;

    private Vector3 Position;

    private RaycastHit Hit;

    private float Distance;

    private int Mask;

    void Start()
    {
        Parent = transform.root.gameObject;

        //transform.localPosition = new Vector3(0.03f, 2.14f, 0.582f);
        Position = transform.localPosition;


        Distance = Vector3.Distance(Parent.transform.position, transform.position);

        Mask = ~(1 << LayerMask.NameToLayer("Player"));
    }

    void Update()
    {
        if (Physics.CheckSphere(gameObject.transform.position, 0.3f, Mask))
        {
            Debug.Log("A");
            transform.position = Vector3.Lerp(transform.position, gameObject.transform.position, 1);
        }
        else if (Physics.SphereCast(gameObject.transform.position, 0.3f, (transform.position - gameObject.transform.position).normalized, out Hit, Distance, Mask))
        {
            Debug.Log("B");
            transform.position = gameObject.transform.position + (transform.position - gameObject.transform.position).normalized * Hit.distance;
        }
        else
        {
            Debug.Log("C");
            transform.localPosition = Vector3.Lerp(transform.localPosition, Position, 1);

        }
    }
}
