using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    public GameObject target;

    
    void Start()
    {
        
    }

    
    void LateUpdate()
    {
        this.transform.position = new Vector3(target.transform.position.x, this.transform.position.y, this.transform.position.z);
        this.transform.position = new Vector4(this.transform.position.x, target.transform.position.y, this.transform.position.z);
    }
}
