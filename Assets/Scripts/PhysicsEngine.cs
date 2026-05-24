using UnityEngine;

public class PhysicsEngine : MonoBehaviour
{

    // constant gravity acceleration
    Vector3 G = new Vector3(0, -9.8f,0); 
    
    public Particle[] particles;
    public Stick[] sticks;
    public Spring[] springs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        particles = Object.FindObjectsByType<Particle>();
        sticks = Object.FindObjectsByType<Stick>();
        springs = Object.FindObjectsByType<Spring>();
    }

    void FixedUpdate(){
        foreach(Particle p in particles){ p.fromUnity(); }

        // add forces
        foreach(Particle p in particles){ p.addForce(G * p.mass); }
        
        foreach(Spring s in springs){ s.addForces(); }

        // EXERCISE: Add Impulses (with buttons maybe)

        foreach(Particle p in particles){ p.dynamicStep(Time.fixedDeltaTime); }

        // enforce ALL constraints. all these cycle are the reason for parallelization
        foreach(Particle p in particles){ p.enforceConstraints(); }
        foreach(Stick s in sticks){ s.enforceConstraints(); }

        foreach(Particle p in particles){ p.toUnity(); }
    }
}
