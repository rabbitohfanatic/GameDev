﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {

    //public float to modify gravity within unity
    public float minGroundNormalY = .65f;
    public float gravityModifier = 1f;

    protected Vector2 targetVelocity;

    public bool grounded;
    protected Vector2 groundNormal;
    //Rigidbody2D of object
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    //ContactFilter2D to pass to Cast
    protected ContactFilter2D contactFilter;

    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];

    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    //GetComponent for object
    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start(){
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    private void Update(){
        targetVelocity = Vector2.zero; 
        ComputeVelocity();
    }

    protected virtual void ComputeVelocity(){
        
    }

    //Runs every frame to Calculate gravity of object
    void FixedUpdate() {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        grounded = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);

        velocity.x = targetVelocity.x;
    }

    //applies gravity to Object
    void Movement (Vector2 move, bool yMovement){
        float distance = move.magnitude;

        if(distance > minMoveDistance){
            //Use rigidbody2D.cast to check if any of the attached colliders of rb2d are going to
            //overlap with anything in the next frame
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();
            for (int i = 0; i < count; i++){
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++){
                Vector2 currentNormal = hitBufferList[i].normal;
                if(currentNormal.y > minGroundNormalY){
                    grounded = true;
                    if(yMovement){
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(velocity, currentNormal);
                if(projection < 0){
                    velocity = velocity - projection * currentNormal;
                }
                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }

        }
        rb2d.position = rb2d.position + move.normalized * distance;
    }
}
