using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HierarchicalCenterOfMass : MonoBehaviour
{
    public Transform[] children;
    public bool visualizeCenterOfMass = true;
    public float sphereSize = 0.1f;

    void Start()
    {
        children = GetComponentsInChildren<Transform>();
        Vector3 totalCenterOfMass = CalculateTotalCenterOfMass();
        Debug.Log("Total Center of Mass: " + totalCenterOfMass);
    }


    Vector3 CalculateTotalCenterOfMass()
    {
        float totalMass = 0f;
        Vector3 weightedPositionSum = Vector3.zero;

        foreach (Transform child in children)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb != null)
            {
                float mass = rb.mass;
                Vector3 centerOfMass = rb.worldCenterOfMass;
                weightedPositionSum += centerOfMass * mass;
                totalMass += mass;
            }
        }

        if (totalMass > 0)
        {
            return weightedPositionSum / totalMass;
        }
        else
        {
            return transform.position; // 기본값으로 Parent의 위치 반환
        }
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying || !visualizeCenterOfMass)
            return;

        Vector3 totalCenterOfMass = CalculateTotalCenterOfMass();

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(totalCenterOfMass, sphereSize);
    }
}
