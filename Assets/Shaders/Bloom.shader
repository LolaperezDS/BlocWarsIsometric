Shader "Hidden/Bloom"
{
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Thresthold("Thresthold", range(0, 1)) = 0.8
        _Glow ("Intensity", Range(0, 100)) = 1
        _SizeBloom("Size", float) = 10
        _Radius("Radius", range(2, 32)) = 16
    }
    SubShader {
        Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
        LOD 100
        Cull Off
        ZWrite On
        Blend SrcAlpha OneMinusSrcAlpha

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            half4 _MainTex_ST;
            half _Glow;
            float _Thresthold;
            float _SizeBloom;
            float _Radius;


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

            
            fixed4 clampOffTexture(float2 uv){
                fixed4 tcol = tex2D(_MainTex, uv) - _Thresthold;
                if (tcol.r < 0 || tcol.b < 0 || tcol.g < 0) {
                    return 0;
                }
                return tcol / (1 - _Thresthold);
            }

            fixed4 gausBlur(v2f v){
                float Pi = 6.28318530718; // Pi*2
    
                // GAUSSIAN BLUR SETTINGS {{{
                float Directions = _Radius; // BLUR DIRECTIONS (Default 16.0 - More is better but slower)
                float Quality = 6.0; // BLUR QUALITY (Default 4.0 - More is better but slower)
                float Size = _SizeBloom; // BLUR SIZE (Radius)
                // GAUSSIAN BLUR SETTINGS }}}
   
                float2 Radius = Size/_ScreenParams.xy;
    
                // Normalized pixel coordinates (from 0 to 1)
                float2 uv = v.uv;
                // Pixel colour
                fixed4 col_out = tex2D(_MainTex, uv);
    
                // Blur calculations
                for( float d=0.0; d<Pi; d+=Pi/Directions)
                {
	                for(float i=1.0/Quality; i<=1.0; i+=1.0/Quality)
                    {
		                // col_out += tex2D(_MainTex, uv+float2(cos(d),sin(d))*Radius*i);		
                        col_out += clampOffTexture(uv+float2(cos(d),sin(d))*Radius*i);
                    }
                }
    
                // Output to screen
                col_out /= Quality * Directions;
                return col_out;
            }


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                return tex2D(_MainTex, i.uv) + gausBlur(i) / _Glow;
            }
            ENDCG
        }
    }
}