using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIpointLine : MonoBehaviour
{
    public static UIpointLine ins;
   //public Transform Line;
    public Transform endTr;
    public BezierLineMesh mesh;
    public BezierLineMesh mesh2;
    private void Awake()
    {
        if(ins==null)        ins = this;
    }
    public void setActive(bool active)
    {
        gameObject.SetActive(active);
    }
    public float HandelLen=100;
    public void setPoint(Vector3 start,Vector3 center, Vector3 end)
    {
        start += Vector3.back;
        end += Vector3.back;
        transform.position = end;
        {
            mesh.SetPoint(start, 0);
            var to = (start - end).normalized* HandelLen + center;
            mesh.SetPoint(to , 1);
            mesh.SetPoint(center , 2);
            mesh.FreshLine();
        }
        {
            mesh2.SetPoint(center, 0);
            var to = (end - start).normalized * HandelLen + center;
            mesh2.SetPoint(to , 1);
            mesh2.SetPoint(end, 2);
            mesh2.FreshLine();
        }



        endTr.position = mesh2.GetPoint(0.95f);
        endTr.LookAt(end);
        endTr.position = end;
    }
    public Transform Center;
    public void setPoint(Vector3 start,Vector3 end)
    {
        setPoint(start, Center.position, end);

    }
}
