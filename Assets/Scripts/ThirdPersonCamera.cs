using UnityEngine;

[System.Serializable]
public partial class ThirdPersonCamera : MonoBehaviour
{
    public Transform cameraTransform;
    private Transform _target;
    // The distance in the x-z plane to the target
    public float distance;
    // the height we want the camera to be above the target
    public float height;
    public float angularSmoothLag;
    public float angularMaxSpeed;
    public float heightSmoothLag;
    public float snapSmoothLag;
    public float snapMaxSpeed;
    public float clampHeadPositionScreenSpace;
    public float lockCameraTimeout;
    private Vector3 headOffset;
    private Vector3 centerOffset;
    private float heightVelocity;
    private float angleVelocity;
    private bool snap;
    private ThirdPersonController controller;
    private float targetHeight;
    public virtual void Awake()
    {
        if (!cameraTransform && Camera.main)
        {
            cameraTransform = Camera.main.transform;
        }
        if (!cameraTransform)
        {
            Debug.Log("Please assign a camera to the ThirdPersonCamera script.");
            enabled = false;
        }
        _target = transform;
        if (_target)
        {
            controller = (ThirdPersonController)_target.GetComponent(typeof(ThirdPersonController));
        }
        if (controller)
        {
            CharacterController characterController = _target.GetComponent<CharacterController>();
            centerOffset = characterController.bounds.center - _target.position;
            headOffset = centerOffset;
            headOffset.y = characterController.bounds.max.y - _target.position.y;
        }
        else
        {
            Debug.Log("Please assign a target to the camera that has a ThirdPersonController script attached.");
        }
        Cut(_target, centerOffset);
    }

    public virtual void DebugDrawStuff()
    {
        Debug.DrawLine(_target.position, _target.position + headOffset);
    }

    public virtual float AngleDistance(float a, float b)
    {
        a = Mathf.Repeat(a, 360);
        b = Mathf.Repeat(b, 360);
        return Mathf.Abs(b - a);
    }

