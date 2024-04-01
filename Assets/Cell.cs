using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public List<Tube> tubes;

    public GameObject input;
    public CellType cellAbility;

    public void updateTube(int tubeCount)
    {
        if (tubeCount == 3)
        {
            return;
        }

        List<bool> tubeExist = new List<bool>() { true, true, true };
        if (tubeCount == 0)
        {
            tubeExist = new List<bool>() { false, false, false };
        }

        if (tubeCount == 1)
        {
            var rand = Random.Range(0, 3);
            tubeExist[rand] = false;
        }

        if (tubeCount == 2)
        {
            
            tubeExist = new List<bool>() { false, false, false };
            var rand = Random.Range(0, 3);
            tubeExist[rand] = true;
        }

        for (int i = tubeExist.Count-1; i >=0 ; i--)
        {
            if (!tubeExist[i])
            {
                Destroy(tubes[i].gameObject);
                tubes.RemoveAt(i);

            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
