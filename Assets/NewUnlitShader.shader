Shader "Unlit/NewUnlitShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float2 s = 3*i.uv;
				float2 ip = trunc(s);
				float2 fr = frac(s);
				float2 ch = ip % 2;
				float2 c = lerp(fr, 1-fr, ch);
				// sample the texture
				fixed4 col = tex2D(_MainTex, c);
				return col;
			}
			ENDCG
		}
	}
}
