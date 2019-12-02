using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInEditor : MonoBehaviour {

    #if UNITY_EDITOR
    [SerializeField] int levelToLoad = 1;
    
	[SerializeField] ConfigPhaseOne m_configP1;
    [SerializeField] ConfigPhaseTwo m_configP2;

    void Awake()
	{
		if (GameState.Instance == null) {
			GameState.CreateGame(m_configP1, m_configP2, levelToLoad);
		}
		Destroy (this);
	}
	#endif
}
