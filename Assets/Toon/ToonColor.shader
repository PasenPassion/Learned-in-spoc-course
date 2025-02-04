Shader "ToonOutline/Color" {
	Properties {
		_Color ("Main Color", Color) = (.5,.5,.5,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { }
	}


	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		Pass 
		{
			Name "BASE"//通道的名字，可以在其他shader中调用
			Cull Off//双面渲染
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			samplerCUBE _ToonShade;
			float4 _MainTex_ST;
			float4 _Color;

			struct appdata 
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				float3 normal : NORMAL;
			};
			
			struct v2f 
			{
				float4 pos : SV_POSITION;
				float2 texcoord : TEXCOORD0;
				float3 cubenormal : TEXCOORD1;
				UNITY_FOG_COORDS(2)
			};

			//顶点函数
			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);//将顶点从模型空间转化到裁剪空间
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);//获取贴图uv
				o.cubenormal = UnityObjectToViewPos(float4(v.normal,0));//获取控制toon的cubemap的法线
				UNITY_TRANSFER_FOG(o,o.pos);
				return o;
			}

			//片元函数
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = _Color * tex2D(_MainTex, i.texcoord);//获取2D贴图上对应uv处的颜色
				fixed4 cube = texCUBE(_ToonShade, i.cubenormal);//获取cubemap上对应法线处的颜色
				fixed4 c = fixed4(2.0f * cube.rgb * col.rgb, col.a);//将cubemap上的颜色*贴图的颜色*2,计算最终颜色,*2是为了控制亮度
				UNITY_APPLY_FOG(i.fogCoord, c);
				return c;
			}
			ENDCG			
		}
	} 

	Fallback "VertexLit"
}
