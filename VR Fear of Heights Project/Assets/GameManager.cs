using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;
    private PaintManager m_paintManager;

    public static GameManager GetInstance()
    {
        return m_instance;
    }

    public PaintManager GetPaintManager()
    {
        return m_paintManager;
    }

    private void Awake()
    {
        if (null != m_instance)
            Destroy(gameObject);
        else
        {
            m_instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        m_paintManager = GetComponent<PaintManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
