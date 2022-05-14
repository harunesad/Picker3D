using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    PlatformController platformController;

    bool crash;

    [SerializeField] AudioClip hitSfx;

    void Awake()
    {
        crash = false;
        platformController = GetComponentInParent<PlatformController>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Object"))
        {
            if (!crash)
            {
                platformController.ObjectCrashGround();
                crash = true;
            }
            AudioSource.PlayClipAtPoint(hitSfx, transform.position);
            collision.gameObject.tag = "Untagged";
            platformController.objectCount++;
            Destroy(collision.gameObject, 3f);
        }
    }
}
