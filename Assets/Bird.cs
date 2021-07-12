using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    private Vector3 _initialPosition;
    private bool _birdLaunched;
    private float _timeSittingAround = 0;

    [SerializeField] private float _launchpower = 300;
   

    private void Awake()
    {
        _initialPosition = transform.position;
    }


    //Reset bird to start position if it goes out of window and stops moving after launched
    private void Update()
    {
        GetComponent<LineRenderer>().SetPosition(0, _initialPosition);
        GetComponent<LineRenderer>().SetPosition(1, transform.position);

        if (_birdLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0)
        {
            _timeSittingAround += Time.deltaTime;
        }
        if (transform.position.y > 10 || transform.position.x > 15 || transform.position.x < -15 || transform.position.y < -10
            || _timeSittingAround > 0.5)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }

    private void OnMouseDown()
    {
        GetComponent<LineRenderer>().enabled = true;
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        Vector2 directionToInitialPosition = _initialPosition - transform.position;
        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _launchpower);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        _birdLaunched = true;

        GetComponent<LineRenderer>().enabled = false;
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y);
    }
}
