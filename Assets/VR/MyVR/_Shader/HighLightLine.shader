// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MyShader/高光线条" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_LightTex("高亮贴图",2D) = "white"{}
		_Diffuse ("diffuse",Color) = (1,1,1,1)
		_Specular ("Specular",Color) = (1,1,1,1)
		_Gloss("gloss",Range(1,256)) = 20
		_S("s",Range(0,1)) = 0.5
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		pass
		{
		    Tags { "LightMode" = "ForwardBase"}
			CGPROGRAM
			//#pragma multi_compile_fwdbase
			#pragma vertex vert
			#pragma fragment frag
			#include "unitycg.cginc"
			#include "Lighting.cginc"

			float4 _Color;
			sampler2D _MainTex;
			float4 _Diffuse;
			float4 _Specular;
			float _Gloss;
			float _S;

			struct v2f
			{
				float4 pos : POSITION;
				float3 worldNormal:TEXCOORD0;
				float3 worldPos:TEXCOORD1;
				float2 uv:TEXCOOD2;
			};

			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.worldPos = mul(unity_ObjectToWorld,v.vertex);
				o.worldNormal = mul(unity_ObjectToWorld,v.normal);
				o.uv = v.texcoord;
				return o;
			}

			float4 frag(v2f i):SV_Target
			{
				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
				fixed3 worldNormal = normalize( i.worldNormal);
				fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);
				
				fixed3 tex = tex2D(_MainTex,i.uv);
				fixed3 diffuse = tex.rgb * _LightColor0.rgb * _Diffuse.rgb * saturate(dot(worldNormal,worldLightDir));
				fixed3 reflectDir = normalize(reflect(-worldLightDir,worldNormal));

				fixed3 vireDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPos.xyz);

				fixed3 specular = _LightColor0.rgb * pow(max(0,dot(reflectDir,vireDir)),_Gloss);
				//return float4(_LightColor0.rgb,1);
				return float4(diffuse + specular + ambient * _S,1);
			}
			ENDCG
		}
		pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma vertex vert 
			#pragma fragment frag
			#include "unitycg.cginc"

			struct v2f
			{
				float4 pos : POSITION;
				float2 uv:TEXCOOD2;
			};

			sampler2D _LightTex;
			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;
				return o;
			}

			float4 frag(v2f i):COLOR
			{
				float4 col = tex2D(_LightTex,i.uv);
				return col;
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}
