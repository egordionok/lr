using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLogic : MonoBehaviour
{
    float timer = 0f;
    bool movable = true;
    public GameObject rig;
    GameLogic gameLogic;
    
    // Start is called before the first frame update
    void Start()
    {
        gameLogic = FindObjectOfType<GameLogic>();
    }

    void RegisterBlock()
    {
        foreach(Transform block in rig.transform)
        {
            gameLogic.grid[Mathf.FloorToInt(block.position.x), Mathf.FloorToInt(block.position.y)] = block;
        }
    }

    bool checkValid()
    {
        foreach(Transform block in rig.transform)
        {
            
            if (block.transform.position.x >= GameLogic.width ||
                block.transform.position.x < 0 ||
                block.transform.position.y < 0)
            {
                return false;
            }
            if (block.position.y < GameLogic.height && gameLogic.grid[Mathf.FloorToInt(block.position.x), Mathf.FloorToInt(block.position.y)] != null)
            {
                return false;
            }
          
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if (movable)
        {
            //Update timer
            timer += 1 * Time.deltaTime;
            //Drop
            if ((Input.GetKey(KeyCode.DownArrow) && (timer > GameLogic.quickDropTime)))
            {
                gameObject.transform.position -= new Vector3(0, 1, 0);
                timer = 0;
                if (!checkValid())
                {
                    movable = false;
                    gameObject.transform.position += new Vector3(0, 1, 0);
                    RegisterBlock();
                    gameLogic.ClearLines();
                    gameLogic.SpawnBlock();
                }
            }
            else if (timer > GameLogic.dropTime)
            {
                gameObject.transform.position -= new Vector3(0, 1, 0);
                timer = 0;
                if (!checkValid())
                {
                    movable = false;
                    gameObject.transform.position += new Vector3(0, 1, 0);
                    RegisterBlock();
                    gameLogic.ClearLines();
                    gameLogic.SpawnBlock();
                }
            }

            //

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                gameObject.transform.position -= new Vector3(1, 0, 0);
                if (!checkValid())
                {
                    gameObject.transform.position += new Vector3(1, 0, 0);
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                gameObject.transform.position += new Vector3(1, 0, 0);
                if (!checkValid())
                {
                    movable = false;
                    gameObject.transform.position -= new Vector3(1, 0, 0);
                }
            }

            //Rotation
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rig.transform.eulerAngles -= new Vector3(0, 0, 90);
                if (!checkValid())
                {
                    rig.transform.eulerAngles += new Vector3(0, 0, 90);
                }
            }


        }
    }
}
