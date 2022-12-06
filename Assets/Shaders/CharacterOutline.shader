Shader "MyShaders/CharacterToonShader"
{
    Properties
    {
        [NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
        _ShadowStrength ("Shadow Strength", Range(0, 1)) = 0.5
        [Header(Outline Front Settings)]
        [Space]
        _OutlineColor ("Color", Color) = (0, 0, 0, 0)
        _OutlineWidth ("Width", Range(0, 0.1)) = 0.01
        _OutlineFrontOpacity ("Opacity", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Geometry"
            "Queue"="Geometry"
            "LightMode"="ForwardBase"
            "PassFlags"="OnlyDirectional"
        }

        // Main Pass
        Pass
        {
            Stencil
            {
                Ref 1
                Comp Always
                Pass Replace
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment pixel

            #include "UnityCG.cginc"

            struct v2f
            {
                half2 uv : TEXCOORD0;
                half4 vertex : SV_POSITION;
                half3 worldNormal : NORMAL;
            };

            sampler2D _MainTex;
            half _ShadowStrength;

            v2f vert (appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 pixel (v2f i) : SV_Target
            {
                float3 normal = i.worldNormal;
                float NdotL = dot(_WorldSpaceLightPos0, normal);
                float lightIntensity = lerp(1, _ShadowStrength, step(NdotL, 0));
                fixed4 col = tex2D(_MainTex, i.uv);
                // col.rgb *= 1 - col.rgb;
                col *= lightIntensity;
                return col;
            }
            ENDCG
        }

        // Outline Pass
        Pass
        {
            Tags
            {
                "RenderType"="Transparent"
                "Queue"="Transparent"
            }

            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off

            Stencil
            {
                Ref 1
                Comp Greater
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment pixel

            #include "UnityCG.cginc"

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 textcoord: TEXTCOORD0;
            };

            sampler2D _OutlineTex;
            half4 _OutlineTex_ST;
            half _OutlineWidth;
            half4 _OutlineColor;
            half _OutlineFrontOpacity;

            v2f vert (appdata_base v)
            {
                v2f o;
                v.vertex.xyz += normalize(v.normal) * _OutlineWidth;
                // v.vertex.xyz += v.vertex * _OutlineWidth;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.textcoord = v.texcoord;
                return o;
            }

            fixed4 pixel (v2f i) : SV_Target
            {
                _OutlineColor.a = _OutlineFrontOpacity;
                return _OutlineColor;
            }
            ENDCG
        }

        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}
