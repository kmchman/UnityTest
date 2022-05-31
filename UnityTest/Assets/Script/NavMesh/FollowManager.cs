using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowManager : AlmostEngine.Singleton<FollowManager>
{
    [SerializeField] private TargetObj targetObj;

    private List<PlayerNavMesh> playerList = new List<PlayerNavMesh>();
    public void AddPlayer(PlayerNavMesh _obj)
    {
        playerList.Add(_obj);
    }


}
