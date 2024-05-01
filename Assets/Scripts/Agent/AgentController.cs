using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.InputSystem;

public class AgentController : Agent
{
    Vector3[] drxns = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] int objectCount;

    RandomSpawner _spawner;
    Rigidbody rb;
    int consecutive = 1;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        // Reset the agent
        transform.localPosition = new Vector3(0, 0, 0);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Collect observations

        sensor.AddObservation(transform.position);

        //if (_spawner == null && RandomSpawner.instance != null) SetSpawner();
        //else if (_spawner == null)
        //{
        //    EmptyObservation(sensor);
        //    return;
        //}

        //RealObservation(sensor);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveRotate = actions.ContinuousActions[0];
        float moveForward = actions.ContinuousActions[1];

        rb.MovePosition(transform.position + transform.forward * moveForward * moveSpeed * Time.deltaTime);
        transform.Rotate(transform.up * moveRotate * moveSpeed * .2f);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // Define heuristic
        var discreteActionsOut = actionsOut.DiscreteActions;
        if (Input.GetKey(KeyCode.W))
        {
            discreteActionsOut[0] = 0;
        }
        if (Input.GetKey(KeyCode.S))
        {
            discreteActionsOut[0] = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            discreteActionsOut[0] = 2;
        }
        if (Input.GetKey(KeyCode.D))
        {
            discreteActionsOut[0] = 3;
        }

    }

    private void EmptyObservation(VectorSensor sensor)
    {

        for (int i = 0; i < objectCount * 2; i++)
        {
            sensor.AddObservation(0f);
            sensor.AddObservation(Vector3.zero);
            sensor.AddObservation(true);
        }
    }

    private void RealObservation(VectorSensor sensor)
    {
        foreach (var human in _spawner.humans)
        {
            float dist = Vector3.Distance(human.transform.position, transform.position) / 50;
            var dir = (human.transform.position - transform.position).normalized;
            sensor.AddObservation(dist);
            sensor.AddObservation(dir);
            sensor.AddObservation(true);
        }

        foreach (var enemy in _spawner.enemies)
        {
            float dist = Vector3.Distance(enemy.transform.position, transform.position) / 50;
            var dir = (enemy.transform.position - transform.position).normalized;
            sensor.AddObservation(dist);
            sensor.AddObservation(dir);
            sensor.AddObservation(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Human"))
        {
            consecutive = 1;
            AddReward(-1f);
            Destroy(collision.gameObject);
            Debug.Log("Human killed!");
        } else if (collision.gameObject.CompareTag("Enemy"))
        {
            consecutive++;
            AddReward(consecutive * 1f);
            Destroy(collision.gameObject);
            Debug.Log("Enemy killed!");
        } else if (collision.gameObject.CompareTag("Wall"))
        {
            AddReward(-1f);
            Debug.Log("Wall hit!");
        }

        EndEpisode();
    }
}
