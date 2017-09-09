using UnityEngine;

public class DeathZone : MonoBehaviour {

	private void OnTriggerEnter(Collider other)
    {
        if (
            ((transform.parent.gameObject.layer == LayerMask.NameToLayer("Player1") && other.gameObject.layer != LayerMask.NameToLayer("Traversable2"))
            || (transform.parent.gameObject.layer == LayerMask.NameToLayer("Player2") && other.gameObject.layer != LayerMask.NameToLayer("Traversable1"))
            || (gameObject.tag != "Finish" && !LayerMask.LayerToName(other.gameObject.layer).Contains("Traversable"))) && !other.isTrigger
            )
        {
            //Debug.Log(LayerMask.LayerToName(transform.parent.gameObject.layer));
            //Debug.Log(LayerMask.LayerToName(other.gameObject.layer));
            Destroy(GetComponent<BoxCollider>());
            GetComponentInParent<PlayerManager>().KillPlayer();
        }
    }
}
