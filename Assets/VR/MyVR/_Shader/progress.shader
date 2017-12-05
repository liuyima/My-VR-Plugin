// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MyShader/progress" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_AlphaTex("alpha texture",2D) = "white"{}
		_Progress("progress",Range(0,1)) =1 
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
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members uv)
//#pragma exclude_renderers d3d11

			#pragma vertex vert
			#pragma fragment frag
			#include "unitycg.cginc"

			struct v2f
			{
				float4 position:POSITION;
				float4 localPos:TEXCOORD2;
				float4 uv:TEXCOORD1;
			};

			sampler2D _MainTex;
			sampler2D _AlphaTex;

			float4 _Color;
			float _Progress;

			v2f vert(appdata_base v)
			{
				v2f o;
				o.position = UnityObjectToClipPos(v.vertex);
				o.localPos = v.vertex;
				o.uv = v.texcoord;
				return o;
			}

			float4 frag(v2f IN):COLOR
			{
				float4 color = tex2D(_MainTex,IN.uv);
				float alpha = tex2D(_AlphaTex, IN.uv).a;
				alpha -= _Progress;
				float d = alpha / abs(alpha);
				d = clamp(d, 0, 1);
				/*if (alpha < _Progress)
				{
					alpha = 0;
				}
				else if(alpha != 0)
				{
					alpha = 1;
				}*/
				color.a = d;
				return color;
			}
			ENDCG
		}
	}
	//FallBack "Diffuse"
}
