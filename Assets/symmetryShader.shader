Shader "Custom/symmetryShader" {
	Properties {
		[NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		PASS {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			sampler2D _MainTex;

			struct input {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(input v) {
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}

			fixed4 frag(v2f o) : SV_TARGET {
				return tex2D(_MainTex, frac(2*o.uv));
			}
			ENDCG
		}
	}
}
