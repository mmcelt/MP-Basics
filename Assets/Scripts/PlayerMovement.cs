using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : NetworkBehaviour
{
	#region Fields

	[SerializeField] NavMeshAgent _navAgent;

	Camera _mainCamera;

	#endregion

	#region Server Methods

	[Command]
	void CmdMove(Vector3 position)
	{
		if (!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas)) return;
		_navAgent.SetDestination(hit.position);
	}
	#endregion

	#region Client Methods

	public override void OnStartAuthority()
	{
		_mainCamera = Camera.main;
	}

	[ClientCallback]
	void Update()
	{
		if (!hasAuthority) return;
		if (!Input.GetMouseButtonDown(1)) return;

		Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

		if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) return;

		CmdMove(hit.point);
	}
	#endregion
}
