Shader "Custom/Shader_00"
{

    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment


            float4 Vertex
            (float4 v: POSITION): SV_POSITION
            {
                return mul(unity_MatrixMVP, v);
            }

            fixed4 Fragment(): SV_Target
            {
                return float4(1.0, 1.0, 1.0, 1.0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
 