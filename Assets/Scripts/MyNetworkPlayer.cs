using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
	#region Fields

	[SerializeField] TMP_Text _displayNameText;
	[SerializeField] Renderer _displayColorRenderer;

	[SerializeField] [SyncVar(hook = nameof(HandleDisplayNameUpdated))] string _displayName = "Missing Name";
	[SerializeField] [SyncVar(hook = nameof(HandleDisplayColorUpdated))] Color _displayColor = Color.white;

	#endregion

	#region SyncVar Callbacks (Hooks)

	void HandleDisplayNameUpdated(string oldName, string newName)
	{
		_displayNameText.SetText(newName);
	}

	void HandleDisplayColorUpdated(Color oldColor, Color newColor)
	{
		_displayColorRenderer.material.SetColor("_BaseColor", newColor);
	}
	#endregion

	#region Server Methods

	[Server]
	public void SetDisplayName(string newName)
	{
		_displayName = newName;
	}

	[Server]
	public void SetDisplayColor(Color newColor)
	{
		_displayColor = newColor;
	}

	[Command]
	void CmdSetDisplayName(string newDisplayName)
	{
		if(newDisplayName.Length >= 2 && newDisplayName.Length <= 15)
		{
			RpcLogNewName(newDisplayName);
			SetDisplayName(newDisplayName);
		}
	}
	#endregion

	#region Client Methods

	[ContextMenu("Set My Name")]
	void SetMyName()
	{
		CmdSetDisplayName("My New Name");
	}

	[ClientRpc]
	void RpcLogNewName(string newName)
	{
		Debug.Log(newName);
	}
	#endregion
}
