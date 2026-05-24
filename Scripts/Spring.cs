using UnityEngine;

public class Spring : MonoBehaviour
{
    public Particle pA, pB;
    
    public float k = 5;                 // elastic constant
    public float restLength; 
    public float thickness = 0.2f;

    void Start(){
        restLength = (pA.p_now - pB.p_now).magnitude;
    }

    public void addForces(){
        float currentDistance = (pA.p_now - pB.p_now).magnitude;
        Vector3 d_AB = (pB.p_now - pA.p_now) / currentDistance; // = .normalized?

        Vector3 forceA = d_AB * ((currentDistance - restLength) * k);

        pA.addForce( forceA);
        pB.addForce(-forceA);   // 3rd law of dynamics
    }

    private void FixedUpdate(){
        // midpoint between particles
        transform.position = (pA.p_now + pB.p_now) / 2;

        Vector3 vectDiff = pA.p_now - pB.p_now;
        
        transform.localScale = new Vector3 (thickness, vectDiff.magnitude / 2, thickness);

        transform.rotation = Quaternion.FromToRotation(Vector3.up, vectDiff.normalized);
    }
}
