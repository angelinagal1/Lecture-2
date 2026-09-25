using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] private int cubesCount;
    private float _angle;
    [Range(0.1f, 50)][SerializeField] private float radius;
    [Range(0, 500)][SerializeField] private float speed;
    [SerializeField] private bool clockwise;
    private List<GameObject> _spawnedCubes = new List<GameObject>();

    private void Awake()
    {
        float baseAngle = Mathf.PI * 2 / cubesCount;
        for (int i = 0; i < cubesCount; i++)
        {
            SpawnCube(baseAngle * i);
        }
    }

    private void SpawnCube(float currentAngle)
    {
        GameObject newCube = Instantiate(obj, transform);
        Vector3 positionCube = new Vector3(Mathf.Cos(currentAngle), 0, Mathf.Sin(currentAngle));
        newCube.transform.position = positionCube * radius;
        _spawnedCubes.Add(newCube);
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * (speed * (clockwise ? -1 : 1) * Time.deltaTime));
    }

    private void FixedUpdate()
    {
        ChangePositions();
    }

    private void ChangePositions()
    {
        for (int i = 0; i < cubesCount; i++)
        {
            Vector3 newPositionCube = _spawnedCubes[i].transform.localPosition.normalized;
            _spawnedCubes[i].transform.localPosition = newPositionCube * radius;
        }
    }
}