Shader "Custom/Shader_01"
{
    Properties {}
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma surface surf
            #pragma fragment  frag

            float4 surf()
            {
            }

            fixed4 frag()
            {
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}