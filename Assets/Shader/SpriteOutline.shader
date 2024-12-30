Shader "Custom/ImprovedSpriteOutline"
{
    Properties
    {
        _MainTex("Sprite Texture", 2D) = "white" {}
        _OutlineColor("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth("Outline Width", Float) = 0.005
    }

    SubShader
    {
        Tags { "RenderType" = "Transparent" }
        LOD 200
        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            Name "OUTLINE"

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

            sampler2D _MainTex;
            float4 _OutlineColor;
            float _OutlineWidth;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv);

                // E�er sprite tamamen �effafsa (alpha = 0)
                if (col.a == 0.0)
                {
                    float alpha = 0.0;

                    // �evredeki 8 pikseli kontrol et
                    alpha += tex2D(_MainTex, i.uv + float2(-_OutlineWidth, 0)).a; // Sol
                    alpha += tex2D(_MainTex, i.uv + float2(_OutlineWidth, 0)).a;  // Sa�
                    alpha += tex2D(_MainTex, i.uv + float2(0, _OutlineWidth)).a;  // Yukar�
                    alpha += tex2D(_MainTex, i.uv + float2(0, -_OutlineWidth)).a; // A�a��

                    alpha += tex2D(_MainTex, i.uv + float2(-_OutlineWidth, _OutlineWidth)).a;  // Sol �st
                    alpha += tex2D(_MainTex, i.uv + float2(_OutlineWidth, _OutlineWidth)).a;   // Sa� �st
                    alpha += tex2D(_MainTex, i.uv + float2(-_OutlineWidth, -_OutlineWidth)).a; // Sol Alt
                    alpha += tex2D(_MainTex, i.uv + float2(_OutlineWidth, -_OutlineWidth)).a;  // Sa� Alt

                    // Alpha threshold ekle
                    alpha = saturate(alpha); // Alpha'y� 0-1 aras�nda tut

                    // E�er �evrede alpha varsa outline �iz
                    if (alpha > 0.1)
                    {
                        return _OutlineColor;
                    }
                }

                // Orijinal sprite rengi d�nd�r
                return col;
            }
            ENDCG
        }
    }
}
