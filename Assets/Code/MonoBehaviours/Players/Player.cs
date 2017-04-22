namespace Assets.Code.MonoBehaviours.Players
{
    using UnityEngine;
    using IoC;
    using DataAccess;
    using GameLogic;
    using Common;
    using Utilities;

    public class Player : PrefabBase
    {
        protected float JumpMagnitude { get; set; }
        protected float MoveMagnitude { get; set; }
        private Vector3 _jumpDirection;
        protected Rigidbody RigidBody { get; set; }

        public virtual void Activate(IoC container)
        {
            base.Activate(container);
            JumpMagnitude = Configuration.param_player_jump_magnitude;
            MoveMagnitude = Configuration.param_player_move_magnitude;
            _jumpDirection = Vector3.up;
            gameObject.SetActive(true);

            // Spawn player inside the level
            var collider = GetComponent<Collider>();
            transform.position = transform.position + Vector3.forward * collider.bounds.extents.x;

            RigidBody = GetComponent<Rigidbody>();
        }

        public void Deactivate()
        {
            PrefabManager.ReturnPrefab(this);
        }

        protected virtual void FixedUpdate()
        {
            if (Input.GetButtonUp("Jump") && !IsJumping())
            {
                Jump();
            }
            Move();
            //MoveRigid();
            //CheckOutOfBounds();
        }

        private bool IsJumping()
        {
            var rb = GetComponent<Rigidbody>();
            if (rb.velocity.y < -0.01f || 0.01f < rb.velocity.y)
            {
                return true;
            }
            return false;
        }

        protected virtual void Jump()
        {
            RigidBody.AddForce(_jumpDirection * JumpMagnitude, ForceMode.Impulse);
            //transform.Translate(_jumpDirection.normalized * Time.deltaTime * Speed);
        }

        protected virtual void Move()
        {
            var xMove = Input.GetAxis("Horizontal");
            var zMove = Input.GetAxis("Vertical");
            //var jump = Input.
            transform.Translate(new Vector3(Time.deltaTime * xMove * MoveMagnitude, 0f, Time.deltaTime * zMove * MoveMagnitude));
        }

        protected virtual void MoveRigid()
        {
            var xMove = Input.GetAxis("Horizontal");
            var zMove = Input.GetAxis("Vertical");
            Debug.LogFormat("mag {0}.", Time.deltaTime * MoveMagnitude);
            RigidBody.AddForce(new Vector3(xMove, 0f, zMove).normalized * Time.deltaTime * MoveMagnitude, ForceMode.VelocityChange);
        }

        protected virtual void CheckOutOfBounds()
        {
            //if(!Container.Resolve<ScreenUtil>().IsOutOfViewportBounds(transform.position))
            //{
            //    return;
            //}
            //Deactivate();
        }

        protected virtual void OnMouseEnter()
        {
            //ScoreLogic.AddToScore(-(int)Speed);
            //Deactivate();
        }
    }
}
