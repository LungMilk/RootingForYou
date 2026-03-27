using UnityEngine;

public class anxietyEnter : MonoBehaviour
{
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        animator.SetBool("enterAnxiety", false);
        animator.SetBool("enterAnxiety", false);

    }
    public void AnxietyEnter()
    {
        animator.SetBool("enterAnxiety", true);
    }

    public void AnxietyWalk()
    {
        animator.SetBool("inAnxiety", true);
    }
}
