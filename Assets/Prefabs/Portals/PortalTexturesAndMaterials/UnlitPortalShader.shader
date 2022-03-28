Shader "Unlit/UnlitPortalShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PortalColor("Portal Color" , Color) = (1,1,1,1)
        _PortalSize("Portal Size", Float) = 0.5
        _PortalRadiusA("Portal Radius A", Range(0,1)) = 0.4
        _PortalRadiusB("Portal Radius B", Range(0,1)) = 0.6
        _PortalCenter("Portal Center", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            #include "Shapes.cginc"
            
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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            float _PortalSize;
            float _PortalRadiusA;
            float _PortalRadiusB;
            float _PortalCenter;
            float4 _PortalColor;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                float oval = OvalShape(i.uv, _PortalCenter, _PortalRadiusA, _PortalRadiusB, _PortalSize);
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv * oval) * _PortalColor;
                float toClip = oval == 1 ? 1 : -1;
                clip(toClip);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
