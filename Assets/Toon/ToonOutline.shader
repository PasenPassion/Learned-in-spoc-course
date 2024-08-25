Shader "ToonOutline/Outline" 
{
	Properties 
	{
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Range (0.002, 0.03)) = 0.005
		_MainTex ("Base (RGB)", 2D) = "white" { }
		_ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { }
	}
	
	CGINCLUDE
	#include "UnityCG.cginc"
	
	struct appdata 
	{
		float4 vertex : POSITION;
		float3 normal : NORMAL;
	};

	struct v2f 
	{
		float4 pos : SV_POSITION;
		UNITY_FOG_COORDS(0)
		fixed4 color : COLOR;
	};
	
	uniform float _Outline;
	uniform float4 _OutlineColor;
	
	//顶点着色器函数
	v2f vert(appdata v) 
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);

		float3 norm   = normalize(mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal));
		float2 offset = TransformViewToProjection(norm.xy);

		#ifdef UNITY_Z_0_FAR_FROM_CLIPSPACE
			o.pos.xy += offset * UNITY_Z_0_FAR_FROM_CLIPSPACE(o.pos.z) * _Outline;
		#else
			o.pos.xy += offset * o.pos.z * _Outline;//核心点：获取边缘位置增加一层偏移，便于描边着色
		#endif
		o.color = _OutlineColor;
		UNITY_TRANSFER_FOG(o,o.pos);
		return o;
	}
	ENDCG

	SubShader 
	{
		//渲染不透明物体
		Tags { "RenderType"="Opaque" }
		UsePass "ToonOutline/Color/BASE"
		Pass 
		{
			Name "OUTLINE"
			Tags { "LightMode" = "Always" }
			Cull Front//剔除正面
			ZWrite On
			ColorMask RGB//对rgb颜色值进行蒙板
			Blend SrcAlpha OneMinusSrcAlpha//混合

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			fixed4 frag(v2f i) : SV_Target
			{
				UNITY_APPLY_FOG(i.fogCoord, i.color);
				return i.color;
			}
			ENDCG
		}
	
	}
	
	Fallback "ToonOutline/Color"
}
