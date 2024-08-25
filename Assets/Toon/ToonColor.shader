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
			Name "BASE"//ͨ�������֣�����������shader�е���
			Cull Off//˫����Ⱦ
			
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

			//���㺯��
			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);//�������ģ�Ϳռ�ת�����ü��ռ�
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);//��ȡ��ͼuv
				o.cubenormal = UnityObjectToViewPos(float4(v.normal,0));//��ȡ����toon��cubemap�ķ���
				UNITY_TRANSFER_FOG(o,o.pos);
				return o;
			}

			//ƬԪ����
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = _Color * tex2D(_MainTex, i.texcoord);//��ȡ2D��ͼ�϶�Ӧuv������ɫ
				fixed4 cube = texCUBE(_ToonShade, i.cubenormal);//��ȡcubemap�϶�Ӧ���ߴ�����ɫ
				fixed4 c = fixed4(2.0f * cube.rgb * col.rgb, col.a);//��cubemap�ϵ���ɫ*��ͼ����ɫ*2,����������ɫ,*2��Ϊ�˿�������
				UNITY_APPLY_FOG(i.fogCoord, c);
				return c;
			}
			ENDCG			
		}
	} 

	Fallback "VertexLit"
}
