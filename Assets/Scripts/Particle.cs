using UnityEngine;

public class Particle : MonoBehaviour
{   
    /*
        During GameView:
        with these calculations, once a small change is made inside the Scene view, it propagates
        (a small move on an axys is like a push, check)
    
    */
    private float friction = 0.7f;

    [SerializeField] public Vector3 p_now = new Vector3();
    [SerializeField] public Vector3 p_old = new Vector3();
    public float mass = 0.2f;


    public Vector3 force;  // cleaned every frame

    float impulseIntensity;

    PhysicsEngine physicsEngine;
    Vector3 boundary;
    float yLimit;

    public void enforceConstraints(){
        p_now.y = Mathf.Clamp(p_now.y,      (physicsEngine.aquaMode ? -yLimit : 0),      boundary.y);
        p_now.x = Mathf.Clamp(p_now.x, -boundary.x, +boundary.x);
        p_now.z = Mathf.Clamp(p_now.z, -boundary.z, +boundary.z);
    }

    public void addForce(Vector3 newForce){
        force += newForce;
    }

    public void dynamicStep(float dt){
        float velocityDamp = friction * dt;

        Vector3 acceleration = force / mass;
        // Vector3 p_next = 2 * p_now - p_old + acceleration * dt * dt;
        // equivalently:

        // Default lerp doesn't do extrapolations
        Vector3 p_next = Vector3.LerpUnclamped(p_old, p_now, 2 - velocityDamp) + acceleration * dt * dt;
        
        p_old = p_now;
        p_now = p_next;

        force = new Vector3();
    }

    void Impulse(Vector3 dir){
        p_old += - dir * (impulseIntensity / mass);
    }


    public void SetImpulseInstensity(float intensity){
        impulseIntensity = intensity;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        fromUnity();
        p_old = p_now;
        physicsEngine = GameObject.Find("PhysicsEngine").GetComponent<PhysicsEngine>();
        friction = physicsEngine.friction;
        boundary = physicsEngine.boundary;
        yLimit = boundary.y;

        impulseIntensity = physicsEngine.impulseIntensity;
    }

    // Update is called once per RENDERING frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)) Impulse(new Vector3(0,1,0));
        if (Input.GetKeyDown(KeyCode.UpArrow)) Impulse(new Vector3(0,0,1));
        if (Input.GetKeyDown(KeyCode.DownArrow)) Impulse(new Vector3(0,0,-1));
        if (Input.GetKeyDown(KeyCode.LeftArrow)) Impulse(new Vector3(-1,0,0));
        if (Input.GetKeyDown(KeyCode.RightArrow)) Impulse(new Vector3(1,0,0));
    }

    // both from and to unity just transport pos info from our verlet rewriting to unity variables
    public void toUnity(){
        transform.position = p_now;
    }

    public void fromUnity(){
        p_now = transform.position;
    }

}

