﻿using System;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Base;
using com.ootii.Helpers;
using com.ootii.Utilities;
using com.ootii.Utilities.Debug;

namespace com.ootii.AI.Controllers
{
    /// <summary>
    /// Controller layers are used to group motions together that
    /// typically don't run at the same time. For example, running,
    /// jumping, and climbing don't happen at the exact same time.
    /// However, someone may be running and swing a sword. Each motion
    /// would be in a seperate layer.
    /// </summary>
    [Serializable]
    public class MotionControllerLayer : BaseObject
    {
        /// <summary>
        /// Friendly name for the layer
        /// </summary>
        public string _Name = "";
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        /// <summary>
        /// Index in the list of motion layers that this layer represents.
        /// </summary>
        private int mIndex = 0;
        public int Index
        {
            get { return mIndex; }
            set { mIndex = value; }
        }

        /// <summary>
        /// Controller this motion is tried to
        /// </summary>
        private MotionController mController;
        public MotionController Controller
        {
            get { return mController; }
            set { mController = value; }
        }

        /// <summary>
        /// Determines the index of the layer in the mechanim animator
        /// where the corresponding layer lies.
        /// </summary>
        [SerializeField]
        protected int mAnimatorLayerIndex = 0;
        public int AnimatorLayerIndex
        {
            get { return mAnimatorLayerIndex; }
            set { mAnimatorLayerIndex = value; }
        }

        /// <summary>
        /// List of motions the controller manages
        /// </summary>
        protected List<MotionControllerMotion> mMotions = new List<MotionControllerMotion>();
        public List<MotionControllerMotion> Motions
        {
            get { return mMotions; }
            set { mMotions = value; }
        }

        /// <summary>
        /// TODO
        /// </summary>
        public List<string> MotionDefinitions = new List<string>();

        /// <summary>
        /// The motion that is running and has top priority.
        /// While we could actually be running multiple motions,
        /// this represents the 'primary' one.
        /// </summary>
        private MotionControllerMotion mActiveMotion;
        public MotionControllerMotion ActiveMotion
        {
            get { return mActiveMotion; }
        }

        /// <summary>
        /// Time in seconds that the active motion has been running for
        /// </summary>
        private float mActiveMotionDuration = 0f;
        public float ActiveMotionDuration
        {
            get { return mActiveMotionDuration; }
        }

        /// <summary>
        /// Returns phase of the motion that is currently
        /// running. While we could actually be running multiple
        /// motions, this represents the primary one.
        /// </summary>
        public int ActiveMotionPhase
        {
            get
            {
                if (mActiveMotion != null) { return mActiveMotion.Phase; }
                return 0;
            }
        }

        /// <summary>
        /// Current velocity caused by the motion. This should be
        /// multiplied by delta-time to create displacement
        /// </summary>
        private Vector3 mVelocity = Vector3.zero;
        public Vector3 Velocity
        {
            get { return mVelocity; }
        }

        /// <summary>
        /// Amount of rotation caused by the motion. This should be
        /// multiplied by delta-time to create angular displacement
        /// </summary>
        private Vector3 mAngularVelocity = Vector3.zero;
        public Vector3 AngularVelocity
        {
            get { return mAngularVelocity; }
        }

        /// <summary>
        /// Determine if the layer motion is disabling gravity
        /// </summary>
        public bool IsGravityEnabled
        {
            get
            {
                bool lResult = true;
                if (mActiveMotion != null) { lResult = mActiveMotion.IsGravityEnabled; }
                
                return lResult;
            }
        }

        /// <summary>
        /// Determines if we use trend data when sending speed data
        /// to the animator
        /// </summary>
        public bool UseTrendData
        {
            get
            {
                bool lResult = true;
                if (mActiveMotion != null) { lResult = mActiveMotion.UseTrendData; }

                return lResult;
            }
        }

