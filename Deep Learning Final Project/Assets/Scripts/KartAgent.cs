using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class KartAgent : Agent
{
    public int numberOfIterationsPerTrack = 5;

   public CheckpointManager _checkpointManager;
   public TrackGenerator _trackGenerator;
   private KartController _kartController;


    private void Awake()
    {
        numberOfIterationsPerTrack = Mathf.Max(1, numberOfIterationsPerTrack);
    }

    public override void Initialize()
   {
      _kartController = GetComponent<KartController>();
   }
   
   public override void OnEpisodeBegin()
   {
      if(this.CompletedEpisodes % numberOfIterationsPerTrack == 0)
       {
         _trackGenerator.GenerateTrack();
         _checkpointManager.UpdateCheckPoints();
       }

        //Debug.Log("Episodes completed: " + this.CompletedEpisodes);

      _checkpointManager.ResetCheckpoints();
      _kartController.Respawn();
      
   }

   public override void CollectObservations(VectorSensor sensor)
   {
      //Vector3 diff = _checkpointManager.nextCheckPointToReach.transform.position - transform.position;
      Vector3 diff = transform.InverseTransformPoint(_checkpointManager.nextCheckPointToReach.transform.position); // Observe the location of the next checkpoint in local space 
      sensor.AddObservation(diff / 20f);
      AddReward(-0.001f);
   }

   public override void OnActionReceived(ActionBuffers actions)
   {
      var Actions = actions.ContinuousActions;
      
      _kartController.ApplyAcceleration(Actions[1]);
      _kartController.Steer(Actions[0]);
      _kartController.AnimateKart(Actions[0]);
   }
   
   public override void Heuristic(in ActionBuffers actionsOut)
   {
      var continousActions = actionsOut.ContinuousActions;
      continousActions.Clear();
      
      continousActions[0] = Input.GetAxis("Horizontal");
      continousActions[1] = Input.GetKey(KeyCode.W) ? 1f : 0f;
   }

   public void EndEpisodeWithPartialReward()
    {
        this.AddReward(_checkpointManager.GetPartialReward());
        this.EndEpisode();
    }
}
