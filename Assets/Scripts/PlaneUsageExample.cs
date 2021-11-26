using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class PlaneUsageExample : MonoBehaviour
{
	public GameObject objectToSlice;
	public Material crossMat;
	public Transform planeTransform;

	private void Start()
	{
		StartCoroutine("MyCoRoutine");
	}

	public SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
	{
		// slice the provided object using the transforms of this object
		return obj.Slice(transform.position, transform.up, crossSectionMaterial);
	}

	void MySlice()
	{
		SlicedHull hull = objectToSlice.Slice(planeTransform.position, Vector3.left, crossMat);
		if (hull != null)
		{
			hull.CreateLowerHull(objectToSlice, crossMat);
			hull.CreateUpperHull(objectToSlice, crossMat);

			objectToSlice.SetActive(false);
		}
	}
	IEnumerator MyCoRoutine()
	{
		yield return new WaitForSeconds(2);
		MySlice();
	}
}