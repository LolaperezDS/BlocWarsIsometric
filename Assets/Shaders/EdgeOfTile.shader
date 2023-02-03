Shader "Unlit/EdgeOfTile"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        [HDR]_MainColor("main color", Color) = (0.5, 0.5, 0.5, 1)
        _CountOctaves("CountOctaves", int) = 4
        _Size("size", Range(0, 1)) = 0.1
        _Frequency("Main freq", float) = 5
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" "ForceNoShadowCasting" = "True" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            int _CountOctaves;
            fixed4 _MainColor;
            float _Size;
            float _Frequency;


            #define MAX_COUNT_OF_OCTAVES 8

            bool IsColored(float2 uv, float frequency){
                return abs(uv.y + sin(frequency * (frequency * _Time[0] + uv.x)) * frequency / (_CountOctaves * _CountOctaves) - 0.5) <= _Size;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = fixed4(1, 1, 1, 0);

                for (int j = 0; j < MAX_COUNT_OF_OCTAVES; j++){
                    if (j > _CountOctaves){
                        break;
                    }
                    if (IsColored(i.uv, _Frequency * (j + 1) * (j + 1))){
                        col = _MainColor;
                    }
                }
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
