// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "MyShader/OutLight" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_Add("Add",Float) = 0.5
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
		//ZWrite Off
		//Cull off
		LOD 200
		pass 
		{
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM


			#pragma vertex vert
			#pragma fragment frag
			#include "unitycg.cginc"

			struct v2f 
			{
				float4 pos:POSITION;
				float3 normal:TEXCOORD1;
				float4 objPos:TEXCOORD2;
			};

			float4 _Color;
			float _Add;

			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.normal = mul(unity_ObjectToWorld, v.normal);
				return o;
			}

			float4 frag(v2f IN):COLOR
			{
				float a = 1- dot(normalize(WorldSpaceViewDir(IN.objPos)),normalize(IN.normal));
				float4 col = _Color;
				col.a = a + _Add;
				return col;
			}

			ENDCG
		}
	}
	FallBack "Diffuse"
}
