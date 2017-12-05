// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MyShader/fade" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Progress("progress",Range(0,1)) =1 
		_range("range",Range(0,1)) = 0.2
		_startPoint("start",FLOAT) = 0.2
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
		ZWrite off
		Cull off
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
				float4 position:POSITION;
				float4 localPos:TEXCOORD1;
			};

			float4 _Color;
			float _Progress;
			float _range;
			float _startPoint;

			v2f vert(appdata_base v)
			{
				v2f o;
				o.position = UnityObjectToClipPos(v.vertex);
				o.localPos = v.vertex;
				return o;
			}

			float4 frag(v2f v):COLOR
			{
				float4 color = _Color;
				float y = v.localPos.y;
				
				float a = (_Progress - _range - y - _startPoint) / _range;
				a = clamp(a, 0, 1);
				color.a = a;
				return color;
			}
			ENDCG
		}
	}
	//FallBack "Diffuse"
}
