Shader "Hidden/Morph" {
	Properties{
		_NoiseTex("Noise texture", 2D) = "white" {}
		_Texture("Texture", 2D) = "white" {}
		_Albedo("Default albedo", Color) = (1.0, 1.0, 1.0, 1.0)
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		CGPROGRAM
#pragma surface surf Lambert vertex:vert fragment:frag
	struct Input {
		float2 uv_NoiseTex;
		float2 uv_Texture;
		float3 worldPos;
	};

	static const float _PI = 3.14159265f;

	sampler2D _Texture;
	sampler2D _NoiseTex;

	float4 _Albedo;

	void vert(inout appdata_full v)
	{
		float t = atan(v.tangent);
		float a = 30;
		float4 noiseTex = tex2Dlod(_NoiseTex, v.texcoord);
		v.vertex.x += v.normal.x*noiseTex.r*cos(a*_Time);
		v.vertex.y += v.normal.y*noiseTex.r*cos(a*_Time - (2*a*_PI/3));
		v.vertex.z += v.normal.z*noiseTex.r*sin(a*_Time - (4*a*_PI/3));
	}

	void surf(Input IN, inout SurfaceOutput o) {
		float4 tex = tex2D(_Texture, IN.uv_Texture);
		o.Albedo = tex;
	}
	ENDCG
	}
		Fallback "Diffuse"
}