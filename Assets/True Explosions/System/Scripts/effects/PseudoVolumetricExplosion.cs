using UnityEngine;
using System.Collections;

public class PseudoVolumetricExplosion : MonoBehaviour {
	public float loopDuration = 1;
	public float loopOffset = 0;
	public bool randomizeLoopOffset = true;
	public AnimationCurve scale = AnimationCurve.EaseInOut(0, 0.2f, 1, 2);
	public AnimationCurve minRange = AnimationCurve.Linear(0, 0, 1, 0.5f);
	public AnimationCurve maxRange = AnimationCurve.Linear(0, 0.2f, 1, 1);
	public AnimationCurve clip = AnimationCurve.Linear(0.5f, 0.7f, 1, 0.5f);
	public float timeScale = 1;
	public Renderer m_ObjectRenderer;

	private Vector3 endScale;
	private float startTime;
	private static System.Timers.Timer aTimer;


	void Start() {
		loopDuration *= timeScale;
		loopOffset *= timeScale;
		if (randomizeLoopOffset) {
			loopOffset = Random.Range(0, loopDuration);
		}
		endScale = transform.localScale;
		startTime = Time.time;
	}

	void Update() {
		float timeFromBegin = Time.time - startTime;
		float pos = (loopOffset + timeFromBegin) / loopDuration;
		float r = Mathf.Sin((pos) * (2 * Mathf.PI)) * 0.5f + 0.25f;
		float g = Mathf.Sin((pos + 0.33333333f) * 2 * Mathf.PI) * 0.5f + 0.25f;
		float b = Mathf.Sin((pos + 0.66666667f) * 2 * Mathf.PI) * 0.5f + 0.25f;
		float correction = 1 / (r + g + b);
		r *= correction;
		g *= correction;
		b *= correction;

		m_ObjectRenderer = GetComponent<Renderer>();

		m_ObjectRenderer.material.SetVector("_ChannelFactor", new Vector4(r, g, b, 0));

		float scaleFactor = scale.Evaluate(timeFromBegin / timeScale);
		transform.localScale = endScale * scaleFactor;

		float beginRange = minRange.Evaluate(timeFromBegin / timeScale);
		float endRange = maxRange.Evaluate(timeFromBegin / timeScale);
		float clipVal = clip.Evaluate(timeFromBegin / timeScale);
		m_ObjectRenderer.material.SetVector("_Range", new Vector4(beginRange, endRange, 0, 0));
		m_ObjectRenderer.material.SetFloat("_ClipRange", clipVal);

		StartCoroutine(ExampleCoroutine());
	}

	IEnumerator ExampleCoroutine()
    {
		yield return new WaitForSeconds(2);
		Destroy(gameObject);
    }
}
