using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Standard : MonoBehaviour, IInteractable
{
    [Range(0, 1)] public float progressBar;
    
    private Vector2 _startPosBar;
    private Vector2 _maxSizeBar;
    private float timer;

    public float maxTimer = 20f;


    void Start()
    {
        _startPosBar = transform.position;
        _maxSizeBar = transform.localScale;
        timer = 0;
    }


    void FixedUpdate()
    {
        if(UpdateBar())
        {
            // - points
            // spawn more
            Destroy(gameObject);
        }
    }


    private bool UpdateBar()
    {
        timer += Time.deltaTime;
        progressBar = timer / maxTimer;

        float x = progressBar * _maxSizeBar.x;
        x = Mathf.Clamp(x, 0f, _maxSizeBar.x);

        transform.localScale = new Vector2(x, transform.localScale.y);
        transform.position = new Vector2((_startPosBar.x + transform.localScale.x) / 2, _startPosBar.y);

        if(progressBar > 1)
        {
            return true;
        }
        return false;
    }


    public void Interact()
    {
        // + points
        Destroy(gameObject);
    }
}