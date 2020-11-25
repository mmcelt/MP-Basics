using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyNetworkManager : NetworkManager
{
	#region Fields

	MyNetworkPlayer _player;

	#endregion

	#region Mirror Callbacks

	public override void OnServerAddPlayer(NetworkConnection conn)
	{
		base.OnServerAddPlayer(conn);

		_player = conn.identity.GetComponent<MyNetworkPlayer>();

		_player.SetDisplayName($"Player {numPlayers}");

		Color randomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

		_player.SetDisplayColor(randomColor);
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
