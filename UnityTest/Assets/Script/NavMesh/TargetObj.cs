using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TargetObj : FollowObject
{
    private List<FollowObject> followList = new List<FollowObject>();

    private void Awake()
    {
        uid = FollowManager.Instance.SUid++;
    }
    public bool HasEmptyFollowSlot()
    {
        return followList.Count < 5;
    }

    public FollowObject AddFollow(FollowObject obj)
    {
        followList.Add(obj);

        if (followList.Count == 1)
        {
            return this;
        }
        return followList[followList.Count - 2];
    }

    public void RemoveFollow(int uid)
    {
        followList.RemoveAll(item => item.uid == uid);
    }

}
