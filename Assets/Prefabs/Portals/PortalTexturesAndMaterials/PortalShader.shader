Shader "Custom/PortalShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.
        _PortalSize("Portal Size", Float) = 0.5
        _PortalRadiusA("Portal Radius A", Range(0,1)) = 0.4
        _PortalRadiusB("Portal Radius B", Range(0,1)) = 0.6
        _PortalCenter("Portal Center", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _PortalSize;
        float _PortalRadiusA;
        float _PortalRadiusB;
        float _PortalCenter;
        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        float PortalOvalShape(float2 uv)
        {
            float x = distance(uv.x, _PortalCenter);
            float y = distance(uv.y, _PortalCenter);
            
            // return step(_PortalSize, pow(x,2) / pow(_PortalRadiusA, 2) + pow(y,2) / pow(_PortalRadiusB, 2));
             return step(pow(x,2) / pow(_PortalRadiusA, 2) + pow(y,2) / pow(_PortalRadiusB, 2), _PortalSize);
        }
        
        
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float oval = PortalOvalShape(IN.uv_MainTex);
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex,  IN.uv_MainTex * oval) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
