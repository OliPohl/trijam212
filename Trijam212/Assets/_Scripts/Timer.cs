using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float _timer;
    private TMP_Text _text;

    void Start()
    {
        _timer = 0;
        _text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gameObject.activeSelf == false)
        {
            _timer = 0;
        }

        _timer += Time.deltaTime;

        var minutes = (int) (_timer / 60);
        var seconds = (int) (_timer - minutes * 60);
        
        _text.text = minutes.ToString() + ":" + seconds.ToString();
    }
}
