using UnityEngine;

public class WallDestory : MonoBehaviour
{

    private int count = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "mazeWall")
        {
            Destroy(other.gameObject);

        }
    }


    private void OnCollisionEnter(Collision collision)
    {

        //Debug.Log("Collision Detected");
        if (collision.gameObject.tag == "mazeWall")
        {
            Destroy(collision.gameObject);

        }

        //if (collision.gameObject.tag == "goalLocation")
        //{

        //}
    }

    void MoveGen2(char dir, int amount)
    {
        count++;
        for (int i = 0; i < amount; i++)
        {
            switch (dir)
            {
                case 'n':
                    transform.position += new Vector3(0.0f, 0.0f, 4.0f);
                    break;
                case 's':
                    transform.position -= new Vector3(0.0f, 0.0f, 2.0f);

                    //targetPos.transform.position -= new Vector3(0.0f, 0.0f, 4.0f * amount);
                    //currentPos.transform.position += (dist.normalized * 50) * Time.deltaTime;
                    //currentPos.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case 'e':
                    transform.position += new Vector3(2.0f, 0.0f, 0.0f);

                    //targetPos.transform.position += new Vector3(4.0f * amount, 0.0f, 0.0f);
                    //currentPos.transform.position += (dist.normalized * 50) * Time.deltaTime;
                    //currentPos.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case 'w':
                    transform.position -= new Vector3(2.0f, 0.0f, 0.0f);

                    //targetPos.transform.position -= new Vector3(4.0f * amount, 0.0f, 0.0f);
                    //currentPos.transform.position += (dist.normalized * 50) * Time.deltaTime;
                    //currentPos.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
            }
        }

    }
}
