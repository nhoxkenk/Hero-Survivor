using UnityEngine;

public class ExperienceOrb : Collectable
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController playerController))
        {
            Debug.Log("Collected");
            OnCollect();
            Destroy(gameObject);
        }
    }
}
