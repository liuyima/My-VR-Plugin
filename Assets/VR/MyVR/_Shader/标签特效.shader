// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MyShader/标签特效" {
	Properties{
		_blurSizeXY("BlurSizeXY", Range(0,10)) = 2
		_MainTex("图片",2D) = "White"{}
		_EffectsTex("特效图片",2D) = "White"{}
		_Color("颜色",Color) = (1,1,1,1)
		_EffectsColor("特效颜色",Color) = (1,1,1,1)
		_Range("范围",Range(0,1)) = 0.3
		_Speed("速度",Float) = 2
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest ("ZTest", Float) = 0
	}
		SubShader{
		// Draw ourselves after all opaque geometry
		Tags{ "Queue" = "Overlay" "RenderType" = "Transparent"}

		//Cull off
		ZTest [_ZTest]
		ZWrite off
		// Grab the screen behind the object into _GrabTexture
		GrabPass{}
		// Render the object with the texture generated above
		Pass{

			Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
#pragma debug
#pragma vertex vert
#pragma fragment frag
#pragma target 3.0
#include "unitycg.cginc"
		sampler2D _GrabTexture : register(s0);
		sampler2D _EffectsTex;
		sampler2D _MainTex;
		float _Range;
		float _blurSizeXY;
		float _Speed;
		float4 _Color;
		float4 _EffectsColor;
		struct data {
		float4 vertex : POSITION;
		float3 normal : NORMAL;
	};

	struct v2f {
		float4 position : POSITION;
		float4 screenPos : TEXCOORD0;
		float4 uv : TEXCOORD1;
	};

	v2f vert(appdata_base i) {
		v2f o;
		o.position = UnityObjectToClipPos(i.vertex);
		o.screenPos = o.position;
		o.uv = i.texcoord;
		return o;
	}

	half4 frag(v2f i) : COLOR
	{
		float2 screenPos = i.screenPos.xy / i.screenPos.w;
		float depth = _blurSizeXY*0.0005;
		screenPos.x = (screenPos.x + 1) * 0.5;
		screenPos.y = 1 - (screenPos.y + 1) * 0.5;
		half4 sum = half4(0.0h,0.0h,0.0h,0.0h);
		sum += tex2D(_GrabTexture, float2(screenPos.x - 5.0 * depth, screenPos.y + 5.0 * depth)) * 0.025;
		sum += tex2D(_GrabTexture, float2(screenPos.x + 5.0 * depth, screenPos.y - 5.0 * depth)) * 0.025;

		sum += tex2D(_GrabTexture, float2(screenPos.x - 4.0 * depth, screenPos.y + 4.0 * depth)) * 0.05;
		sum += tex2D(_GrabTexture, float2(screenPos.x + 4.0 * depth, screenPos.y - 4.0 * depth)) * 0.05;

		sum += tex2D(_GrabTexture, float2(screenPos.x - 3.0 * depth, screenPos.y + 3.0 * depth)) * 0.09;
		sum += tex2D(_GrabTexture, float2(screenPos.x + 3.0 * depth, screenPos.y - 3.0 * depth)) * 0.09;

		sum += tex2D(_GrabTexture, float2(screenPos.x - 2.0 * depth, screenPos.y + 2.0 * depth)) * 0.12;
		sum += tex2D(_GrabTexture, float2(screenPos.x + 2.0 * depth, screenPos.y - 2.0 * depth)) * 0.12;

		sum += tex2D(_GrabTexture, float2(screenPos.x - 1.0 * depth, screenPos.y + 1.0 * depth)) *  0.15;
		sum += tex2D(_GrabTexture, float2(screenPos.x + 1.0 * depth, screenPos.y - 1.0 * depth)) *  0.15;

		sum += tex2D(_GrabTexture, screenPos - 5.0 * depth) * 0.025;
		sum += tex2D(_GrabTexture, screenPos - 4.0 * depth) * 0.05;
		sum += tex2D(_GrabTexture, screenPos - 3.0 * depth) * 0.09;
		sum += tex2D(_GrabTexture, screenPos - 2.0 * depth) * 0.12;
		sum += tex2D(_GrabTexture, screenPos - 1.0 * depth) * 0.15;
		sum += tex2D(_GrabTexture, screenPos) * 0.16;
		sum += tex2D(_GrabTexture, screenPos + 5.0 * depth) * 0.15;
		sum += tex2D(_GrabTexture, screenPos + 4.0 * depth) * 0.12;
		sum += tex2D(_GrabTexture, screenPos + 3.0 * depth) * 0.09;
		sum += tex2D(_GrabTexture, screenPos + 2.0 * depth) * 0.05;
		sum += tex2D(_GrabTexture, screenPos + 1.0 * depth) * 0.025;

		float4 ec = tex2D(_EffectsTex, i.uv);
		float a = tex2D(_MainTex,i.uv).a;
		float l = _Time * _Speed;
		l %= (1 + _Range * 2);
		l -= _Range;
		float4 color = (sum*0.5) * _Color;
		float ler = clamp(abs(ec.a - l) / _Range, 0, 1);
		float4 _ec = lerp(_EffectsColor, color, _EffectsColor.a);
		return float4(lerp(_ec, color, ler).rgb,a);
	}
		ENDCG
	}
	}
		Fallback Off
}