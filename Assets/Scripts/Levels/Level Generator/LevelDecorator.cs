using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDecorator : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject meteorite;
    public GameObject meteorBin;
    public GameObject alienA;
    public GameObject alienB;
    public GameObject enemyBin;
    public GameObject player;
    private int alienAAmount;
    private int alienBAmount;
    private int alienCAmount;
    private SkeletonGenerator skeletonGenerator;


    void Start()
    {
        alienAAmount = 15;
        alienBAmount = 15;
        skeletonGenerator = new SkeletonGenerator();

        AddAsteroids();


    }

    private void AddAsteroids()
    {
        List<Point> freeSpace = new List<Point>();
        for(int row = 0; row < 15 * skeletonGenerator.size; row++)
        {
            for(int column = 0; column < 100 * skeletonGenerator.size; column++)
            {
                if (this.skeletonGenerator.matrix[row, column] == 1)
                {
                    GameObject meteor = Instantiate(meteorite, new Vector3(row, column, 0), Quaternion.identity);
                    meteor.transform.SetParent(meteorBin.transform);
                    meteor.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                }
                else
                {
                    freeSpace.Add(new Point(row, column));
                }
            }
        }
        
        System.Random rand = new System.Random();
        int index;
        while (freeSpace.Count > 0 && (alienAAmount > 0 || alienBAmount > 0 || alienCAmount > 0)) { 

            index = rand.Next(freeSpace.Count);
            Point position = freeSpace[index];
            GameObject createdObject;   
            freeSpace.RemoveAt(index);
            if( alienAAmount > 0)
            {
                createdObject = Instantiate(alienA, new Vector2(position.row, position.column), Quaternion.identity);
                alienAAmount--;
            }
            else
            {
                createdObject = Instantiate(alienB, new Vector2(position.row, position.column), Quaternion.identity);
                alienBAmount--;
            }
            createdObject.transform.parent = enemyBin.transform;
        }

    }
}
