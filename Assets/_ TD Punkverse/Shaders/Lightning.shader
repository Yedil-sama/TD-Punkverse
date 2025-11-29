Shader "Custom/URP/LightningArcs"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (0.1, 0.1, 0.2, 1)
        _LightningColor ("Main Lightning", Color) = (0.4, 0.6, 1, 1)
        _ArcColor ("Arc Color", Color) = (1, 1, 1, 1)

        _NoiseScale ("Noise Scale", Float) = 3.0
        _LightningThreshold ("Lightning Threshold", Range(0,1)) = 0.4
        _Intensity ("Lightning Intensity", Float) = 6.0
        _Speed ("Scroll Speed", Float) = 1.0

        _ArcIntensity ("Arc Intensity", Float) = 2.0
        _ArcWidth ("Arc Width", Range(0.001, 0.2)) = 0.03
        _ArcSpeed ("Arc Jump Speed", Float) = 1.5

        _MainTex("Noise Texture", 2D) = "white" {}
        _ArcNoise("Arc Noise", 2D) = "gray" {}
    }

    SubShader
    {
        Tags { "RenderPipeline"="UniversalRenderPipeline" }
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }

        Pass
        {
            Tags{"LightMode"="UniversalForward"}
            Blend SrcAlpha OneMinusSrcAlpha

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 normalWS : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                float2 sphereUV : TEXCOORD2;
            };

            sampler2D _MainTex;
            sampler2D _ArcNoise;

            float4 _BaseColor;
            float4 _LightningColor;
            float4 _ArcColor;

            float _NoiseScale;
            float _LightningThreshold;
            float _Intensity;
            float _Speed;

            float _ArcIntensity;
            float _ArcWidth;
            float _ArcSpeed;

            // Spherical UV projection
            float2 Spherify(float3 n)
            {
                float2 uv;
                uv.x = atan2(n.z, n.x) / (2.0 * 3.14159265) + 0.5;
                uv.y = asin(n.y) / 3.14159265 + 0.5;
                return uv;
            }

            // Voronoi cell function for arc cracks
            float voronoi(float2 uv)
            {
                float2 g = floor(uv);
                float2 f = frac(uv);

                float dist = 10.0;

                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        float2 offset = float2(x, y);
                        float2 cell = g + offset;

                        float2 r = f - offset - frac(sin(dot(cell, float2(12.9898,78.233))) * 43758.5453);
                        float d = dot(r, r);
                        dist = min(dist, d);
                    }
                }
                return sqrt(dist);
            }

            Varyings vert (Attributes IN)
            {
                Varyings OUT;

                float3 worldPos = TransformObjectToWorld(IN.positionOS.xyz);

                OUT.positionHCS = TransformWorldToHClip(worldPos);
                OUT.worldPos = worldPos;
                OUT.normalWS = normalize(TransformObjectToWorldNormal(IN.normalOS));
                OUT.sphereUV = Spherify(OUT.normalWS);

                return OUT;
            }

            float4 frag(Varyings IN) : SV_Target
            {
                float t = _Time.y;

                float3 baseCol = _BaseColor.rgb;
                float alpha = _BaseColor.a;

                // Base lightning noise
                float mainNoise = tex2D(_MainTex, IN.sphereUV * _NoiseScale + t * _Speed).r;
                float boltMask = smoothstep(_LightningThreshold, 1.0, mainNoise);
                float3 lightningGlow = _LightningColor.rgb * boltMask * _Intensity;

                // ARCS — moving cracks using Voronoi
                float2 arcUV = IN.sphereUV * 8.0;

                // animate Voronoi space
                arcUV += t * _ArcSpeed;

                float vor = voronoi(arcUV);

                // arc mask = thin crack line
                float arc = smoothstep(_ArcWidth * 2.0, _ArcWidth, vor);

                // distort arcs using noise
                float noise = tex2D(_ArcNoise, IN.sphereUV * 5.0 + t).r;
                arc *= (0.8 + noise * 0.8);

                // arc flash pulses
                float flash = (sin(t * 6.0 + noise * 10.0) * 0.5 + 0.5);
                arc *= flash;

                float3 arcColor = _ArcColor.rgb * arc * _ArcIntensity;

                // combine all
                float3 finalCol = baseCol + lightningGlow + arcColor;
                alpha += boltMask * 0.5 + arc * 0.8;

                return float4(finalCol, saturate(alpha));
            }

            ENDHLSL
        }
    }
}
