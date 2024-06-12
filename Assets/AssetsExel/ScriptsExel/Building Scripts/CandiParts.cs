
using UnityEngine;

public class CandiParts : MonoBehaviour
{
    int currentHealth;
    [SerializeField] Sprite[] candiSprites;
    Animator anim;
    // Update is called once per frame
    private void Awake() {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        currentHealth = GetComponentInParent<Candi>().currentHealth;
        anim.SetInteger("health",currentHealth);
    }
}
