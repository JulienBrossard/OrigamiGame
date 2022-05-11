// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader ""
{
	Properties
	{
		_Disp("Disp", 2D) = "white" {}
		[HDR][Header(Bicolor)][HideIf(_NOBICOLOR_ON)]_ColorA("ColorA", Color) = (0,0,0,1)
		[HDR][HideIf(_NOBICOLOR_ON)]_ColorB("ColorB", Color) = (1,1,1,1)
		[HideIf(_NOBICOLOR_ON)]_BicolorThreshold("BicolorThreshold", Float) = 0
		[HideIf(_NOBICOLOR_ON)]_BicolorSmoothness("BicolorSmoothness", Float) = 1
		[Toggle]_Bicolor_OneMinus("Bicolor_OneMinus", Float) = 0
		_Col("Col", 2D) = "white" {}
		_ColB("ColB", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,0)
		_ColorSh("ColorSh", Color) = (1,1,1,0)
		[Header(Alpha Sharp)]_AlphaThreshold("AlphaThreshold", Float) = 0
		_AlphaSmoothness("AlphaSmoothness", Float) = 1
		[Toggle]_AlphaSharp_OneMinus("AlphaSharp_OneMinus", Float) = 0
		_DistanceDebutDegrade("DistanceDebutDegrade", Float) = 1
		_DegradeVitesse("DegradeVitesse", Float) = 1
		_Float2("Float 1", Float) = 1
		_Float3("Float 1", Float) = 1
		_Displacement("Displacement", Float) = 0
		_Normal("Normal", Float) = 0
		[Header(TimeStep)]_TimeStep("TimeStep", Float) = 60
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
			float3 worldNormal;
		};

		struct SurfaceOutputCustomLightingCustom
		{
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half Metallic;
			half Smoothness;
			half Occlusion;
			half Alpha;
			Input SurfInput;
			UnityGIInput GIData;
		};

		uniform float _Displacement;
		uniform sampler2D _Disp;
		uniform float _TimeStep;
		uniform float4 _Disp_ST;
		uniform float4 _ColorA;
		uniform float4 _ColorB;
		uniform float _BicolorThreshold;
		uniform float _BicolorSmoothness;
		uniform sampler2D _Col;
		uniform float4 _Col_ST;
		uniform sampler2D _ColB;
		uniform float _Bicolor_OneMinus;
		uniform float4 _Color;
		uniform float4 _ColorSh;
		uniform float _AlphaThreshold;
		uniform float _AlphaSmoothness;
		uniform float _Normal;
		uniform float _AlphaSharp_OneMinus;
		uniform float _Float2;
		uniform float _Float3;
		uniform float _DistanceDebutDegrade;
		uniform float _DegradeVitesse;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertexNormal = v.normal.xyz;
			float2 panner1_g56 = ( ( floor( ( _Time.y * _TimeStep ) ) / _TimeStep ) * _Disp_ST.zw + ( v.texcoord.xy * _Disp_ST.xy ));
			float4 tex2DNode18 = tex2Dlod( _Disp, float4( ( float2( 0,0 ) + panner1_g56 ), 0, 0.0) );
			v.vertex.xyz += ( ase_vertexNormal * _Displacement * tex2DNode18.r );
			v.vertex.w = 1;
		}

		inline half4 LightingStandardCustomLighting( inout SurfaceOutputCustomLightingCustom s, half3 viewDir, UnityGI gi )
		{
			UnityGIInput data = s.GIData;
			Input i = s.SurfInput;
			half4 c = 0;
			float3 ase_worldPos = i.worldPos;
			float clampResult56 = clamp( pow( ( distance( ase_worldPos , _WorldSpaceCameraPos ) / _DistanceDebutDegrade ) , _DegradeVitesse ) , 0.0 , 1.0 );
			float lerpResult57 = lerp( _Float2 , _Float3 , clampResult56);
			c.rgb = 0;
			c.a = lerpResult57;
			return c;
		}

		inline void LightingStandardCustomLighting_GI( inout SurfaceOutputCustomLightingCustom s, UnityGIInput data, inout UnityGI gi )
		{
			s.GIData = data;
		}

		void surf( Input i , inout SurfaceOutputCustomLightingCustom o )
		{
			o.SurfInput = i;
			float2 panner1_g54 = ( ( floor( ( _Time.y * _TimeStep ) ) / _TimeStep ) * _Col_ST.zw + ( i.uv_texcoord * _Col_ST.xy ));
			float2 temp_output_26_0 = ( float2( 0,0 ) + panner1_g54 );
			float lerpResult36 = lerp( tex2D( _Col, temp_output_26_0 ).r , tex2D( _ColB, temp_output_26_0 ).r , 0.35);
			float smoothstepResult5_g58 = smoothstep( _BicolorThreshold , ( _BicolorThreshold + _BicolorSmoothness ) , lerpResult36);
			float lerpResult12_g58 = lerp( smoothstepResult5_g58 , ( 1.0 - smoothstepResult5_g58 ) , _Bicolor_OneMinus);
			float4 lerpResult4_g58 = lerp( _ColorA , _ColorB , lerpResult12_g58);
			float temp_output_7_0_g57 = ( _AlphaThreshold + 0.0 );
			float3 ase_worldPos = i.worldPos;
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_worldlightDir = 0;
			#else //aseld
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			#endif //aseld
			float2 panner1_g56 = ( ( floor( ( _Time.y * _TimeStep ) ) / _TimeStep ) * _Disp_ST.zw + ( i.uv_texcoord * _Disp_ST.xy ));
			float4 tex2DNode18 = tex2D( _Disp, ( float2( 0,0 ) + panner1_g56 ) );
			float temp_output_37_0 = ( tex2DNode18.r + lerpResult36 );
			float3 temp_cast_0 = (temp_output_37_0).xxx;
			float3 ase_worldNormal = i.worldNormal;
			float3 lerpResult38 = lerp( temp_cast_0 , ase_worldNormal , _Normal);
			float3 normalizeResult16 = normalize( lerpResult38 );
			float dotResult8 = dot( ase_worldlightDir , normalizeResult16 );
			float temp_output_1_0_g57 = dotResult8;
			float lerpResult9_g57 = lerp( temp_output_1_0_g57 , ( 1.0 - temp_output_1_0_g57 ) , _AlphaSharp_OneMinus);
			float smoothstepResult2_g57 = smoothstep( temp_output_7_0_g57 , ( temp_output_7_0_g57 + _AlphaSmoothness ) , lerpResult9_g57);
			float4 lerpResult13 = lerp( _Color , _ColorSh , smoothstepResult2_g57);
			float4 temp_output_28_0 = ( lerpResult4_g58 * lerpResult13 );
			o.Albedo = temp_output_28_0.rgb;
			o.Emission = temp_output_28_0.rgb;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf StandardCustomLighting alpha:fade keepalpha fullforwardshadows vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float3 worldNormal : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = IN.worldNormal;
				SurfaceOutputCustomLightingCustom o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputCustomLightingCustom, o )
				surf( surfIN, o );
				UnityGI gi;
				UNITY_INITIALIZE_OUTPUT( UnityGI, gi );
				o.Alpha = LightingStandardCustomLighting( o, worldViewDir, gi ).a;
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
0;0;1536;795;1520.722;1433.078;2.580138;True;False
Node;AmplifyShaderEditor.FunctionNode;25;-1262.817,-382.5539;Inherit;False;SF_SteppedTime;21;;49;d99f7a5ca99e84e49bd0eb15605d7cdc;0;1;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureTransformNode;24;-1271.014,-285.9567;Inherit;False;27;False;1;0;SAMPLER2D;;False;2;FLOAT2;0;FLOAT2;1
Node;AmplifyShaderEditor.TextureTransformNode;21;-1366.102,197.6177;Inherit;False;18;False;1;0;SAMPLER2D;;False;2;FLOAT2;0;FLOAT2;1
Node;AmplifyShaderEditor.FunctionNode;26;-1000.373,-359.4944;Inherit;False;SF_PanningUV;-1;;54;62ce0b89c9caeac4c9a120a5538d5698;0;5;12;FLOAT;0;False;3;FLOAT2;0,0;False;5;FLOAT2;0,0;False;8;FLOAT2;1,1;False;10;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;5;-1301.792,19.18788;Inherit;False;SF_SteppedTime;21;;55;d99f7a5ca99e84e49bd0eb15605d7cdc;0;1;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;20;-1116.502,175.5176;Inherit;False;SF_PanningUV;-1;;56;62ce0b89c9caeac4c9a120a5538d5698;0;5;12;FLOAT;0;False;3;FLOAT2;0,0;False;5;FLOAT2;0,0;False;8;FLOAT2;1,1;False;10;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;35;-651.1031,-295.082;Inherit;True;Property;_ColB;ColB;8;0;Create;True;0;0;0;False;0;False;-1;None;dd7d8a4b5f3e3794dbbdfd6b20d818c3;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;27;-713.6548,-502.3499;Inherit;True;Property;_Col;Col;7;0;Create;True;0;0;0;False;0;False;-1;None;dd7d8a4b5f3e3794dbbdfd6b20d818c3;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;36;-335.9033,-341.382;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0.35;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;18;-890.3021,204.1177;Inherit;True;Property;_Disp;Disp;0;0;Create;True;0;0;0;False;0;False;-1;None;dd7d8a4b5f3e3794dbbdfd6b20d818c3;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;37;-537.1918,313.6843;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-755.5918,473.9879;Inherit;False;Property;_Normal;Normal;20;0;Create;True;0;0;0;False;0;False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;7;-999.5918,642.9879;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.LerpOp;38;-454.2667,672.4107;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WorldPosInputsNode;46;-355.8811,-788.4046;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldSpaceCameraPos;49;-393.6127,-626.3998;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;52;20.40172,-460.3641;Inherit;False;Property;_DistanceDebutDegrade;DistanceDebutDegrade;15;0;Create;True;0;0;0;False;0;False;1;12;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;6;-1070.592,372.9879;Inherit;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.NormalizeNode;16;-268.9919,628.988;Inherit;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DistanceOpNode;50;-116.1773,-692.2557;Inherit;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;51;253.735,-686.6509;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;8;-209.3918,497.1879;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;55;419.1566,-460.2065;Inherit;False;Property;_DegradeVitesse;DegradeVitesse;16;0;Create;True;0;0;0;False;0;False;1;12;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;54;614.7806,-686.2012;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;12;-19.5918,533.8878;Inherit;False;SF_AlphaSharp;11;;57;1a46ba76a207bfe4e97ac05d03cb8401;0;2;1;FLOAT;0;False;6;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;15;-37.39178,307.3883;Inherit;False;Property;_ColorSh;ColorSh;10;0;Create;True;0;0;0;False;0;False;1,1,1,0;0.6980392,0.6980392,0.6,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;14;-45.19179,111.5879;Inherit;False;Property;_Color;Color;9;0;Create;True;0;0;0;False;0;False;1,1,1,0;0.9058824,0.9058824,0.8392157,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;56;885.4571,-640.8065;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;59;1072.938,-608.1147;Inherit;False;Property;_Float3;Float 1;18;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;39;-511.8922,12.87311;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;34;-162.3019,-243.0822;Inherit;False;SF_Bicolor;1;;58;8f1c0adb31a562646a4d2a8fec362420;0;3;9;COLOR;0,0,0,0;False;10;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;13;383.4082,452.9879;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;58;1070.951,-689.611;Inherit;False;Property;_Float2;Float 1;17;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-444.4024,185.9175;Inherit;False;Property;_Displacement;Displacement;19;0;Create;True;0;0;0;False;0;False;0;0.002;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;3;-725.5918,-46.01215;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-293.4517,53.66111;Inherit;False;3;3;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;57;1233.944,-624.0163;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;9;-731.9922,561.9882;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;586.4974,319.8176;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-377.1918,405.9879;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;987.8704,78.2345;Float;False;True;-1;2;ASEMaterialInspector;0;0;CustomLighting;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;26;12;25;0
WireConnection;26;8;24;0
WireConnection;26;10;24;1
WireConnection;20;12;5;0
WireConnection;20;8;21;0
WireConnection;20;10;21;1
WireConnection;35;1;26;0
WireConnection;27;1;26;0
WireConnection;36;0;27;1
WireConnection;36;1;35;1
WireConnection;18;1;20;0
WireConnection;37;0;18;1
WireConnection;37;1;36;0
WireConnection;38;0;37;0
WireConnection;38;1;7;0
WireConnection;38;2;11;0
WireConnection;16;0;38;0
WireConnection;50;0;46;0
WireConnection;50;1;49;0
WireConnection;51;0;50;0
WireConnection;51;1;52;0
WireConnection;8;0;6;0
WireConnection;8;1;16;0
WireConnection;54;0;51;0
WireConnection;54;1;55;0
WireConnection;12;1;8;0
WireConnection;56;0;54;0
WireConnection;34;1;36;0
WireConnection;13;0;14;0
WireConnection;13;1;15;0
WireConnection;13;2;12;0
WireConnection;4;0;39;0
WireConnection;4;1;23;0
WireConnection;4;2;18;1
WireConnection;57;0;58;0
WireConnection;57;1;59;0
WireConnection;57;2;56;0
WireConnection;9;0;10;0
WireConnection;9;1;7;0
WireConnection;28;0;34;0
WireConnection;28;1;13;0
WireConnection;10;0;37;0
WireConnection;10;1;11;0
WireConnection;0;0;28;0
WireConnection;0;2;28;0
WireConnection;0;9;57;0
WireConnection;0;11;4;0
ASEEND*/
//CHKSM=A61A5CF57DFC8D1E5B0B8E5DE3A11B307ADC31B9