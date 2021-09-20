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
    public GameObject alienC;
    public GameObject SpawnerA;
    public GameObject spawnerB;
    public GameObject spawnerC;
    private int alienAAmount;
    private int alienBAmount;
    private int alienCAmount;
    private SkeletonGenerator skeletonGenerator;


    void Start()
    {
        alienAAmount = 10;
        alienBAmount = 10;
        alienCAmount = 10;
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

            freeSpace.RemoveAt(index);
            if( alienAAmount > 0)
            {
                Instantiate(alienA, new Vector2(position.row, position.column), Quaternion.identity);
                alienAAmount--;
            }
            else if (alienBAmount > 0)
            {
                Instantiate(alienB, new Vector2(position.row, position.column), Quaternion.identity);
                alienBAmount--;
            }
            else 
            {
                Instantiate(alienC, new Vector2(position.row, position.column), Quaternion.identity);
                alienCAmount--;
            }
        }

    }
}
