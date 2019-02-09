using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ParticleSystem ps = this.gameObject.GetComponent<ParticleSystem>();
        ParticleSystem.EmissionModule em = ps.emission;
        em.rateOverTime = 50.0f;
	}
}
