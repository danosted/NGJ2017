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
        protected float JumpFade { get; set; }
        protected float MoveMagnitude { get; set; }
        protected float CurrentJumpValue { get; set; }
        private Vector3 _jumpDirection;
        protected Rigidbody RigidBody { get; set; }
        private bool _isJumping { get; set; }

        public override void Activate(IoC container)
        {
            base.Activate(container);
            JumpMagnitude = Configuration.param_player_jump_magnitude;
            JumpFade = Configuration.param_player_jump_fade;
            MoveMagnitude = Configuration.param_player_move_magnitude;
            _jumpDirection = Vector3.up;
            gameObject.SetActive(true);
            RigidBody = GetComponent<Rigidbody>();

            // Spawn player inside the level
            transform.position = Configuration.param_player_initial_position;
        }

        public void Deactivate()
        {
            PrefabManager.ReturnPrefab(this);
        }

        protected virtual void FixedUpdate()
        {
            if (Input.GetButtonDown("Jump") && !_isJumping)
            {
                Jump();
            }
            else if (_isJumping)
            {
                if (CurrentJumpValue == 1.5f * Mathf.PI/* && RigidBody.velocity.y == 0*/)
                {
                    //Debug.LogFormat("stopped jump.");
                    _isJumping = false;
                    return;
                    RigidBody.useGravity = true;
                }
                Debug.LogFormat("Sine: {0}.", Mathf.Sin(_jumpDirection.y));
                transform.Translate(_jumpDirection.normalized * Time.deltaTime * JumpMagnitude);
                CurrentJumpValue = Mathf.MoveTowards(CurrentJumpValue, 1.5f * Mathf.PI, Time.deltaTime * JumpFade);
                _jumpDirection.y = Mathf.Sin(CurrentJumpValue);
                //Debug.LogFormat("RB velo {0}. Jump dir: {1}.", RigidBody.velocity, _jumpDirection.y);
            }
            Move();
            //MoveRigid();
            //CheckOutOfBounds();
        }

        //private bool IsJumping()
        //{
        //    //var rb = GetComponent<Rigidbody>();
        //    //if (rb.velocity.y < -0.1f || 0.1f < rb.velocity.y)
        //    //{
        //    //    return true;
        //    //}
        //    //return false;
        //}

        protected virtual void Jump()
        {
            //RigidBody.isKinematic = true;
            RigidBody.useGravity = false;
            _isJumping = true;
            _jumpDirection = Vector3.up;
            CurrentJumpValue = 0.5f * Mathf.PI;
            //RigidBody.AddForce(_jumpDirection * JumpMagnitude, ForceMode.Impulse);

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
            RigidBody.AddForce(new Vector3(xMove, 0f, zMove).normalized * Time.deltaTime * MoveMagnitude, ForceMode.Impulse);
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
