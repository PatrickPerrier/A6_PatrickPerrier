using UnityEngine;

public class CoinBlock : MonoBehaviour
{
    [SerializeField]
    Material shader1;

    [SerializeField]
    Material shader2;

    Renderer rend;

    Character playerScript;

    [SerializeField]
    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        playerScript = GameObject.FindObjectOfType<Character>();
        
        rend = GetComponent<Renderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            source.Play();
            playerScript.jumpVelocity = 0;
            rend.material = shader2;
        }
        else
        {
            rend.material = shader1;
        }
    }
}
