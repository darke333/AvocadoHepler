using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tea_controller : MonoBehaviour
{
    public bool[] Inventar = new bool[12];
    public bool Go;
    public bool AutoStop;
    public bool TrueIngridient;
    public bool FalseIngridient;
    public bool NeadIngridient;
    int k;

       // Update is called once per frame
    void Update()
    {
        
        if (AutoStop)
        {
            Go = false;
        }

        if (Go)
        {
            TrueIngridient = false;
            FalseIngridient = false;
            NeadIngridient = false;
        }

        k = 0;
        for (int i=0; i<12; i++)
        {
            if (Inventar[i])
                k++;
        }
        if (k == 12)
        {
            AutoStop = true;
        }
    }

    public void Sugar()
    {
        if (!Inventar[0]&&Go)
        {
            Inventar[0] = true;
            Go = false;
            TrueIngridient = true;
        }
    }

    public void Honey()
    {
        if (!Inventar[1] && Go)
        {
            Inventar[1] = true;
            Go = false;
            TrueIngridient = true;
        }
    }

    public void Jam()
    {
        if (!Inventar[2] && Go)
        {
            Inventar[2] = true;
            Go = false;
            TrueIngridient = true;
        }
    }

    public void Milk()
    {
        if (!Inventar[3] && Go)
        {
            Inventar[3] = true;
            Go = false;
            TrueIngridient = true;
        }
    }

    public void Lemon()
    {
        if (!Inventar[4] && Go)
        {
            Inventar[4] = true;
            Go = false;
            TrueIngridient = true;
        }
    }

    public void Tea()
    {
        if (!Inventar[5] && Go)
        {
            Inventar[5] = true;
            Go = false;
            NeadIngridient = true;
        }
    }

    public void Water()
    {
        if (!Inventar[6] && Go)
        {
            Inventar[6] = true;
            Go = false;
            NeadIngridient = true;
        }
    }

    public void Salt()
    {
        if (!Inventar[7] && Go)
        {
            Inventar[7] = true;
            Go = false;
            FalseIngridient = true;
        }
    }

    public void Pepper()
    {
        if (!Inventar[8] && Go)
        {
            Inventar[8] = true;
            Go = false;
            FalseIngridient = true;
        }
    }

    public void Peas()
    {
        if (!Inventar[9] && Go)
        {
            Inventar[9] = true;
            Go = false;
            FalseIngridient = true;
        }
    }

    public void Flour()
    {
        if (!Inventar[10] && Go)
        {
            Inventar[10] = true;
            Go = false;
            FalseIngridient = true;
        }
    }
    public void Pasta()
    {
        if (!Inventar[11] && Go)
        {
            Inventar[11] = true;
            Go = false;
            FalseIngridient = true;
        }
    }

}
