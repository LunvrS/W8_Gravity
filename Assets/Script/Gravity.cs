using System;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    Rigidbody rb;
    const float G = 0.00674f;

    public static List<Gravity> gravityObjects;

    [SerializeField] bool planet = false;
    [SerializeField] int orbitSpeed = 1000;
     
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (gravityObjects == null)
        {
            gravityObjects = new List<Gravity>();
        }
        
        gravityObjects.Add(this);

        if (!planet)
        { rb.AddForce(Vector3.left * orbitSpeed); }
    }

    private void FixedUpdate()
    {
        //all Attract
        foreach (Gravity obj in gravityObjects)
        {
            Attract(obj);
        }
    }

    void Attract(Gravity other)
    {
        Rigidbody rbOther = other.rb;
        
        Vector3 direction = rb.position - rbOther.position;
        
        float distance = direction.magnitude;
        
        if (distance == 0) { return; }
        
        float forceMagnitude = ((rb.mass * rbOther.mass)/ Mathf.Pow(distance, 2));
        Vector3 force = forceMagnitude * direction.normalized;

        rbOther.AddForce(force);
    }
    
}