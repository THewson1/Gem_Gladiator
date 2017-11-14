// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Flag" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}

		_ZSpeed ("ZSpeed", Range(0, 5.0)) = 1
		_ZFrequency ("ZFrequency", Range(0, 1.3)) = 1
		_ZAmplitude ("ZAmplitude", Range(0, 1)) = 1

		_XSpeed ("XSpeed", Range(0, 5.0)) = 1
		_XFrequency ("XFrequency", Range(0, 1.3)) = 1
		_XAmplitude ("XAmplitude", Range(0, 1)) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		Cull off
		
		Pass {

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			float _ZSpeed;
			float _ZFrequency;
			float _ZAmplitude;

			float _XSpeed;
			float _XFrequency;
			float _XAmplitude;

			v2f vert(appdata_base v)
			{
				v2f o;
				float numberToAdd = cos((v.vertex.y + _Time.y * _ZSpeed) * _ZFrequency) * _ZAmplitude * (v.vertex.y - 5);
				if (numberToAdd > 0 && v.vertex.y < 2)
					v.vertex.x += numberToAdd;
				numberToAdd = cos((v.vertex.y + _Time.y * _XSpeed) * _XFrequency) * _XAmplitude * (v.vertex.y - 5);
				if (v.vertex.y < 2)
					v.vertex.z += numberToAdd;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				return tex2D(_MainTex, i.uv);
			}

			ENDCG

		}
	}
	FallBack "Diffuse"
}