using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class CameraPostProcessing : MonoBehaviour
{
    [SerializeField] private List<Material> materials;
    public List<Material> PostProcessingMaterials => materials;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        RenderTexture r = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
        Graphics.CopyTexture(source, r);
        foreach (Material material in materials)
        {
            if (material != null)
            {
                Graphics.Blit(r, material);
                Graphics.CopyTexture(RenderTexture.active, r);
            }
        }
        Graphics.Blit(r, destination);
        RenderTexture.ReleaseTemporary(r);
    }
}
