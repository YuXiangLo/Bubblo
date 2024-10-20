using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float BubbleLifetime = 2f;

    private void Awake()
    {
        Destroy(gameObject, BubbleLifetime);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("Collision detected with " + other.gameObject.name + " on layer " + other.gameObject.layer);
            other.gameObject.GetComponent<IModifyHealth>().TakeDamage(10f);
        }
        Destroy(gameObject);
    }
}