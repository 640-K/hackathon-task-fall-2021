using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;

namespace Entities
{

    public class Entity : MonoBehaviour
    {
        public const string deathTrigger = "dead";
        public const string stateEnum = "state";
        public const string actionEnum = "action";



        [Header("Features")]
        public uint health = 100;
        public float believerLevel = 0.25f;



        [Header("Movement")]
        public float maxSpeed = 1f;

        [Range(0, 1)] [Tooltip("Ability to develop speed. If 1 is given, the speed change is instant.")]
        public float accelerationRate = .25f;

        [Range(0, 1)] [Tooltip("Speed decrease when the player does not move. If 1 is given, the speed drops to 0 instantly.")]
        public float deccelerationRate = 1f;

        [Range(0, 1)] [Tooltip("Ability to resist excentric force when turning. If 1 is given, the excentic force does not influence the movement.")]
        public float maneuverability = 1f;

        public float speedFactor = 1f;


        [Space(10)]
        public EntityEvents events;


        [Header("Components")]
        public GameObject avatarCenterPivot;
        public Rigidbody2D physicsBody;
        public Collider2D collisionBounds;
        public Animator avatarAnimator;



        public uint currentHealth { get; protected set; }

        // manages continuous states (like Idle or Move)
        public int state { get => avatarAnimator.GetInteger(stateEnum); set { if (!dead) avatarAnimator.SetInteger(stateEnum, value); } }

        // manages activatable states (like Attack or Hurt)
        public int action { get => avatarAnimator.GetInteger(actionEnum); set { if (!dead) avatarAnimator.SetInteger(actionEnum, value); } }

        public Vector2 motionDirection { get; protected set; }
        public bool dead { get => currentHealth == 0; }
        



        public virtual void Start()
        {
            currentHealth = health;
            motionDirection = Vector2.zero;
        }

        public virtual void OnValidate()
        {
            // acceleration can't have 0 value, or else the player won't move,
            // so it is set to physics default time
            if (accelerationRate == 0)
                accelerationRate = Time.fixedDeltaTime;
        }

        protected virtual void FixedUpdate()
        {
            if (dead) motionDirection = Vector2.zero;

            if (motionDirection.magnitude > 0)
            {
                Vector2 perpMotionDirection = Vector2.Perpendicular(motionDirection);
                physicsBody.AddForce(-perpMotionDirection * physicsBody.mass * Vector2.Dot(physicsBody.velocity, perpMotionDirection) * maneuverability, ForceMode2D.Impulse);

                physicsBody.AddForce(motionDirection * physicsBody.mass * (maxSpeed * speedFactor - Vector2.Dot(physicsBody.velocity, motionDirection)) * accelerationRate, ForceMode2D.Impulse);

                events.onMove.Invoke();
            }
            else
                physicsBody.AddForce(-physicsBody.velocity * physicsBody.mass * deccelerationRate, ForceMode2D.Impulse);
        }



        public virtual void Move(Vector2 direction)
        {
            direction.Normalize();
            if (dead || direction == motionDirection) return;

            if (motionDirection.magnitude == 0)
            {
                if (direction.magnitude != 0)
                {
                    state = 1;
                    events.onStartMoving.Invoke();
                }
            }
            else if (direction.magnitude == 0)
            {
                state = 0;
                events.onStopMoving.Invoke();
            }

            if (direction.x > 0)
                avatarCenterPivot.transform.localScale = Vector3.one;
            if(direction.x < 0)
                avatarCenterPivot.transform.localScale = new Vector3(-1, 1, 1);


            motionDirection = direction;
        }

        public uint Hurt(uint damage)
        {
            if (dead || damage == 0) return 0;

            uint damageDealt = Math.Min(damage, currentHealth);
            currentHealth -= damageDealt;


            if (dead)
            {
                avatarAnimator.SetTrigger(deathTrigger);
                events.onDie.Invoke(damageDealt);
            }
            else
            {
                action = 3;
                events.onHurt.Invoke(damageDealt);
            }

            return damageDealt;
        }

        public void Kill() => Hurt(health);

        public uint Heal(uint factor)
        {
            if (dead) return 0;

            uint healingDealt = Math.Min(factor, health - currentHealth);
            currentHealth += healingDealt;

            events.onHeal.Invoke(healingDealt);

            return healingDealt;
        }

        public void Resurrect()
        {
            if (!dead) return;

            currentHealth = health;

            avatarAnimator.Rebind();

            events.onResurrect.Invoke();
        }
    }




    [Serializable]
    public partial class EntityEvents
    {
        public UnityEvent onStartMoving;
        public UnityEvent onMove;
        public UnityEvent onStopMoving;
        public UnityEvent<uint> onDie;
        public UnityEvent<uint> onHurt;
        public UnityEvent<uint> onHeal;
        public UnityEvent onResurrect;
    }
}
