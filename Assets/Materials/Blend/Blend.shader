Shader "Custom/ColorBurnShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _BlendTex ("Blend (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 texcoord : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _BlendTex;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }

            half4 ColorBurn(half4 base, half4 blend)
            {
                half4 result;
                result.r = (blend.r == 0.0) ? blend.r : max(1.0 - ((1.0 - base.r) / blend.r), 0.0);
                result.g = (blend.g == 0.0) ? blend.g : max(1.0 - ((1.0 - base.g) / blend.g), 0.0);
                result.b = (blend.b == 0.0) ? blend.b : max(1.0 - ((1.0 - base.b) / blend.b), 0.0);
                result.a = base.a;
                return result;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 baseColor = tex2D(_MainTex, i.texcoord);
                half4 blendColor = tex2D(_BlendTex, i.texcoord);
                return ColorBurn(baseColor, blendColor);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
