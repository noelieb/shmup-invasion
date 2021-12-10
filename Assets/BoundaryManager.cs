using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //hors de l'écran //screen blabla
        if (transform.position.z < 20)
        {
            transform.Translate(new Vector3(0, 0, 500));
        }
    }
}
