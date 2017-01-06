Shader "Hidden/Force Field.shader" {
	Properties{
	_ColorMap("Color Map", 2D) = "white" {}
	_Center("Collision coords", Vector) = (1000,1000,1000,0)
		_Radius("Effect radius", Range(0, 1)) = 0.1
		_Albedo("Default albedo", Color) = (1.0, 1.0, 1.0, 1.0)
		_Alpha("Default alpha", Float) = 0.1
	}
		SubShader{
		Tags{ "RenderType" = "Transparent" "QueueType" = "Transparent"}
		LOD 200

		CGPROGRAM
#pragma surface surf Lambert alpha

		struct Input {
		float2 uv_ColorMap;
		float3 worldPos;
	};

	static const float PI = 3.14159265f;

	sampler2D _ColorMap;

	float _Radius;
	float _Alpha;
	float3 _Center;
	float4 _Albedo;

	void surf(Input IN, inout SurfaceOutput o)
	{
		half3 c = tex2D(_ColorMap, IN.uv_ColorMap).rgb;
		float mult = 200.0f;
		float timeMult = _Time * mult;
		float position =  IN.worldPos.x * unity_WorldToObject;
		c.r = c.r * cos(timeMult + position);
		c.b = c.b * cos(timeMult + position - (2 * PI * mult / 3));
		c.g = c.g * cos(timeMult + position - (4 * PI * mult / 3));
		o.Albedo = c;
		o.Alpha = _Alpha;
	}
	ENDCG
	}
		FallBack "Transparent"
}