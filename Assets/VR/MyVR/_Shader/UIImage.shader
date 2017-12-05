// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MyShader/UIImage" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Color("Color",Color) = (1,1,1,1)
	}
	SubShader {
		Tags{ "RenderType" = "Overlay" "Queue" = "Transparent" }
		Cull off
		ZTest Always
		ZWrite off
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
				float4 uv:TEXCOORD1;
			};
			sampler2D _MainTex;
			float4 _Color;
			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;
				return o;
			}

			float4 frag(v2f IN):COLOR
			{
				float4 color = tex2D(_MainTex,IN.uv);
				float a = color.a;
				color = color * _Color;
				//color.a = a;
				return color;
			}

			ENDCG
		}	
	}
	//FallBack "Diffuse"
}
