using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/NightVision")]
[RequireComponent (typeof (Shader))]

public class NightVision : MonoBehaviour
{
	private Material _material;

	[Header("Shader")] 
	public Shader ImageShader;
	public Shader BlurShader;

	[Header("Night Vision")] 
	public float Exposure = 3f;
	public Color MainColor = Color.white;
	public Texture2D RampTex;

	[Header("Distortion")] 
	public float CubicDistortionCoeff = 0.5f;
	public float DistortionCoeff = -0.15f;

	[Header("Noise")] [Range(0, 1)] 
	public float NoiseIntensity = 0.7f;

	private float VignetteSoftness = 0.03f;
	private float VignetteRadius = 0.41f;

	private int DownsampleRate = 1;

	public Material Material
	{
		get
		{
			if (_material == null)
			{
				_material = new Material(ImageShader);
				_material.hideFlags = HideFlags.HideAndDontSave;
			}
			return _material;
		}
	}

	private void Start()
	{
		if (!SystemInfo.supportsImageEffects || !ImageShader || !Material.shader.isSupported)
		{
			enabled = false;
		}
	}

	private void BlurImage(RenderTexture source, RenderTexture destination)
	{
		var smallScreenWidth = Screen.width/4;
		var smallScreenHeight = Screen.height/4;
		var renderTex = RenderTexture.GetTemporary(smallScreenWidth, smallScreenHeight, 0);
		source.filterMode = FilterMode.Bilinear;
		Graphics.Blit(renderTex, destination);
		RenderTexture.ReleaseTemporary(renderTex);
	}

	public void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Material.SetTexture("_RampTex", RampTex);
		Material.SetFloat("_Intensity", Exposure);
		Material.SetColor("_MainTint", MainColor);
		Material.SetFloat("_NoiseIntensity", NoiseIntensity);
		Material.SetFloat("_NoiseRandomOffset", Random.Range(0.1f, 0.4f));
		Material.SetFloat("_VignetteRadius", VignetteRadius);
		Material.SetFloat("_VignetteSoftness", VignetteSoftness);
		Material.SetFloat("_DistortionCoeff", DistortionCoeff);
		Material.SetFloat("_CubicDistortionCoeff", CubicDistortionCoeff);

		//Color
		var rtPass0 = RenderTexture.GetTemporary(Screen.width, Screen.height, source.depth, source.format);
		Graphics.Blit(source, rtPass0, Material, 0);

		//Noise
		var rtPass1 = RenderTexture.GetTemporary(Screen.width, Screen.height, source.depth, source.format);
		Graphics.Blit(rtPass0, rtPass1, Material, 1);

		//Blur
		var rtPass2 = RenderTexture.GetTemporary(Screen.width, Screen.height, source.depth, source.format);
		Graphics.Blit(rtPass1, rtPass2);

		//Vignette
		var rtPass3 = RenderTexture.GetTemporary(Screen.width, Screen.height, source.depth, source.format);
		Graphics.Blit(rtPass2, rtPass3, Material, 3);

		//Final downsampling of image. Could be integrated in last blit but for better readability
		var rtDownsample = RenderTexture.GetTemporary(Screen.width/DownsampleRate, Screen.height/DownsampleRate,
		source.depth, source.format);
		Graphics.Blit(rtPass3, rtDownsample);

		Graphics.Blit(rtDownsample, destination);
		RenderTexture.ReleaseTemporary(rtPass0);
		RenderTexture.ReleaseTemporary(rtPass1);
		RenderTexture.ReleaseTemporary(rtPass2);
		RenderTexture.ReleaseTemporary(rtPass3);
		RenderTexture.ReleaseTemporary(rtDownsample);
	}

	public void OnDisable()
	{
		if (_material != null)
		{
			DestroyImmediate(_material);
		}
	}
}