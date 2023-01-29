Shader "Custom/HDR_standart"
{
    Properties
    {
        [HDR]_Color ("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Toon finalcolor:colout

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _Color;

        fixed4 LightingToon (SurfaceOutput s, fixed3 lightDir, fixed atten)
        {
             fixed4 c;
             c.rgb = _Color;
             c.a = 1;
             return c;
        }


        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void colout(Input IN, SurfaceOutput o, inout fixed4 color) {
            color.rgb = _Color;
            color.a = 1;
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            o.Albedo = _Color.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
