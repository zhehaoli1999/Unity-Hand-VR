using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHandCollider : MonoBehaviour
{
    private GameObject hand;

    private struct FingerBone
    {
        public readonly float Radius;
        public readonly float Height;
        public FingerBone(float radius, float height)
        {
            Radius = radius;
            Height = height;
        }
        public Vector3 GetCenter()
        {
            return new Vector3(Height / 2.0f, 0, 0);
        }
    }

    private readonly FingerBone Phalanges = new FingerBone(0.01f,0.03f);
    private readonly FingerBone Metacarpals = new FingerBone(0.01f,0.05f);

    //CreateCollider: Create CapsuleCollider for each finger joint. 
    //Parameter: the transform of the finger joint.
    private void CreateCollider(Transform transform)
    {
        //if the object has "ignore" in its name, just ignore it.
        if(transform.name.Contains("ignore")|| transform.name.EndsWith("hand") ){ return; }
        // if the object is the palm, it needs a box collider.
        if(transform.name.Contains("grip"))
        {
            // if did not have collider before:
            if(!transform.gameObject.GetComponent(typeof(SphereCollider)))
            {
                SphereCollider collider1 = transform.gameObject.AddComponent<SphereCollider>();
                collider1.radius = 0.03f;
                collider1.center = new Vector3(0.01f,0.015f,0.035f);
                CapsuleCollider collider2 = transform.gameObject.AddComponent<CapsuleCollider>();
                collider2.radius = 0.01f;
                collider2.height = 0.06f;
                collider2.center = new Vector3(0.01f,0.05f,0.035f);
                collider2.direction = 2;
            }
        }

        // if the object is not palm but a finger, it needs a capsulde collider
        else if(!transform.gameObject.GetComponent(typeof(CapsuleCollider)))
        {
            // if the finger bone is not a Metacarpals
            if(!transform.name.EndsWith("0"))
            {
                CapsuleCollider collider = transform.gameObject.AddComponent<CapsuleCollider>();
                if(!transform.name.EndsWith("1"))
                {
                    collider.radius = Phalanges.Radius;
                    collider.height = Phalanges.Height;
                    collider.center = Phalanges.GetCenter();
                    collider.direction = 0;
                }
                // if the finger bone is a Metacarpals
                else
                {
                    collider.radius = Metacarpals.Radius;
                    collider.height = Metacarpals.Height;
                    collider.center = Metacarpals.GetCenter();
                    collider.direction = 0;
                }
            }
        }
    }

    // RecurseChildrenCreateCollider: is used to create collider for each child of a parent Transform
    // Parameter: transform of the parent
    private void RecurseChildrenCreateCollider(Transform transform)
    {
        // if this transform has children
        // Debug.Log(transform.gameObject.ToString());
        if(transform.childCount >0 )
        {
            foreach(Transform child in transform)
            {
                RecurseChildrenCreateCollider(child);
                CreateCollider(transform);
            }
        }
        // if this transform has no child, end recursion!
        else
        {
            CreateCollider(transform);
            return; 
        }
    }

    void Start()
    {
        hand = GameObject.Find("hands:b_r_hand");
    }

    void Update()
    {
        RecurseChildrenCreateCollider(hand.transform);
    }
}
