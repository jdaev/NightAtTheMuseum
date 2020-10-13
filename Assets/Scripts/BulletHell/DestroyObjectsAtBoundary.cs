using UnityEngine;

public class DestroyObjectsAtBoundary : MonoBehaviour {
    private void OnTriggerExit2D(Collider2D other) {
        GameObject obj = other.gameObject;
        GameObject.Destroy(obj);
    }
    
}