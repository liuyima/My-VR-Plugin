// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MyShader/mask" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MaskTex("alpha texture",2D) = "white"{}
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
				float4 mask_uv:TEXCOORD1;
				float4 uv:TEXCOORD3;
			};

			sampler2D _MainTex;
			sampler2D _MaskTex;
			float4 _MainTex_ST;
			float4 _MaskTex_ST;

			float4 _Color;

			v2f vert(appdata_base v)
			{
				v2f o;
				o.position = UnityObjectToClipPos(v.vertex);
				o.localPos = v.vertex;
				o.mask_uv = v.texcoord*float4( _MaskTex_ST.xy,1,1)+float4( _MaskTex_ST.zw,0,0);
				o.uv = v.texcoord*float4( _MainTex_ST.xy,1,1)+float4( _MainTex_ST.zw,0,0);
				return o;
			}

			float4 frag(v2f IN):COLOR
			{
				float4 color = tex2D(_MainTex,IN.uv);
				float alpha = tex2D(_MaskTex, IN.mask_uv).a;
				color.a *= alpha*_Color.a;
				color.rgb=_Color.rgb;
				return color;
			}
			ENDCG
		}
	}
	//FallBack "Diffuse"
}
