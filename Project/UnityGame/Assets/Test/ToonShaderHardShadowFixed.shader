Shader "Custom/ToonShaderHardShadowFixed" {
    Properties {
        _Color ("Base Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Ramp ("Toon Ramp (RGB)", 2D) = "white" {} // 用于硬阴影的光照渐变纹理
        _OutlineWidth ("Outline Width", Range(0,1)) = 0.1
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _FresnelColor ("Fresnel Color", Color) = (1,1,1,1)
        _FresnelThreshold ("Fresnel Threshold", Range(0,1)) = 0.5 // 菲涅尔硬边阈值
        _ShadowColor ("Shadow Color", Color) = (0,0,0,1) // 阴影颜色
        _EmissionMap ("Emission Map", 2D) = "black" {} // 发光贴图
        _EmissionColor ("Emission Color", Color) = (1,1,1,1) // 发光颜色
        _EmissionIntensity ("Emission Intensity", Range(0,10)) = 1 // 发光强度
        _Roughness ("Roughness", Range(0,1)) = 0.5 // 粗糙度
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        // 轮廓 Pass
        Pass {
            Name "OUTLINE"
            Tags { "LightMode"="Always" }
            Cull Front
            ZWrite On
            ColorMask RGB

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float _OutlineWidth;
            fixed4 _OutlineColor;

            struct appdata {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f {
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata v) {
                v2f o;
                // 使用模型空间的法线扩展顶点
                float3 norm = normalize(v.normal);
                float4 pos = UnityObjectToClipPos(v.vertex + norm * _OutlineWidth);
                o.pos = pos;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                return _OutlineColor;
            }
            ENDCG
        }

        // 主 Pass
        CGPROGRAM
        #pragma surface surf Toon fullforwardshadows

        sampler2D _MainTex;
        sampler2D _Ramp; // 硬阴影的光照渐变纹理
        sampler2D _EmissionMap; // 发光贴图
        float4 _Color;
        float4 _FresnelColor;
        float _FresnelThreshold; // 菲涅尔硬边阈值
        float4 _ShadowColor; // 阴影颜色
        float4 _EmissionColor; // 发光颜色
        float _EmissionIntensity; // 发光强度
        float _Roughness; // 粗糙度

        struct Input {
            float2 uv_MainTex;
            float2 uv_EmissionMap; // 发光贴图的 UV 坐标
            float3 viewDir;
            float3 worldNormal;
            INTERNAL_DATA
        };

        // 自定义光照模型
        inline fixed4 LightingToon(SurfaceOutput s, fixed3 lightDir, fixed3 viewDir, fixed atten) {
            // 计算漫反射光照
            float NdotL = dot(s.Normal, lightDir);
            // 使用渐变纹理实现硬阴影
            NdotL = tex2D(_Ramp, float2(NdotL * 0.5 + 0.5, 0)).r; // 只取 R 通道

            // 计算高光反射（Blinn-Phong 模型）
            float3 halfDir = normalize(lightDir + viewDir);
            float NdotH = saturate(dot(s.Normal, halfDir));
            float specular = pow(NdotH, s.Gloss * 128.0) * s.Specular; // 根据粗糙度调整高光

            // 结合漫反射和高光
            fixed4 c;
            c.rgb = s.Albedo * _LightColor0.rgb * NdotL * atten + _LightColor0.rgb * specular;
            c.a = s.Alpha;
            return c;
        }

        void surf (Input IN, inout SurfaceOutput o) {
            // 基础颜色
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;

            // 硬边菲涅尔效果
            float fresnel = 1.0 - saturate(dot(normalize(IN.viewDir), WorldNormalVector(IN, o.Normal)));
            fresnel = step(_FresnelThreshold, fresnel); // 使用 step 函数实现硬边
            o.Emission = _FresnelColor.rgb * fresnel;

            // 发光贴图
            fixed4 emission = tex2D(_EmissionMap, IN.uv_EmissionMap) * _EmissionColor * _EmissionIntensity;
            o.Emission += emission.rgb; // 将发光贴图的效果叠加到自发光上

            // 粗糙度
            o.Specular = 1.0 - _Roughness; // 高光强度（粗糙度越低，高光越强）
            o.Gloss = _Roughness; // 高光锐利程度（粗糙度越高，高光越模糊）
        }
        ENDCG

        // 阴影 Pass
        Pass {
            Name "ShadowCaster"
            Tags { "LightMode"="ShadowCaster" }
            ZWrite On
            ZTest LEqual
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_shadowcaster
            #include "UnityCG.cginc"

            struct v2f {
                V2F_SHADOW_CASTER;
            };

            v2f vert(appdata_base v) {
                v2f o;
                TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}