using UnityEngine;

public class DeathZone : MonoBehaviour {

	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("PlayerAll") || other.gameObject.layer != LayerMask.NameToLayer("Player1") || other.gameObject.layer != LayerMask.NameToLayer("Player2") || gameObject.tag != "Finish")
        {
            Destroy(GetComponent<BoxCollider>());
            GetComponentInParent<PlayerManager>().KillPlayer();
        }
    }
}
