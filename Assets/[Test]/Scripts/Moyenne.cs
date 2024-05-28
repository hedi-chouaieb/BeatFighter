using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Moyenne : MonoBehaviour
{
    public Module[] modules = new Module[]{
        new Module(2, new Matier[]{
                    new Matier(11,12,0,Matier.MatierType.DS)
                    }),
        new Module(3, new Matier[]{
                    new Matier(15,15,16,Matier.MatierType.EXAM),
                    new Matier(11,11,12,Matier.MatierType.EXAM),
                    new Matier(10,15,11,Matier.MatierType.EXAM),
                    }),
        new Module(2, new Matier[]{
                    new Matier(14.5f,15,15,Matier.MatierType.EXAM),
                    new Matier(14.17f,15.33f,15,Matier.MatierType.EXAM),
                    }),
        new Module(2, new Matier[]{
                    new Matier(13f,13,12,Matier.MatierType.EXAM),
                    new Matier(13f,13,12,Matier.MatierType.EXAM),
                    }),
        new Module(3.5f, new Matier[]{
                    new Matier(16,16.5f,17.5f,Matier.MatierType.EXAM),
                    new Matier(11.5f,11.5f,17.5f,Matier.MatierType.EXAM),
                    }),
        new Module(2.5f, new Matier[]{
                    new Matier(11,13,10,Matier.MatierType.DS)
                    })
        };

    private void Start()
    {
        Debug.Log(modules.Sum(m => m.MoyenneModule()) / modules.Sum(m => m.coefficient));
    }


    public float MoyenneFinal(float[] modules, float[] coefficients)
    {
        return modules.Sum() / coefficients.Sum();
    }
    [System.Serializable]
    public struct Module
    {
        public float coefficient;
        public Matier[] matiers;

        public Module(float coefficient, Matier[] matiers)
        {
            this.coefficient = coefficient;
            this.matiers = matiers;
        }

        public float MoyenneModule()
        {
            return coefficient * (matiers.Sum(m => m.Moyenne()) / matiers.Length);
        }
    }
    [System.Serializable]
    public struct Matier
    {
        public float exam1;
        public float exam2;
        public float exam3;
        public MatierType matierType;

        public Matier(float exam1, float exam2, float exam3, MatierType matierType)
        {
            this.exam1 = exam1;
            this.exam2 = exam2;
            this.exam3 = exam3;
            this.matierType = matierType;
        }

        public enum MatierType
        {
            DS, EXAM
        }

        public float Moyenne()
        {
            if (matierType == MatierType.DS)
                return DS(exam1, exam2, exam3);

            if (matierType == MatierType.EXAM)
                return Exam(exam1, exam2, exam3);

            return 0;
        }

        public float DS(float ds1, float ds2, float ds3)
        {
            return ds1 * 0.4f + ds2 * 0.4f + ds3 * 0.2f;
        }

        public float Exam(float ds1, float ds2, float exam)
        {
            return ds1 * 0.2f + ds2 * 0.1f + exam * 0.7f;
        }
    }
}