        /// <summary>
        /// Returns any camera offsets generated by this layer
        /// </summary>
        public Vector3 CameraOffset
        {
            get
            {
                Vector3 lResult = Vector3.zero;
                if (mActiveMotion != null) { lResult = mActiveMotion.RootMotionCameraOffset; }

                return lResult;
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public MotionControllerLayer() 
            : base()
        {
        }

        /// <summary>
        /// Controller constructor
        /// </summary>
        /// <param name="rController">Controller the layer is tied to</param>
        public MotionControllerLayer(MotionController rController) 
            : base()
        {
            mController = rController;
        }

        /// <summary>
        /// Controller constructor
        /// </summary>
        /// <param name="rController">Controller the layer is tied to</param>
        public MotionControllerLayer(string rName, MotionController rController)
            : base()
        {
            _Name = rName;
            mController = rController;
        }

        /// <summary>
        /// Adds a motion to the list of motions being managed
        /// </summary>
        /// <param name="rMotion">Motion to add</param>
        public void AddMotion(MotionControllerMotion rMotion)
        {
            if (!mMotions.Contains(rMotion))
            {
                rMotion.Controller = mController;
                rMotion.MotionLayer = this;

                mMotions.Add(rMotion);
            }
        }

        /// <summary>
        /// Removes the motion from the list of motions being managed
        /// </summary>
        /// <param name="rMotion">Motion to remove</param>
        public void RemoveMotion(MotionControllerMotion rMotion)
        {
            mMotions.Remove(rMotion);

            rMotion.Controller = null;
            rMotion.MotionLayer = null;
        }

        /// <summary>
        /// Attempt to activate the specified motion and make it the 
        /// active motion.
        /// </summary>
        /// <param name="rMotion">Motion to activate</param>
        /// <returns>Determines if the motion was set as the active motion</returns>
        public bool QueueMotion(MotionControllerMotion rMotion)
        {
            if (!mMotions.Contains(rMotion)) { return false; }
            if (mActiveMotion == rMotion) { return false; }

            rMotion.QueueActivation = true;
            return true;
        }

        /// <summary>
        /// Load the animator state and transition IDs
        /// </summary>
        public void LoadAnimatorData()
        {
            // Create the motions to match the defintions
            InstanciateMotions();

            // Allow the motions to load thier data
            int lMotionCount = mMotions.Count;
            for (int i = 0; i < lMotionCount; i++)
            {
                mMotions[i].LoadAnimatorData();
            }
        }

        /// <summary>
        /// Allows the motion to modify the ground and support information
        /// </summary>
        /// <param name="rState">Current state whose support info can be modified</param>
        /// <returns>Boolean that determines if the avatar is grounded</returns>
        public virtual bool DetermineGrounding(ref ControllerState rState)
        {
            if (mActiveMotion != null)
            {
                return mActiveMotion.DetermineGrounding(ref rState);
            }

            return rState.IsGrounded;
        }

        /// <summary>
        /// Allows the motion to modify the velocity before it is applied.
        /// </summary>
        /// <returns></returns>
        public void CleanRootMotion(ref Vector3 rRootMotionVelocity, ref Quaternion rRootMotionAngularVelocity)
        {
            if (mActiveMotion != null)
            {
                // Check the motions to determine if we should remove the root motion.
                // If even one active motion wants it removed, remove it.
                mActiveMotion.CleanRootMotion(ref rRootMotionVelocity, ref rRootMotionAngularVelocity);
            }
        }

        /// <summary>
        /// Updates the motions tied to this layer at the variable
        /// time step (ie Update() not FixedUpdate().
        /// </summary>
        public void UpdateMotions()
        {
            int lPriorityIndex = int.MinValue;
            float lPriorityValue = float.MinValue;

            // Track how long the motion has been playing
            mActiveMotionDuration += Time.deltaTime;

            // Clean up the active motion's flag
            if (mActiveMotion != null) { mActiveMotion.IsActivatedFrame = false; }

            // First, check if our current motion is interruptible.
            // If it's not, we know we are simply running it.
            if (mActiveMotion == null || mActiveMotion.IsInterruptible)
            {
                // Cycle through the motions to determine which ones were not
                // active and should be. We'll take the motion with the highest priority
                for (int i = 0; i < mMotions.Count; i++)
                {
                    // Clean up the activation flag
                    mMotions[i].IsActivatedFrame = false;

                    // Don't test if the motion is not enabled
                    if (!mMotions[i].IsEnabled) { continue; }

                    // If we're to force the motion, don't check others
                    if (mMotions[i].QueueActivation)
                    {
                        lPriorityIndex = i;
                        lPriorityValue = mMotions[i].Priority;

                        mMotions[i].QueueActivation = false;
                        break;
                    }
                    // Check for activation
                    else if (mMotions[i].IsStartable && mMotions[i].TestActivate())
                    {
                        if (lPriorityValue < mMotions[i].Priority)
                        {
                            lPriorityIndex = i;
                            lPriorityValue = mMotions[i].Priority;
                        }
                    }
                }

                // If we have a newly chosen motion, we need to activate it
                if (lPriorityIndex >= 0 && lPriorityIndex < mMotions.Count)
                {
                    // Ensure the motion will allow the interruption and
                    // shuts down as needed. If not, we cancel the upcoming motion.
                    if (mActiveMotion != null)
                    {
                        if (mActiveMotion.Priority > lPriorityValue)
                        {
                            lPriorityIndex = int.MinValue;
                        }
                        else if (!mActiveMotion.OnInterruption(mMotions[lPriorityIndex]))
                        {
                            lPriorityIndex = int.MinValue;
                        }
                    }

                    // Look to start the new motion
                    if (lPriorityIndex >= 0)
                    {
                        mMotions[lPriorityIndex].Activate(mActiveMotion);

                        mActiveMotion = mMotions[lPriorityIndex];
                        mActiveMotionDuration = 0f;
                    }
                }
            }

            // Process any motions that are active. They will die out on thier own
            for (int i = 0; i < mMotions.Count; i++)
            {
                if (mMotions[i].IsActive)
                {
                    mMotions[i].UpdateMotion();

                    // As a safetly, test and set the active motion
                    if (mActiveMotion == null && mMotions[i].IsActive) 
                    {
                        mActiveMotion = mMotions[i];
                        mActiveMotionDuration = 0f;
                    }
                }
            }

            // Check if we've deactivated the current motion. If so, we
            // need to remove our reference to it
            if (mActiveMotion != null && !mActiveMotion.IsActive)
            {
                mActiveMotion = null;
                mActiveMotionDuration = 0f;
            }

            // Calculate the velocities of the active motions
            mAngularVelocity = Vector3.zero;
            mVelocity = Vector3.zero;

            for (int i = 0; i < mMotions.Count; i++)
            {
                if (mMotions[i].IsActive)
                {
                    mAngularVelocity += mMotions[i].AngularVelocity;
                    mVelocity += mMotions[i].Velocity;
                }
            }
        }

        /// <summary>
        /// Updates the motions tied to this layer at the end
        /// of the update cycles (ie LateUpdate() not Update().
        /// </summary>
        public void LateUpdateMotions()
        {
            // Process any motions that are active. They will die out on thier own
            for (int i = 0; i < mMotions.Count; i++)
            {
                if (mMotions[i].IsActive)
                {
                    mMotions[i].LateUpdateMotion();
                }
            }
        }

        /// <summary>
        /// Allows the layer to process IK animations
        /// </summary>
        public virtual void UpdateIK()
        {
            for (int i = 0; i < mMotions.Count; i++)
            {
                if (mMotions[i].IsActive)
                {
                    mMotions[i].UpdateIK();
                }
            }
        }

        /// <summary>
        /// Raised when the animator's state has changed
        /// </summary>
        public void OnAnimatorStateChange(int rAnimatorLayer, int rLastStateID, int rNewStateID)
        {
            // Send the state change to all active motions
            for (int i = 0; i < mMotions.Count; i++)
            {
                // We allow the motions to interrogate state changes.
                if (mMotions[i].IsActive && mMotions[i].AnimatorLayerIndex == rAnimatorLayer)
                {
                    mMotions[i].OnAnimatorStateChange(rLastStateID, rNewStateID);
                }
            }
        }

        /// <summary>
        /// Processes the motion definitions and updates the motions to match
        /// the definitions.
        /// </summary>
        public void InstanciateMotions()
        {
            int lMotionCount = mMotions.Count;
            int lMotionDefCount = MotionDefinitions.Count;

            // First, remove any extra motions that may exist
            for (int i = lMotionCount - 1; i > lMotionDefCount; i--)
            {
                mMotions.RemoveAt(i);
            }

            // We need to match the motion definitions to the motions
            for (int i = 0; i < lMotionDefCount; i++)
            {
                string lDefinition = MotionDefinitions[i];
                JSONNode lDefinitionNode = JSONNode.Parse(lDefinition);
                if (lDefinitionNode == null) { continue; }

                MotionControllerMotion lMotion = null;
                string lType = lDefinitionNode["Type"].Value;

                float lPriority = 0;

                // If don't have a motion matching the type, we need to create one
                if (Motions.Count <= i || lType != mMotions[i].GetType().AssemblyQualifiedName)
                {
                    lMotion = Activator.CreateInstance(Type.GetType(lType)) as MotionControllerMotion;
                    lMotion.Controller = mController;
                    lMotion.MotionLayer = this;
                    
                    if (mMotions.Count <= i)
                    {
                        mMotions.Add(lMotion);
                    }
                    else
                    {
                        mMotions[i] = lMotion;
                    }

                    // Track the priority so we can reset it
                    lPriority = lMotion.Priority;
                }
                // Grab the matching motion
                else
                {
                    lMotion = mMotions[i];
                }

                // Fill the motion with data from the definition
                if (lMotion != null) 
                { 
                    lMotion.DeserializeMotion(lDefinition);

                    // Reset the priority based on the default
                    if (lPriority > 0 && lMotion.Priority == 0f) { lMotion.Priority = lPriority; }

                    // We re-serialize the motion incase there was a change. If the
                    // type changed or some other value, we want the updated definition
                    MotionDefinitions[i] = lMotion.SerializeMotion();
                }
            }
        }
    }
}
