using UnityEngine;

public class Stick : MonoBehaviour
{
    PhysicsEngine physicsEngine;
    public float length;
    public Particle pA, pB;

    public float density = 7.87f;

    public float thickness = 0.4f;
    float volume;

    public void enforceConstraints(){
        equidistanceConstraint();

        if (physicsEngine.aquaMode) buoyancyConstraint();
    }

    private void equidistanceConstraint(){
        // equidistant constraint
        Vector3 d_ab = (pB.p_now - pA.p_now);

        float distance = d_ab.magnitude - length;   // current distance between the two

        // Vector3 displacementOfA = (d_ab * (distance - length)) / (distance * 2); with no masses

        // float stickMass = volume * density; ?
        float totMass = pA.mass + pB.mass; // + stickMass;

        Vector3 displacementOfA =  d_ab.normalized * (+distance * pB.mass / totMass);
        Vector3 displacementOfB =  d_ab.normalized * (-distance * pA.mass / totMass);
        
        pA.p_now += displacementOfA;    // currently ignoring the mass (for now equivalent mass)
        pB.p_now += displacementOfB;
    }

    private void buoyancyConstraint(){
        Particle min = (pA.p_now.y < pb.p_now.y) ? pA : pB;
        Particle max = (pA.p_now.y < pb.p_now.y) ? pB : pA;

        // not submerged
        if (min.p_now.y > 0) return;

        float waterDensity = 1f;
        float massMadeOfWater = volume * waterDensity;

        float particleHeightDiff =  max.p_now.y - min.p_now.y;

        float submergedQuantity = min.p_now.y / particleHeightDiff;

        float buoyancyForce = submergedPart * massMadeOfWater * g;

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        length = (pA.transform.position - pB.transform.position).magnitude;
        physicsEngine = GameObject.Find("PhysicsEngine").GetComponent<PhysicsEngine>();

        volume = 3.14f * thickness*thickness * length;
    }

    private void Update(){
        // midpoint between particles
        transform.position = (pA.p_now + pB.p_now) / 2;

        Vector3 vectDiff = pA.p_now - pB.p_now;
        
        transform.localScale = new Vector3 (thickness, vectDiff.magnitude / 2, thickness);

        transform.rotation = Quaternion.FromToRotation(Vector3.up, vectDiff.normalized);
    }
}
