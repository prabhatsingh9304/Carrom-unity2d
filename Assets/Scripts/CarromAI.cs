using System.Collections;
using UnityEngine;


public class CarromAI : MonoBehaviour
{
    public float moveDelay = 1f;
    public GameManager gameManager;
    public bool isMyTurn;
    private float currentDelay;
    Rigidbody2D rb;

    public void OnEnable()
    {
        gameManager = FindObjectOfType<GameManager>();
        isMyTurn = true;
        currentDelay = 0f;
        rb = GetComponent<Rigidbody2D>();
        gameManager.enemydone = false;
    }

    public void Update()
    {
        if (!isMyTurn)
        {
            StartCoroutine(WaitForTurnToFinish());
            return;
        }

        currentDelay += Time.deltaTime;

        if (currentDelay >= moveDelay)
        {
            MakeAIMove();
            currentDelay = 0f;
            isMyTurn = false;
        }
    }

    public void MakeAIMove()
    {

        int pocketIndex = Random.Range(0, gameManager.pockets.Count);
        int coinIndex = Random.Range(0, gameManager.coins.Count);
        rb.AddForce(new Vector3(gameManager.coinPositions[coinIndex].x - transform.position.x, gameManager.coinPositions[coinIndex].y - transform.position.y, 0) * 100);

    }

    public IEnumerator WaitForTurnToFinish()
    {
        yield return new WaitForSeconds(3f);
        gameManager.enemydone = true;
    }
}
