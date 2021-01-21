using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TestCurve : MonoBehaviour
{
	private float currentCurvatureX;
	private float newCurvatureX;
	private float currentCurvatureY;
	private float newCurvatureY;

	// Time Interval - time to wait
	[Header("Time Interval - Seconds")]
	public float timeInterval = 30f;

	// Curve Origin - Drag Main Camera into this public slot
	[Header("Curve Origin - Main Camera")]
	public Transform CurveOrigin;

	// Curvature X
	[Range(-500f, 500f)]
	[SerializeField]
	[Header("Curvature X")]
	float x = 0f;

	// Curvature Y
	[Range(-500f, 500f)]
	[SerializeField]
	[Header("Curvature Y")]
	float y = 0f;

	// Falloff Effect
	[Range(0f, 50f)]
	[SerializeField]
	[Header("Falloff Effect")]
	float falloff = 0f;

	private Vector2 bendAmount = Vector2.zero;

	// Global shader property ids
	private int bendAmountId;
	private int bendOriginId;
	private int bendFalloffId;

	void Start()
	{
		bendAmountId = Shader.PropertyToID("_BendAmount");
		bendOriginId = Shader.PropertyToID("_BendOrigin");
		bendFalloffId = Shader.PropertyToID("_BendFalloff");

		// Starting after timeInterval ( wait x amount of seconds ), call
		// changePathCurvature function every timeInterval ( repeat every x amount of seconds )
		//InvokeRepeating("changePathCurvature", timeInterval, timeInterval);
	}

	void Update()
	{
		bendAmount.x = x;
		bendAmount.y = y;

		Shader.SetGlobalVector(bendAmountId, bendAmount);
		Shader.SetGlobalVector(bendOriginId, CurveOrigin.position);
		Shader.SetGlobalFloat(bendFalloffId, falloff);
	}

	void changePathCurvature()
	{
		// x - Get current value of CurvatureX
		currentCurvatureX = x;

		// newCurvatureX = Random within a range of -50 and 50
		newCurvatureX = (Random.Range(-50.0f, 50.0f));

		// x = Lerp currentCurvatureX to newCurvatureX over timeInterval
		x = Mathf.Lerp(currentCurvatureX, newCurvatureX, timeInterval);

		// y - Get current value of CurvatureY
		currentCurvatureY = y;

		// newCurvatureY = Random within a range of -50 and 50
		newCurvatureY = (Random.Range(-50.0f, 50.0f));

		// y = Lerp currentCurvatureY to newCurvatureY over timeInterval
		y = Mathf.Lerp(currentCurvatureY, newCurvatureY, timeInterval);
	}
}