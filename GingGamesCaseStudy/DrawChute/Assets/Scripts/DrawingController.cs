using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawingController : MonoBehaviour
{
    public Spawn spawn;
    public Camera cam;
    public GameObject brush;
    public float brushThickness = 0.4f;

    private GameObject drawingBrush;
    private List<Vector2> touchPoints = new List<Vector2>();

    void Update()
    {
        TouchController();
    }

    private void TouchController()
    {
        foreach (Touch touch in Input.touches)
        {
            int id = touch.fingerId;

            if (EventSystem.current.IsPointerOverGameObject(id))
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Vector2 touchPosition = cam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, cam.transform.position.z * -1));
                    StartDraw(touchPosition);

                    var brushRigidbody = drawingBrush.GetComponent<Brush>();
                    brushRigidbody.BrushRigidbody.gravityScale = 0;
                }

            }

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchPosition = cam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, cam.transform.position.z * -1));
                UpdateDraw(touchPosition);
            }


            if (touch.phase == TouchPhase.Ended)
            {
                var brushRigidbody = drawingBrush.GetComponent<Brush>();
                brushRigidbody.BrushRigidbody.gravityScale = 1;

                GameObject.Destroy(spawn.TrueSpawn.GetChild(0).gameObject);

                var brush = Instantiate(drawingBrush, Vector3.zero, Quaternion.identity);

                Transform spawnPosition = new GameObject("TrueSpawn").transform;
                spawnPosition.position = touchPoints[0];
                brush.transform.SetParent(spawnPosition);

                spawnPosition.SetParent(spawn.TrueSpawn);
                spawnPosition.localPosition = Vector3.zero;
                spawnPosition.localEulerAngles = Vector3.zero;
                spawnPosition.localScale *= brushThickness;

                GameObject.Destroy(drawingBrush);
            }
        }
    }

    public void StartDraw(Vector2 touchPosition)
    {
        touchPoints.Clear();
        touchPoints.Add(touchPosition);
        touchPoints.Add(touchPosition);

        drawingBrush = Instantiate(brush);
        var brushObject = drawingBrush.GetComponent<Brush>();

        brushObject.LineRenderer.SetPosition(0, touchPoints[0]);
        brushObject.LineRenderer.SetPosition(1, touchPoints[1]);

        brushObject.EdgeCollider.points = touchPoints.ToArray();
    }

    public void UpdateDraw(Vector2 touchPosition)
    {
        if (Vector2.Distance(touchPosition, touchPoints[touchPoints.Count - 1]) > .2f)
        {
            touchPoints.Add(touchPosition);

            var brushObject = drawingBrush.GetComponent<Brush>();

            brushObject.LineRenderer.positionCount += 1;
            brushObject.LineRenderer.SetPosition(brushObject.LineRenderer.positionCount - 1, touchPoints[touchPoints.Count - 1]);

            brushObject.EdgeCollider.points = touchPoints.ToArray();
        }
    }
}
