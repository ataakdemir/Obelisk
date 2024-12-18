Shader "Custom/ImprovedSpriteOutline"
{
    Properties
    {
        _MainTex("Sprite Texture", 2D) = "white" {}
        _OutlineColor("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth("Outline Width", Float) = 0.01
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

                // 8 yönlü piksel kontrolü
                float alpha = col.a;
                alpha += tex2D(_MainTex, i.uv + float2(_OutlineWidth, 0)).a;     // Sað
                alpha += tex2D(_MainTex, i.uv - float2(_OutlineWidth, 0)).a;     // Sol
                alpha += tex2D(_MainTex, i.uv + float2(0, _OutlineWidth)).a;     // Yukarý
                alpha += tex2D(_MainTex, i.uv - float2(0, _OutlineWidth)).a;     // Aþaðý

                alpha += tex2D(_MainTex, i.uv + float2(_OutlineWidth, _OutlineWidth)).a; // Sað üst
                alpha += tex2D(_MainTex, i.uv + float2(_OutlineWidth, -_OutlineWidth)).a; // Sað alt
                alpha += tex2D(_MainTex, i.uv - float2(_OutlineWidth, _OutlineWidth)).a; // Sol üst
                alpha += tex2D(_MainTex, i.uv - float2(_OutlineWidth, -_OutlineWidth)).a; // Sol alt

                // Eðer çevre pikseller alpha deðerine sahipse ve bu piksel þeffafsa, outline uygula
                if (alpha > 0.0 && col.a == 0.0)
                {
                    return _OutlineColor;
                }

                return col;
            }
            ENDCG
        }
    }
}
