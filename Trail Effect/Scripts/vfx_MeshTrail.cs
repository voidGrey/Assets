using System;
using System.Threading.Tasks;
using UnityEngine;

public class vfx_MeshTrail : MonoBehaviour
{   
    [SerializeField] private Vector3 lastPosition;
    [SerializeField] private float distanceForTrail = .2f;
    [SerializeField] private int trailLifeTime = 200;
    [SerializeField] private Material mat;

    private MeshFilter mainMF;
    private MeshRenderer mainMR;

    void Awake()
    {
        lastPosition = this.transform.position;
        mainMF = this.GetComponent<MeshFilter>();
        mainMR = this.GetComponent<MeshRenderer>();
    }

    async void Update()
    {
        if(Vector3.Distance(lastPosition,this.transform.position) > distanceForTrail)
        {
            await CreateTrailObject();
        }
    }

    private async Task CreateTrailObject()
    {
        GameObject trailObj = new GameObject($"{this.name} + Trail Obj");
        MeshRenderer MR = trailObj.AddComponent<MeshRenderer>();
        MeshFilter MF = trailObj.AddComponent<MeshFilter>();
        MF.mesh = mainMF.mesh;
        MR.material = mat;
        trailObj.transform.position = lastPosition;
        trailObj.transform.localScale = this.transform.localScale;
        lastPosition = this.transform.position;
        await Task.Delay(trailLifeTime);
        Destroy(trailObj);
    }
}
