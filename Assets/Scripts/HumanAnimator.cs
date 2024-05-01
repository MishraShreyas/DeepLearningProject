using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAnimator : MonoBehaviour
{
    [SerializeField] string animName;
    Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        StartCoroutine(PlayAnim());
    }

    private IEnumerator PlayAnim()
    {
        // play animation after a random amount of seconds
        yield return new WaitForSeconds(Random.Range(3, 10));
        _anim.Play(animName);
        StartCoroutine(PlayAnim());
    }
}
