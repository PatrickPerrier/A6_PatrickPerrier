using UnityEngine;

public class BrickBlock : MonoBehaviour
{
    [SerializeField]
    ParticleSystem bParticle;

    Character playerScript;

    [SerializeField]
    AudioSource source;

    void Start()
    {
        playerScript = GameObject.FindObjectOfType<Character>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerScript.jumpVelocity = 0;
            Instantiate(source, transform.position, transform.rotation);
            Instantiate(bParticle, transform.position, transform.rotation);
            Destroy(gameObject);
        } 
    }
}
