Shader "Hidden/TrueReflectionShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _WaterDepth("Water depth", 2D) = "white" {}
        _WaterReflection("Water reflection", 2D) = "white" {}

        _WaterColor("Water Color", Color) = (0.4, 0.4, 0.1, 1)

        _WaterDisplacement("Water Displacement", 2D) = "white" {}
        _DisplacementMultiplayer("Water Displacement Power", Range(0, 1)) = 0.3
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
            sampler2D _WaterDepth;
            sampler2D _WaterReflection;
            sampler2D _WaterDisplacement;

            fixed4 _WaterColor;

            float _DisplacementMultiplayer;

            float2 GetDisplacementFromUV(float2 uv){
                fixed4 normal = tex2D(_WaterDisplacement, frac(uv + _Time.x));
                float2 disp = float2(normal.r, normal.g) - 0.5;
                return disp * _DisplacementMultiplayer;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // fixed4 col = pow(tex2D(_CameraDepthTexture, i.uv), _TestVal);
                fixed4 refcol = tex2D(_WaterReflection, float2(i.uv.x, 1 - i.uv.y) + GetDisplacementFromUV(i.uv));
                float waterReflectionDepth;
                float3 wnd;
                DecodeDepthNormal(tex2D(_WaterReflection, float2(i.uv.x, 1 - i.uv.y) + GetDisplacementFromUV(i.uv)), waterReflectionDepth, wnd);
                float depthOfThisPixel = tex2D(_CameraDepthTexture, i.uv).r;

                if (depthOfThisPixel < tex2D(_WaterDepth, i.uv).r)
                {
                    col = tex2D(_MainTex, i.uv + GetDisplacementFromUV(i.uv));
                    if (waterReflectionDepth >= 0.01 && waterReflectionDepth < tex2D(_WaterDepth, i.uv).r)
                    {
                        col = refcol;
                    }
                    else
                    {
                        col += _WaterColor;
                    }
                    

                    // ADD DISPLACEMENT
                }
                return col;
            }
            ENDCG
        }
    }
}
