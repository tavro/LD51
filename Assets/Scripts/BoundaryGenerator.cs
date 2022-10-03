using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryGenerator : MonoBehaviour
{
    private void Start()
    {
        for (int idx = 0; idx < 4; idx++)
        {
            GameObject boundary = new GameObject($"Boundary {idx + 1}");
            boundary.layer = LayerMask.NameToLayer("Obstacles");
            boundary.transform.parent = transform;
            BoxCollider2D collider = boundary.AddComponent<BoxCollider2D>();
            
            float camHeight = Camera.main.orthographicSize * 2;
            float camWidth = Camera.main.aspect * camHeight;

            float x = 0, y = 0;
            float width = 0, height = 0;
            switch (idx)
            {
                case 0:
                    x = camWidth / 2 + 0.5f;
                    y = 0;
                    width = 1;
                    height = camHeight;
                    break;
                case 1:
                    x = -(camWidth / 2 + 0.5f);
                    y = 0;
                    width = 1;
                    height = camHeight;
                    break;
                case 2:
                    x = 0;
                    y = camHeight / 2 + 0.5f;
                    width = camWidth;
                    height = 1;
                    break;
                case 3:
                    x = 0;
                    y = -(camHeight / 2 + 0.5f);
                    width = camWidth;
                    height = 1;
                    break;
            }

            collider.offset = new Vector2(x, y);
            collider.size = new Vector2(width, height);
        }
    }  
}
