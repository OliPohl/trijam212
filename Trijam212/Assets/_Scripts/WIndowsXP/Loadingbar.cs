using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loadingbar : MonoBehaviour
{
    [Range(0, 1)] public float progress;
    
    private Vector2 _startPos;
    private Vector2 _maxSize;


    void Start()
    {
        _startPos = transform.position;
        _maxSize = transform.localScale;
    }


    void FixedUpdate()
    {
        float x = progress * _maxSize.x;
        x = Mathf.Clamp(x, 0f, _maxSize.x);

        transform.localScale = new Vector2(x, transform.localScale.y);
        transform.position = new Vector2((_startPos.x + transform.localScale.x) / 2, _startPos.y);
    }
}
