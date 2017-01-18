Shader "Hidden/Glow.shader" {
	Properties{
		_Radius("Glow radius", Range(0, 5)) = 1
		_GlowAlbedo("Glow albedo", Color) = (1.0, 1.0, 1.0, 1.0)
		_Albedo("Object albedo", Color) = (1.0, 1.0, 1.0, 1.0)
		_NoiseMap("Noise texture", 2D) = "white" {}
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

	float _Radius;
	float4 _GlowAlbedo;

	v2f vert(appdata v) {
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

		float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
		float2 offset = TransformViewToProjection(norm.xy);
		o.pos.xy += offset * o.pos.z * _Radius;
		o.color = _GlowAlbedo * abs(v.vertex.y) / _Radius;
		return o;
	}
	ENDCG


		SubShader{
		Tags{ "Queue" = "Transparent" }

		Pass{
		Name "MAIN"
		Tags{ "LightMode" = "Always" }
		Cull Off
		ZWrite Off
		ZTest Always

		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

		half4 frag(v2f i) : COLOR{
		return i.color;
	}
		ENDCG
	}

		CGPROGRAM
		#pragma surface surf Lambert
		
				struct Input {
				float2 uv_NoiseMap;
				float3 worldPos;
			};
		
			static const float PI = 3.14159265f;
		
			sampler2D _NoiseMap;
		
			float4 _Albedo;
			float4 _AltAlbedo;
		
			void surf(Input IN, inout SurfaceOutput o)
			{
				// Noise factor
				float s = tex2D(_NoiseMap, IN.uv_NoiseMap).r;
				o.Albedo = _Albedo;
				//o.Normal = UnpackNormal(tex2D(_NoiseMap, IN.uv_NoiseMap));
			}
			ENDCG
	}
		FallBack "Diffuse"
}
