using UnityEngine;

public class Floor : MonoBehaviour
{
    public Material defaultMat;
    public Material aquaMat;

    public PhysicsEngine physicsEngine;

    bool aquaMode = false;

    private Renderer rend;

    void Start(){
        rend = GetComponent<Renderer>();
    }

    void Update(){
        if (aquaMode != physicsEngine.aquaMode){
            rend.material = (physicsEngine.aquaMode) ? aquaMat : defaultMat;
            aquaMode = physicsEngine.aquaMode;
        }

    }
}
