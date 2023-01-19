Shader "Hidden/PalletePixel"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Pixelization("_Pixelization", Range(1, 32)) = 0
        _Ramps("Count of Ramps of color", int) = 16
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
            float _Pixelization;
            int _Ramps;

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                // Displacing pixels
                uv.x = ceil(uv.x * _ScreenParams.x / _Pixelization) * _Pixelization / _ScreenParams.x;
                uv.y = ceil(uv.y * _ScreenParams.y / _Pixelization) * _Pixelization / _ScreenParams.y;

                

                // color ramping pixels
                fixed4 col = tex2D(_MainTex, uv);

                col = floor(col * (_Ramps - 1) + 0.5) / (_Ramps - 1);

                return col;
            }
            ENDCG
        }
    }
}
