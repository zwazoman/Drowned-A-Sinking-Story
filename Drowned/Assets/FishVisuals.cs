using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishVisuals : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer _mesh;
    public float targetFatness;
    float actualFatness;
    float offset;

    [SerializeField] float a, b;

    private void Update()
    {
        actualFatness = Mathf.MoveTowards(actualFatness, targetFatness+offset, Time.deltaTime*a);
        _mesh.SetBlendShapeWeight(0,actualFatness);
    }

    public IEnumerator SetOffset(float force,float duration)
    {
        offset = force;
        yield return new WaitForSeconds(duration);
        offset = 0;
    }
}
