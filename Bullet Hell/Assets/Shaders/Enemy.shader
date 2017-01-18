Shader "Hidden/Enemy.shader" {
	Properties{
		_ScalingFactor("Scaling factor", Float) = 1
		_Albedo("Default albedo", Color) = (1.0, 1.0, 1.0, 1.0)
	}
CGINCLUDE
#include "UnityCG.cginc"

struct appdata {
	float4 vertex : POSITION;
	float3 normal : NORMAL;
};

struct v2f {
	float4 pos : POSITION;
	float4 color : COLOR;
};

float _ScalingFactor;
float4 _Albedo;

v2f vert(appdata v) {
	v2f o;
	o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

	half3 pos = mul(o.pos, unity_WorldToObject);
	/*float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
	float2 offset = TransformViewToProjection(norm.xy);
	o.pos.xy += offset * o.pos.z * _Outline;*/
	o.color = _Albedo * (cos(pos.x * pos.y * pos.z * _Time / _ScalingFactor) + sin(pos.x * pos.y * pos.z * (_Time + 50.0f) / _ScalingFactor));
	return o;
}
ENDCG
	

SubShader{
	Tags {"Queue" = "Transparent"}

	Pass{
		Name "MAIN"
		Tags {"LightMode" = "Always"}
		Cull Off
		/*ZWrite Off
		ZTest Always*/

		Blend SrcAlpha OneMinusSrcAlpha
CGPROGRAM
#pragma vertex vert
#pragma fragment frag

half4 frag(v2f i) : COLOR {
	return i.color;
	}
ENDCG
}

//CGPROGRAM
//#pragma surface surf Lambert
//
//		struct Input {
//		float2 uv_NoiseMap;
//		float3 worldPos;
//	};
//
//	static const float PI = 3.14159265f;
//
//	sampler2D _NoiseMap;
//
//	float4 _Albedo;
//	float4 _AltAlbedo;
//
//	void surf(Input IN, inout SurfaceOutput o)
//	{
//		// Noise factor
//		float s = tex2D(_NoiseMap, IN.uv_NoiseMap).r;
//		o.Albedo = (1 - s)*_Albedo + s*_AltAlbedo;
//		//o.Normal = UnpackNormal(tex2D(_NoiseMap, IN.uv_NoiseMap));
//	}
//	ENDCG
}
	FallBack "Diffuse"
}
