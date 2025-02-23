Shader "Custom/ToonShaderHardShadowFixed" {
    Properties {
        _Color ("Base Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Ramp ("Toon Ramp (RGB)", 2D) = "white" {} // ����Ӳ��Ӱ�Ĺ��ս�������
        _OutlineWidth ("Outline Width", Range(0,1)) = 0.1
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _FresnelColor ("Fresnel Color", Color) = (1,1,1,1)
        _FresnelThreshold ("Fresnel Threshold", Range(0,1)) = 0.5 // ������Ӳ����ֵ
        _ShadowColor ("Shadow Color", Color) = (0,0,0,1) // ��Ӱ��ɫ
        _EmissionMap ("Emission Map", 2D) = "black" {} // ������ͼ
        _EmissionColor ("Emission Color", Color) = (1,1,1,1) // ������ɫ
        _EmissionIntensity ("Emission Intensity", Range(0,10)) = 1 // ����ǿ��
        _Roughness ("Roughness", Range(0,1)) = 0.5 // �ֲڶ�
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        // ���� Pass
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
                // ʹ��ģ�Ϳռ�ķ�����չ����
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

        // �� Pass
        CGPROGRAM
        #pragma surface surf Toon fullforwardshadows

        sampler2D _MainTex;
        sampler2D _Ramp; // Ӳ��Ӱ�Ĺ��ս�������
        sampler2D _EmissionMap; // ������ͼ
        float4 _Color;
        float4 _FresnelColor;
        float _FresnelThreshold; // ������Ӳ����ֵ
        float4 _ShadowColor; // ��Ӱ��ɫ
        float4 _EmissionColor; // ������ɫ
        float _EmissionIntensity; // ����ǿ��
        float _Roughness; // �ֲڶ�

        struct Input {
            float2 uv_MainTex;
            float2 uv_EmissionMap; // ������ͼ�� UV ����
            float3 viewDir;
            float3 worldNormal;
            INTERNAL_DATA
        };

        // �Զ������ģ��
        inline fixed4 LightingToon(SurfaceOutput s, fixed3 lightDir, fixed3 viewDir, fixed atten) {
            // �������������
            float NdotL = dot(s.Normal, lightDir);
            // ʹ�ý�������ʵ��Ӳ��Ӱ
            NdotL = tex2D(_Ramp, float2(NdotL * 0.5 + 0.5, 0)).r; // ֻȡ R ͨ��

            // ����߹ⷴ�䣨Blinn-Phong ģ�ͣ�
            float3 halfDir = normalize(lightDir + viewDir);
            float NdotH = saturate(dot(s.Normal, halfDir));
            float specular = pow(NdotH, s.Gloss * 128.0) * s.Specular; // ���ݴֲڶȵ����߹�

            // ���������͸߹�
            fixed4 c;
            c.rgb = s.Albedo * _LightColor0.rgb * NdotL * atten + _LightColor0.rgb * specular;
            c.a = s.Alpha;
            return c;
        }

        void surf (Input IN, inout SurfaceOutput o) {
            // ������ɫ
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;

            // Ӳ�߷�����Ч��
            float fresnel = 1.0 - saturate(dot(normalize(IN.viewDir), WorldNormalVector(IN, o.Normal)));
            fresnel = step(_FresnelThreshold, fresnel); // ʹ�� step ����ʵ��Ӳ��
            o.Emission = _FresnelColor.rgb * fresnel;

            // ������ͼ
            fixed4 emission = tex2D(_EmissionMap, IN.uv_EmissionMap) * _EmissionColor * _EmissionIntensity;
            o.Emission += emission.rgb; // ��������ͼ��Ч�����ӵ��Է�����

            // �ֲڶ�
            o.Specular = 1.0 - _Roughness; // �߹�ǿ�ȣ��ֲڶ�Խ�ͣ��߹�Խǿ��
            o.Gloss = _Roughness; // �߹������̶ȣ��ֲڶ�Խ�ߣ��߹�Խģ����
        }
        ENDCG

        // ��Ӱ Pass
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