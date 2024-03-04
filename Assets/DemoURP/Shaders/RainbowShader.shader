Shader "Custom/RainbowShader"
{
Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Speed ("Speed", Float) = 1
    }
    SubShader
    {
        Tags { "RenderPipeline"="UniversalPipeline" }
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
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            fixed4 _Color;
            float _Speed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                float2 pixelCoord = i.uv;

                float time = _Time.y * _Speed;
                fixed4 c = fixed4(sin(time + pixelCoord.x), cos(time + pixelCoord.y), 0.5, 1);
                c *= _Color;

                return c;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
