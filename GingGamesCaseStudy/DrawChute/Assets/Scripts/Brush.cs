using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public Rigidbody2D brushRigidbody;

    public LineRenderer LineRenderer
    {
        get
        {
            return lineRenderer;
        }
    }

    public EdgeCollider2D EdgeCollider
    {
        get
        {
            return edgeCollider;
        }
    }

    public Rigidbody2D BrushRigidbody
    {
        get
        {
            return brushRigidbody;
        }
    }
}
