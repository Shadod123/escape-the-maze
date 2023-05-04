using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitoDeLuz : MonoBehaviour
{
    public new Light light;
    public GameObject emissiveObject;
    public AudioSource audioSource;
    public AnimationCurve animationCurve;
    public Color color;
    public float multiplyIntensity = 1.0f;

    public WrapMode wrapMode = WrapMode.PingPong;
    private Material emissiveMaterial;

    void Start()
    {
        this.animationCurve.postWrapMode = this.wrapMode;
        this.emissiveMaterial = emissiveObject.GetComponent<Renderer>().material;
    }

    void Update()
    {
        this.light.color = this.color;
        float value = animationCurve.Evaluate(Time.time);

        this.light.intensity = value;
        this.emissiveMaterial.SetColor("_EmissionColor", this.color * value);   
    }
}

