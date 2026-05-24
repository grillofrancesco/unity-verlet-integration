using UnityEngine;

public class Stick : MonoBehaviour
{
    public float length = 1f;
    public Particle pA, pB;

    public float thickness = 0.4f;

    public void enforceConstraints(){
        // equidistant constraint
        Vector3 d_ab = (pB.p_now - pA.p_now);

        float distance = d_ab.magnitude - length;   // current distance between the two

        // Vector3 displacementOfA = (d_ab * (distance - length)) / (distance * 2); with no masses

        // now with masses
        float totMass = pA.mass + pB.mass;

        Vector3 displacementOfA =  d_ab.normalized * (+distance * pB.mass / totMass);
        Vector3 displacementOfB =  d_ab.normalized * (-distance * pA.mass / totMass);
        
        pA.p_now += displacementOfA;    // currently ignoring the mass (for now equivalent mass)
        pB.p_now += displacementOfB;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        length = (pA.transform.position - pB.transform.position).magnitude;
    }

    private void Update(){
        // midpoint between particles
        transform.position = (pA.p_now + pB.p_now) / 2;

        Vector3 vectDiff = pA.p_now - pB.p_now;
        
        transform.localScale = new Vector3 (thickness, vectDiff.magnitude / 2, thickness);

        transform.rotation = Quaternion.FromToRotation(Vector3.up, vectDiff.normalized);
    }
}
