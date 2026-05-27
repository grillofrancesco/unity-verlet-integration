using UnityEngine;

public class PhysicsEngine : MonoBehaviour
{

    // constant gravity acceleration
    public Vector3 G = new Vector3(0, -9.8f,0); 
    
    public Particle[] particles;
    public Stick[] sticks;
    public Spring[] springs;

    public float friction = 0.7f;

    [Header("Boundaries")]
    public Vector3 boundary = new Vector3(20,20,10);

    public bool aquaMode = false;

    public float impulseIntensity = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        particles = Object.FindObjectsByType<Particle>();
        sticks = Object.FindObjectsByType<Stick>();
        springs = Object.FindObjectsByType<Spring>();
    }

    void FixedUpdate(){
        foreach(Particle p in particles){ p.fromUnity(); }

        // add gravity
        foreach(Particle p in particles){ p.addForce(G * p.mass); }
        foreach(Stick s in sticks){ s.addForce(G * s.mass); }
        
        foreach(Spring s in springs){ s.addForces(); }


        foreach(Particle p in particles){ p.dynamicStep(Time.fixedDeltaTime); }

        // enforce ALL constraints. all these cycle are the reason for parallelization
        foreach(Particle p in particles){ p.enforceConstraints(); }
        foreach(Stick s in sticks){ s.enforceConstraints(); }

        foreach(Particle p in particles){ p.toUnity(); }
    }

    public void UpdateImpulseIntensity(float intensity){
        impulseIntensity = intensity;
        foreach(Particle p in particles) p.SetImpulseInstensity(intensity);
    }


    public void SetAquaMode(bool mode){
        aquaMode = mode;
    }
}