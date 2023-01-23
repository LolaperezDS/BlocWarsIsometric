Shader "Hidden/SCreenSpaceReflections"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _WaterDepth("Water depth", 2D) = "white" {}
        _TestVal ("TV", float) = 1

        _CollisionStep("Collision Step", Range(0.001, 0.1)) = 0.006
        _MaxCountOfStepSearch("_MaxCountOfStepSearch", int) = 32
        _CamAngle("Angle of Cam", Range(1, 89)) = 60
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

            float _TestVal;
            int _MaxCountOfStepSearch;
            float _CollisionStep;
            float _CamAngle;

            #define stateLoop 256

            fixed4 gausBlur(v2f v){
                float Pi = 6.28318530718; // Pi*2
    
                // GAUSSIAN BLUR SETTINGS {{{
                float Directions = 16; // BLUR DIRECTIONS (Default 16.0 - More is better but slower)
                float Quality = 6.0; // BLUR QUALITY (Default 4.0 - More is better but slower)
                float Size = 20; // BLUR SIZE (Radius)
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
		                col_out += tex2D(_MainTex, uv+float2(cos(d),sin(d))*Radius*i);
                    }
                }
    
                // Output to screen
                col_out /= Quality * Directions;
                return col_out;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // fixed4 col = pow(tex2D(_CameraDepthTexture, i.uv), _TestVal);
                float depthOfThisPixel = tex2D(_CameraDepthTexture, i.uv).r;
                float angleOfCamToRadians = radians(_CamAngle);

                if (depthOfThisPixel < tex2D(_WaterDepth, i.uv).r)
                {
                    // HERE WATER LAYER
                    float deltaDepth =  - tex2D(_WaterDepth, i.uv).r + tex2D(_WaterDepth, i.uv - float2(0, _CollisionStep)).r;
                    float currentDepth = tex2D(_WaterDepth, i.uv).r;
                    float2 currentUV = i.uv;

                    for (int j = 0; j < stateLoop; j++)
                    {
                        currentUV += float2(0, _CollisionStep);
                        currentDepth += deltaDepth;

                        if (currentDepth <= tex2D(_CameraDepthTexture, currentUV).r + deltaDepth && currentDepth >= tex2D(_CameraDepthTexture, currentUV).r - deltaDepth)
                        {
                            col = tex2D(_MainTex, currentUV) + fixed4(0, 0, 0.2, 1);
                            break;
                        }
                        if (j > _MaxCountOfStepSearch){
                            col += fixed4(0, 0, 0.2, 1);
                            break;
                        }
                    }
                    // col = fixed4(_TestVal, _TestVal, _TestVal, 1);
                }

                return col;
            }
            ENDCG
        }
    }
}
