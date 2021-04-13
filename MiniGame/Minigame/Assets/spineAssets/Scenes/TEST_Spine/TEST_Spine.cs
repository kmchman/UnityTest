using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_Spine : MonoBehaviour
{
    private SkeletonData mSkeletonData;

    private Spine.Animation[] mAnimations;

    private SkeletonAnimation[] mAnimationObjects;

    private void Start()
    {
        //SkeletonDataAsset skeletonDataAsset = Resources.Load<SkeletonDataAsset>("Chelsia/Chelsia_SkeletonData");
        //mSkeletonData = skeletonDataAsset.GetAnimationStateData().SkeletonData;
        //Debug.Log("Animations.Count : " + mSkeletonData.Animations.Count);

        //mAnimations = mSkeletonData.Animations.Items;
        //Debug.Log("mAnimations[0].Name : " + mAnimations[0].Name);

        //GameObject go;

        //float xx = -5;
        //mAnimationObjects = new SkeletonAnimation[3];
        //for (int i = 0; i < mAnimationObjects.Length; i++)
        //{
        //    go = new GameObject(string.Format("{0}_{1}", i, skeletonDataAsset.name));
        //    go.transform.position = new Vector3(xx, -5, 0);
        //    xx += 5f;

        //    mAnimationObjects[i] = go.AddComponent<SkeletonAnimation>();
        //    mAnimationObjects[i].skeletonDataAsset = skeletonDataAsset;

        //    mAnimationObjects[i].Start();
        //    mAnimationObjects[i].AnimationState.SetAnimation(0, mAnimations[0].Name, true);
        //}
    }

}
