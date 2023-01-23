Shader "Hidden/Vignette"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _VignetteTex ("Texture Vignette", 2D) = "black" {}
        _Fill ("Fill", Range(1, 10)) = 1
        _Scale ("Scale", Range(0, 1)) = 1
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _VignetteTex;
            float _Fill;
            float _Scale;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                col *= 1 - tex2D(_VignetteTex, (i.uv - 0.5) / _Fill + 0.5).a * _Scale;

                return col;
            }
            ENDCG
        }
    }
}
