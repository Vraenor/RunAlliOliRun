using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpDetectionAndInterception : MonoBehaviour {

	private Collider interceptorCollider;
	private GameObject kartParent;
	private ExampleAIControllerNewMode AIScript;
	private List<GameObject> obstaclesList;
	private GameObject obstacleToGet;
	private bool IsIntercepting = false;

	// Use this for initialization
	void Start () 
	{
		interceptorCollider = GetComponent<SphereCollider> ();
		kartParent = this.transform.parent.gameObject;
		AIScript = kartParent.GetComponent<ExampleAIControllerNewMode> ();
		RefreshObstacles ();

	}
	
	// Update is called once per frame
	void Update () 
	{

		if (!IsIntercepting)
		{
			RefreshObstacles ();
			for (int i = 0; i < obstaclesList.Count; i++) 
			{
				if (interceptorCollider.collider.bounds.Intersects(obstaclesList[i].collider.bounds)) 
				{
					obstacleToGet = obstaclesList[i];
					AIScript.targetObject = null;
					IsIntercepting = true;
					break;
				}
			}
		}

		else
		{
			kartParent.transform.LookAt(obstacleToGet.transform);
			kartParent.transform.Translate(Vector3.forward * kartParent.GetComponent<KartControllerNewMode>().topSpeedMPH/10 * Time.deltaTime);
			if (obstacleToGet.activeSelf == false)
			{
				ApplyPowerUpEffect();
				obstaclesList.Remove(obstacleToGet);
				AIScript.targetObject = kartParent.transform.parent.gameObject.transform;
				IsIntercepting = false;
			}
		}

	}

	void RefreshObstacles()
	{
		obstaclesList = AIScript.ObstaclesList.GetComponent<ObstaclesListNewMode> ().obstacles;
	}

	void ApplyPowerUpEffect()
	{
		switch (obstacleToGet.name) {
		case "Pickups_Bomb":
			kartParent.GetComponent<KartControllerNewMode>().SpeedPenalty();
			kartParent.GetComponent<KartControllerNewMode>().Spin(2.0f);
			break;
		case "Pickups_OilSlick":
			kartParent.GetComponent<KartControllerNewMode>().SpeedPenalty(0.25f, 1.0f, 1.0f);
			kartParent.GetComponent<KartControllerNewMode>().Wiggle(1.0f);
			break;
		}
	}
}
