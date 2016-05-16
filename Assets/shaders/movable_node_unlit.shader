Shader "Unlit/movable_node_unlit"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_ang ("Angle", Float) = 5.5
		_d ("Distance from Center", Float) = 0.170
		_intensity ("Light Intensity", Float) = .220
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" }
		LOD 100

		Pass
		{
			//ZWrite Off
			 Blend SrcAlpha OneMinusSrcAlpha 
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			//#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _intensity;
			float _ang;
			float _d;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			float4 frag (v2f i) : SV_Target
			{
				float2 t = i.uv-float2(.5,.5);
				float d2 = _d;
				t += float2(cos(_ang)*d2,sin(_ang)*d2);
				float c = _intensity-sqrt(t.x*t.x+t.y*t.y);
				float4 m = tex2D(_MainTex,i.uv);
				//float4 m=_MainTex_ST;
				float3 cm = float3(c, c, c);	
				//return float4(mix(cm, m.xyz, intensity), m.w);
				float alph = m.w;

				return float4((c + m.xyz)*m.a, m.a);
			}
			ENDCG
		}
	}
}
