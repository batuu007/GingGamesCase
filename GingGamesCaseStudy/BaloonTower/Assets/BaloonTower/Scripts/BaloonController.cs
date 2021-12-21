using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonController : MonoBehaviour
{
    public Material[] Materials;
    public Transform target;
    public GameObject baloon;
    public float speed = 1f;

    private Vector3 targetSize;

    private Rigidbody rb;
    private LineRenderer lineRenderer;

    void Start()
    {
        Components();
        MaterialChange();
        InitialSize();
    }

    private void Components()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void InitialSize()
    {
        targetSize = new Vector3(0.5f, 0.5f, 0.5f);
        gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
    }

    private void MaterialChange()
    {
        GameObject material = transform.gameObject;
        material.GetComponent<MeshRenderer>().material = Materials[Random.Range(0, 5)];
    }

    void Update()
    {
        LineRendererController();
        Strength();
        SizeChange();
    }

    private void Strength()
    {
        rb.AddForce(Vector3.up * speed, ForceMode.Impulse);
    }

    private void LineRendererController()
    {
        target.position = baloon.transform.position;

        lineRenderer.SetPosition(0, new Vector3(0, 0.835f, -0.33f));
        lineRenderer.SetPosition(1,target.position);
        lineRenderer.SetWidth(0.001f, 0.001f);

        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
    }
    private void SizeChange()
    {
        if (gameObject.transform.localScale.magnitude<=targetSize.magnitude)
        {
            gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }
    }
}
