Shader "Hidden/EdgeShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ColorOfEdges("ColorOfEdges", Color) = (0, 0, 0, 1)
        _tresthold("_tresthold", Range(0, 1)) = 0.1
        _Size("size", Range(0, 1)) = 0.05
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
            sampler2D _CameraDepthTexture;
            fixed4 _ColorOfEdges;
            float _tresthold;
            float _Size;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float depthCol = tex2D(_CameraDepthTexture, i.uv).r;

                for (int k = -1; k <= 1; k++){
                    for (int j = -1; j <= 1; j++){
                        if (abs(depthCol - tex2D(_CameraDepthTexture, float2(i.uv.x + k * _Size, i.uv.y + j * _Size)).r) > _tresthold){
                            col = _ColorOfEdges;
                        }
                    }
                }

                return col;
            }
            ENDCG
        }
    }
}