    public virtual void Apply(Transform dummyTarget, Vector3 dummyCenter)
    {
        // Early out if we don't have a target
        if (!controller)
        {
            return;
        }
        Vector3 targetCenter = _target.position + centerOffset;
        Vector3 targetHead = _target.position + headOffset;
        //	DebugDrawStuff();
        // Calculate the current & target rotation angles
        float originalTargetAngle = _target.eulerAngles.y;
        float currentAngle = cameraTransform.eulerAngles.y;
        // Adjust real target angle when camera is locked
        float targetAngle = originalTargetAngle;
        // When pressing Fire2 (alt) the camera will snap to the target direction real quick.
        // It will stop snapping when it reaches the target
        if (Input.GetButton("Fire2"))
        {
            snap = true;
        }
        if (snap)
        {
            // We are close to the target, so we can stop snapping now!
            if (AngleDistance(currentAngle, originalTargetAngle) < 3f)
            {
                snap = false;
            }
            currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref angleVelocity, snapSmoothLag, snapMaxSpeed);
        }
        else
        {
            // Normal camera motion
            if (controller.GetLockCameraTimer() < lockCameraTimeout)
            {
                targetAngle = currentAngle;
            }
            // Lock the camera when moving backwards!
            // * It is really confusing to do 180 degree spins when turning around.
            if ((AngleDistance(currentAngle, targetAngle) > 160) && controller.IsMovingBackwards())
            {
                targetAngle = targetAngle + 180;
            }
            currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref angleVelocity, angularSmoothLag, angularMaxSpeed);
        }
        // When jumping don't move camera upwards but only down!
        if (controller.IsJumping())
        {
            // We'd be moving the camera upwards, do that only if it's really high
            float newTargetHeight = targetCenter.y + height;
            if ((newTargetHeight < targetHeight) || ((newTargetHeight - targetHeight) > 5))
            {
                targetHeight = targetCenter.y + height;
            }
        }
        else
        {
            // When walking always update the target height
            targetHeight = targetCenter.y + height;
        }
        // Damp the height
        float currentHeight = cameraTransform.position.y;
        currentHeight = Mathf.SmoothDamp(currentHeight, targetHeight, ref heightVelocity, heightSmoothLag);
        // Convert the angle into a rotation, by which we then reposition the camera
        Quaternion currentRotation = Quaternion.Euler(0, currentAngle, 0);
        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        cameraTransform.position = targetCenter;
        cameraTransform.position = cameraTransform.position + ((currentRotation * Vector3.back) * distance);

        {
            float _210 = // Set the height of the camera
            currentHeight;
            Vector3 _211 = cameraTransform.position;
            _211.y = _210;
            cameraTransform.position = _211;
        }
        // Always look at the target	
        SetUpRotation(targetCenter, targetHead);
    }

    public virtual void LateUpdate()
    {
        Apply(transform, Vector3.zero);
    }

    public virtual void Cut(Transform dummyTarget, Vector3 dummyCenter)
    {
        float oldHeightSmooth = heightSmoothLag;
        float oldSnapMaxSpeed = snapMaxSpeed;
        float oldSnapSmooth = snapSmoothLag;
        snapMaxSpeed = 10000;
        snapSmoothLag = 0.001f;
        heightSmoothLag = 0.001f;
        snap = true;
        Apply(transform, Vector3.zero);
        heightSmoothLag = oldHeightSmooth;
        snapMaxSpeed = oldSnapMaxSpeed;
        snapSmoothLag = oldSnapSmooth;
    }

    public virtual void SetUpRotation(Vector3 centerPos, Vector3 headPos)
    {
        // Now it's getting hairy. The devil is in the details here, the big issue is jumping of course.
        // * When jumping up and down we don't want to center the guy in screen space.
        //  This is important to give a feel for how high you jump and avoiding large camera movements.
        //   
        // * At the same time we dont want him to ever go out of screen and we want all rotations to be totally smooth.
        //
        // So here is what we will do:
        //
        // 1. We first find the rotation around the y axis. Thus he is always centered on the y-axis
        // 2. When grounded we make him be centered
        // 3. When jumping we keep the camera rotation but rotate the camera to get him back into view if his head is above some threshold
        // 4. When landing we smoothly interpolate towards centering him on screen
        Vector3 cameraPos = cameraTransform.position;
        Vector3 offsetToCenter = centerPos - cameraPos;
        // Generate base rotation only around y-axis
        Quaternion yRotation = Quaternion.LookRotation(new Vector3(offsetToCenter.x, 0, offsetToCenter.z));
        Vector3 relativeOffset = (Vector3.forward * distance) + (Vector3.down * height);
        cameraTransform.rotation = yRotation * Quaternion.LookRotation(relativeOffset);
        // Calculate the projected center position and top position in world space
        Ray centerRay = cameraTransform.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 1));
        Ray topRay = cameraTransform.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, clampHeadPositionScreenSpace, 1));
        Vector3 centerRayPos = centerRay.GetPoint(distance);
        Vector3 topRayPos = topRay.GetPoint(distance);
        float centerToTopAngle = Vector3.Angle(centerRay.direction, topRay.direction);
        float heightToAngle = centerToTopAngle / (centerRayPos.y - topRayPos.y);
        float extraLookAngle = heightToAngle * (centerRayPos.y - centerPos.y);
        if (extraLookAngle < centerToTopAngle)
        {
            extraLookAngle = 0;
        }
        else
        {
            extraLookAngle -= centerToTopAngle;
            cameraTransform.rotation *= Quaternion.Euler(-extraLookAngle, 0, 0);
        }
    }

    public virtual Vector3 GetCenterOffset()
    {
        return centerOffset;
    }

    public ThirdPersonCamera()
    {
        distance = 7f;
        height = 3f;
        angularSmoothLag = 0.3f;
        angularMaxSpeed = 15f;
        heightSmoothLag = 0.3f;
        snapSmoothLag = 0.2f;
        snapMaxSpeed = 720f;
        clampHeadPositionScreenSpace = 0.75f;
        lockCameraTimeout = 0.2f;
        headOffset = Vector3.zero;
        centerOffset = Vector3.zero;
        targetHeight = 100000f;
    }

}