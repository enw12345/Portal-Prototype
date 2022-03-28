Shader "Unlit/PortalRingShdaer"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PortalShimmerTexture("Portal Shimmer Texture",2D) = "white"{}
        
        _PortalRingColor("Portal Ring Color" , Color) = (1,1,1,1)
        _PortalRingOuterSize("Portal Ring Outer Size", Float) = 0.6
        _PortalRingInnerSize("Portal Ring Inner Size", Float) = 0.5
        _PortalRingRadiusA("Portal Ring Radius A", Range(0,1)) = 0.4
        _PortalRingRadiusB("Portal Ring Radius B", Range(0,1)) = 0.6
        _PortalRingCenter("Portal Ring Center", Range(0,1)) = 0.5
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
            
            sampler2D _PortalShimmerTexture;
            float4 _PortalShimmerTexture_ST;
            
            float _PortalRingOuterSize;
            float _PortalRingInnerSize;
            float _PortalRingRadiusA;
            float _PortalRingRadiusB;
            float _PortalRingCenter;
            float4 _PortalRingColor;
            
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
                float inner = OvalShape(i.uv, _PortalRingCenter, _PortalRingRadiusA, _PortalRingRadiusB, _PortalRingInnerSize);
                float outer = OvalRingShape(i.uv, _PortalRingCenter, _PortalRingRadiusA, _PortalRingRadiusB);
                
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) + tex2D(_PortalShimmerTexture, i.uv) * _PortalRingColor;
                float toClip = (inner == 1) ? -1 : 1;
                if(outer > _PortalRingOuterSize )
                {
                    toClip = -1;
                }
                clip(toClip);
                
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
