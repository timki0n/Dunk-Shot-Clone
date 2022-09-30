using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projection : MonoBehaviour
{
	private Scene _simulationScene;
	private PhysicsScene2D _physicsScene;

	[SerializeField]
	private BallGhost _ballPrefab;

	[SerializeField]
	private Transform _obstacle;

	[SerializeField]
	private DottedLine _line;

	[SerializeField]
	private int _maxPhysicsIterations;

	[SerializeField]
	private float _physicsFixedTime;

	private Ball _ball;

	private Transform _ghostObstacle;

	private void Start()
	{
		_ball = FindObjectOfType<Ball>();

		CreatePhysicsScene();
		TouchManager.instance.OnSlide += Slide;
		ScoreManager.instance.OnScoreChanged += OnScoreChanged;
	}

	private void OnScoreChanged(int score)
	{
		UpdatePositionObstacle();
	}

	private void Slide(Vector2 velocity, float distance)
	{
		float impulse = GameManager.instance.CalculateImpulse(distance);
		SimulateTrajectory(_ball.transform.position, velocity, impulse);
	}
			

	private void CreatePhysicsScene()
	{
		_simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics2D));
		_physicsScene = _simulationScene.GetPhysicsScene2D();

		_ghostObstacle = Instantiate(_obstacle, _obstacle.position, _obstacle.rotation);
		SceneManager.MoveGameObjectToScene(_ghostObstacle.gameObject, _simulationScene);
	}

	public void SimulateTrajectory(Vector2 pos, Vector2 velocity, float impulse)
	{
		var ghostBall = Instantiate(_ballPrefab, pos, Quaternion.identity);
		SceneManager.MoveGameObjectToScene(ghostBall.gameObject, _simulationScene);

		ghostBall.Shot(velocity, impulse);

		List<Vector2> positions = new List<Vector2>(); 

		for (int i = 0; i < _maxPhysicsIterations; i++)
		{
			_physicsScene.Simulate(_physicsFixedTime);
			positions.Add(ghostBall.transform.position);
		}
		_line.DrawDottedLine(positions);
		Destroy(ghostBall.gameObject);
	}

	private void UpdatePositionObstacle()
	{
		_ghostObstacle.position = _obstacle.position;
	}
}
