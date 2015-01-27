using UnityEngine;
using System.Collections;

public static class RendererExtensions
{
    // Comprueba si el renderer objetivo está dentro del volumen de visión guardado en el CameraManager
    // a través de la función RecalculateFrustumPlanes()
	public static bool EP_IsVisibleFromCurrentCamera(this Renderer renderer)
	{
		return GeometryUtility.TestPlanesAABB(CameraManager.Instance.GetViewFrustumPlanes(), renderer.bounds);
	}
}

