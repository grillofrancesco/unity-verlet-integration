using UnityEngine;

public class Particle : MonoBehaviour
{   
    /*
        During GameView:
        with these calculations, once a small change is made inside the Scene view, it propagates
        (a small move on an axys is like a push, check)
    
    */

    [SerializeField]
    public Vector3 p_now = new Vector3();
    [SerializeField]
    public Vector3 p_old = new Vector3();
    public float mass = 0.2f;

    public Vector3 force;  // cleaned every frame

    public void enforceConstraints(){
        p_now.y = Mathf.Clamp(p_now.y, 0, 10); // floor constraint
        p_now.x = Mathf.Clamp(p_now.x, -8, +8); // borders
        p_now.z = Mathf.Clamp(p_now.z, -8, +8);
    }

    public void addForce(Vector3 newForce){
        force += newForce;
    }


    public void dynamicStep(float dt){
        float velocityDamp = 0.5f * dt;

        Vector3 acceleration = force / mass;
        // Vector3 p_next = 2 * p_now - p_old + acceleration * dt * dt;
        // equivalently:

        // Default lerp doesn't do extrapolations
        Vector3 p_next = Vector3.LerpUnclamped(p_old, p_now, 2 - velocityDamp) + acceleration * dt * dt;
        
        p_old = p_now;
        p_now = p_next;

        force = new Vector3();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        fromUnity();
        p_old = p_now;
    }

    // Update is called once per RENDERING frame
    void Update(){}

    // both from and to unity just transport pos info from our verlet rewriting to unity variables

    public void toUnity(){
        transform.position = p_now;
    }

    public void fromUnity(){
        p_now = transform.position;
    }

}
