using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private PaddleController _paddle1;
    [SerializeField] private float xPush = 2f;
    [SerializeField] private float yPush = 15f;
    [SerializeField] private float randomFactor;

    [SerializeField] private AudioClip[] _ballSounds;
    private AudioClip _selectedClip;
    private AudioSource _audioSource;

    private Rigidbody2D _rigidbody2D;
    private Transform _transform;


    private Vector2 _paddle2BallVector;
    private bool _hasLaunched = false;

    // Start is called before the first frame update
    void Start()
    {
        _paddle2BallVector = transform.position - _paddle1.transform.position;
        _transform = this.gameObject.transform;

        _audioSource = this.GetComponent<AudioSource>();
        _selectedClip = this.GetComponent<AudioClip>();
        _rigidbody2D = this.GetComponent<Rigidbody2D>();

        _rigidbody2D.simulated = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!_hasLaunched)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_hasLaunched)
        {
            PlayRandomBallSound();

            Vector2 velocityTweak = new Vector2
            (Random.Range(0f, randomFactor),
            Random.Range(0f, randomFactor));

            _rigidbody2D.velocity += velocityTweak;

            float randomAngle = Random.Range(-randomFactor, randomFactor);
            _rigidbody2D.velocity = Quaternion.Euler(0, 0, randomAngle) * _rigidbody2D.velocity;
        }
    }

    private void PlayRandomBallSound()
    {
        int desiredIndex = UnityEngine.Random.Range(0, _ballSounds.Length);
        _selectedClip = _ballSounds[desiredIndex];
        _audioSource.PlayOneShot(_selectedClip);
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _hasLaunched = true;
            _rigidbody2D.velocity = new Vector2(xPush, yPush);
            _rigidbody2D.simulated = true;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(_paddle1.transform.position.x, _paddle1.transform.position.y);
        _transform.position = paddlePos + _paddle2BallVector;
    }
}
