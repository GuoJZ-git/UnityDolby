using UnityEngine;

public class _Line: MonoBehaviour
{
  public float fTime = 0.0f;

  public Vector3 ptFrom;
  public Vector3 ptTo;

  public Vector3 length;

  public void InitLine(Vector3 posFrom, Vector3 posTo)
  {
    ptFrom = posFrom;
    ptTo = posTo;

    length = ptTo - ptFrom;
    fTime = 0.0f;
  }


  private void Update()
  {
    fTime += Time.deltaTime *0.05f;

    Vector3 pos = ptFrom + length * fTime ;// (1.0f - 1.0f / (fTime + 1.0f));
    transform.position = Camera.main.transform.TransformPoint(pos);

  }
}
