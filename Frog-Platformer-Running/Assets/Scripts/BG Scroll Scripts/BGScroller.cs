using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    public float offsetSpeed = 0.0006f;
    private Renderer _myRenderer;

    public bool canScroll;

    private void Awake()
    {
        _myRenderer = GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canScroll)
        {
            _myRenderer.material.mainTextureOffset += new Vector2(offsetSpeed, 0);
        }
    }
}
