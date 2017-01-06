Shader "Hidden/Force Field.shader" {
	Properties{
	_NoiseMap("Noise texture", 2D) = "white" {}
	_Alpha("Default alpha", Float) = 0.1
	_HitAlpha("Alpha when hit", Float) = 1.0
	}
		SubShader{
		Cull Off
		ZWrite Off
		Tags { 
		"QueueType" = "Transparent" 
		"RenderType" = "Transparent" }
		LOD 200

		CGPROGRAM
#pragma surface surf Lambert alpha

	struct Input {
		float2 uv_NoiseMap;
		float3 worldPos;
	};

	static const float PI = 3.14159265f;

	sampler2D _NoiseMap;

	float _Alpha;
	float _HitAlpha;
	float _Radii[30];
	float3 _Centers[30];
	float4 _Albedo;

	// WIP: Randomness for sparkling
	//float rand()
	//{
	//	float m = 99999999;
	//	float a = 99999990;
	//	float c = 10000000;
	//	_Seed = fmod((a*_Seed + c), m);
	//	return _Seed / 10000000;
	//}

	void surf(Input IN, inout SurfaceOutput o)
	{
		// Noise factor
		half3 noise = tex2D(_NoiseMap, IN.uv_NoiseMap).rgb;

		// Rainbow road style effect
		half3 c = half3(1,1,1); // Start with white
		float mult = 40.0f;
		float timeMult = _Time * mult;
		float position = IN.worldPos.x * unity_WorldToObject;
		c.r = c.r * cos(timeMult + position + noise.r);
		c.b = c.b * cos(timeMult + position + noise.r - (2 * PI * mult / 3));
		c.g = c.g * cos(timeMult + position + noise.r - (4 * PI * mult / 3));
		o.Albedo = c;

		float dN = 0;
		for (int i = 0; i < 30; i++)
		{
			float d = distance(_Centers[i], IN.worldPos);
			// if d less than r, we have a hit; raise alpha
			dN = dN + step(d, _Radii[i]);
		}
		// Constrain to [0,1]
		dN = saturate(dN);
		o.Alpha = _Alpha*(1 - dN) + _HitAlpha*dN;

	}
	ENDCG
	}
		FallBack "Transparent"
}