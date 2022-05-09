Shader "PSX/Particles"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		_MainColor("MainColor", Color) = (1,1,1,1)
		_EmissionTexture("EmissionTexture", 2D) = "white" {}
		_EmissionColor("EmissionColor", Color) = (0,0,0,0)
		_EmissionBakedMultiplier("EmissionBakedMultiplier", Float) = 1.0
		_AlphaClippingDitherIsEnabled("_AlphaClippingDitherIsEnabled", Float) = 1.0
		_AlphaClippingScaleBiasMinMax("_AlphaClippingScaleBiasMinMax", Vector) = (1.0, 0.0, 0.0, 1.0)
		_AffineTextureWarpingWeight("_AffineTextureWarpingWeight", Float) = 1.0
		_PrecisionGeometryWeight("_PrecisionGeometryWeight", Float) = 1.0
		_PrecisionGeometryOverrideMode("_PrecisionGeometryOverrideMode", Float) = 0.0
		_PrecisionGeometryOverrideParameters("_PrecisionGeometryOverrideParameters", Vector) = (0, 0, 0, 0)
		_PrecisionColorOverrideMode("_PrecisionColorOverrideMode", Float) = 0.0
		_PrecisionColorOverrideParameters("_PrecisionColorOverrideParameters", Vector) = (0, 0, 0, 0)
		_FogWeight("_FogWeight", Float) = 1.0
		_DrawDistanceOverrideMode("_DrawDistanceOverrideMode", Int) = 0
		_DrawDistanceOverride("_DrawDistanceOverride", Vector) = (100, 10000, 0, 0)
		_ReflectionCubemap("_ReflectionCubemap", Cube) = "black" {}
		_ReflectionTexture("_ReflectionTexture", 2D) = "white" {}
		_ReflectionColor("_ReflectionColor", Color) = (1,1,1,1)
		_ReflectionDirectionMode("_ReflectionDirectionMode", Int) = 0
		_ReflectionBlendMode("_ReflectionBlendMode", Int) = 0
		[HideInInspector] _DoubleSidedConstants("_DoubleSidedConstants", Vector) = (1, 1, 1, 0)
		_UVAnimationMode("_UVAnimationMode", Float) = 0.0
		_UVAnimationParametersFrameLimit("_UVAnimationParametersFrameLimit", Vector) = (0, 60, 0, 0)
		_UVAnimationParameters("_UVAnimationParameters", Vector) = (1, 1, 0, 0)
		_VertexColorBlendMode("_VertexColorBlendMode", Float) = 0.0

			// C# side material state tracking.
			[HideInInspector] _TextureFilterMode("__textureFilterMode", Float) = 0.0
			[HideInInspector] _VertexColorMode("__vertexColorMode", Float) = 0.0
			[HideInInspector] _RenderQueueCategory("__renderQueueCategory", Float) = 0.0
			[HideInInspector] _LightingMode("__lightingMode", Float) = 0.0
			[HideInInspector] _LightingBaked("__lightingBaked", Float) = 1.0
			[HideInInspector] _LightingDynamic("__lightingDynamic", Float) = 1.0
			[HideInInspector] _ShadingEvaluationMode("__shadingEvaluationMode", Float) = 0.0
			[HideInInspector] _BRDFMode("__brdfMode", Float) = 0.0
			[HideInInspector] _Surface("__surface", Float) = 0.0
			[HideInInspector] _Blend("__blend", Float) = 0.0
			[HideInInspector] _AlphaClip("__clip", Float) = 1.0
			[HideInInspector] _BlendOp("__blendOp", Float) = 0.0
			[HideInInspector] _SrcBlend("__src", Float) = 1.0
			[HideInInspector] _DstBlend("__dst", Float) = 0.0
			[HideInInspector] _ZWrite("__zw", Float) = 1.0
			[HideInInspector] _Cull("__cull", Float) = 2.0
			[HideInInspector] _ColorMask("__colorMask", Float) = 15.0 // UnityEngine.Rendering.ColorWriteMask.All
			[HideInInspector] _Reflection("__reflection", Float) = 0.0
			[HideInInspector] _DoubleSidedNormalMode("__doubleSidedNormalMode", Float) = 0.0

			//Here comes the cut part of hit cut
			//_AlphaCut("AlphaCut", Float) = 0.0
			//_DistanceCut("CutDistance", Float) = 0.5
			//_BlendingCut("Blending", Float) = 0.0
	}
	SubShader
	{
		Tags{ "RenderType" = "PSXLit" }
		LOD 100

		Pass
		{
			Name "PSXLit"
			Tags { "LightMode" = "PSXLit" }

			BlendOp[_BlendOp]
			Blend[_SrcBlend][_DstBlend]
			ZWrite[_ZWrite]
			Cull[_Cull]
			ColorMask[_ColorMask]

			HLSLPROGRAM
			// Required to compile gles 2.0 with standard srp library
			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x
			#pragma target 3.0

			// -------------------------------------
			// Global Keywords (set by render pipeline)
			#pragma multi_compile _OUTPUT_LDR _OUTPUT_HDR
			#pragma multi_compile _FOG_COLOR_LUT_MODE_DISABLED _FOG_COLOR_LUT_MODE_TEXTURE2D_DISTANCE_AND_HEIGHT _FOG_COLOR_LUT_MODE_TEXTURECUBE
			#pragma multi_compile _ LIGHTMAP_SHADOW_MASK

			// -------------------------------------
			// Material Keywords
			#pragma shader_feature _TEXTURE_FILTER_MODE_TEXTURE_IMPORT_SETTINGS _TEXTURE_FILTER_MODE_POINT _TEXTURE_FILTER_MODE_POINT_MIPMAPS _TEXTURE_FILTER_MODE_N64 _TEXTURE_FILTER_MODE_N64_MIPMAPS
			#pragma shader_feature _ _VERTEX_COLOR_MODE_COLOR _VERTEX_COLOR_MODE_LIGHTING _VERTEX_COLOR_MODE_COLOR_BACKGROUND _VERTEX_COLOR_MODE_ALPHA_ONLY _VERTEX_COLOR_MODE_EMISSION _VERTEX_COLOR_MODE_EMISSION_AND_ALPHA_ONLY _VERTEX_COLOR_MODE_SPLIT_COLOR_AND_LIGHTING
			#pragma shader_feature _LIGHTING_BAKED_ON
			#pragma shader_feature _LIGHTING_DYNAMIC_ON
			#pragma shader_feature _SHADING_EVALUATION_MODE_PER_VERTEX _SHADING_EVALUATION_MODE_PER_PIXEL _SHADING_EVALUATION_MODE_PER_OBJECT
			#pragma shader_feature _BRDF_MODE_LAMBERT _BRDF_MODE_WRAPPED_LIGHTING
			#pragma shader_feature _EMISSION
			#pragma shader_feature _ALPHATEST_ON
			#pragma shader_feature _ _ALPHAPREMULTIPLY_ON _ALPHAMODULATE_ON
			#pragma shader_feature _REFLECTION_ON
			#pragma shader_feature _FOG_ON
			#pragma shader_feature _DOUBLE_SIDED_ON
			#pragma shader_feature _BLENDMODE_TONEMAPPER_OFF
			#pragma shader_feature _LOD_REQUIRES_ADJUSTMENT

			// -------------------------------------
			// Unity defined keywords
			#pragma multi_compile _ DIRLIGHTMAP_COMBINED
			#pragma multi_compile _ LIGHTMAP_ON

			//--------------------------------------
			// GPU Instancing
			#pragma multi_compile_instancing

			//_AlphaCutoff = 0;

			#pragma vertex LitPassVertex
			#pragma fragment LitPassFragment



			#include "Packages/com.hauntedpsx.render-pipelines.psx/Runtime/Material/PSXLit/PSXLitInput.hlsl"
			//#include "Packages/com.hauntedpsx.render-pipelines.psx/Runtime/Material/PSXLit/PSXLitPass.hlsl"   

			#ifndef PSX_LIT_PASS
			#define PSX_LIT_PASS

			#include "Packages/com.hauntedpsx.render-pipelines.psx/Runtime/ShaderLibrary/ShaderVariables.hlsl"
			#include "Packages/com.hauntedpsx.render-pipelines.psx/Runtime/ShaderLibrary/ShaderFunctions.hlsl"
			#include "Packages/com.hauntedpsx.render-pipelines.psx/Runtime/ShaderLibrary/BakedLighting.hlsl"
			#include "Packages/com.hauntedpsx.render-pipelines.psx/Runtime/ShaderLibrary/DynamicLighting.hlsl"
			#include "Packages/com.hauntedpsx.render-pipelines.psx/Runtime/ShaderLibrary/MaterialFunctions.hlsl"

			float _AlphaCut;
			float _DistanceCut;
			float _BlendingCut;

			// Simply rely on dead code removal to strip unused attributes and varyings from shaders.
			// Manually stripping with ifdefing made the shader harder to read.
			struct Attributes
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
				float4 color : COLOR;
				float2 lightmapUV : TEXCOORD1;
			};

			struct Varyings
			{
				float4 vertex : SV_POSITION;
				float3 uvw : TEXCOORD0;
				float3 positionVS : TEXCOORD1;
				float3 positionWS : TEXCOORD2;
				float3 normalWS : TEXCOORD3;
				float4 color : TEXCOORD4;
				float4 fog : TEXCOORD5;
				float3 lighting : TEXCOORD6;
				float2 lightmapUV : TEXCOORD7;
				float4 posWorld : TEXCOORD8;
			};

			Varyings LitPassVertex(Attributes v)
			{
				Varyings o;
				ZERO_INITIALIZE(Varyings, o);

				float3 objectPositionWS = TransformObjectToWorld(float3(0.0f, 0.0f, 0.0f));
				float3 objectPositionVS = TransformWorldToView(objectPositionWS);
				float3 positionWS = TransformObjectToWorld(v.vertex.xyz);
				float3 positionVS = TransformWorldToView(positionWS);
				ApplyGeometryPushbackToPosition(positionWS, positionVS, _GeometryPushbackParameters);

				float4 positionCS = TransformWorldToHClip(positionWS);
				o.vertex = positionCS;

				float2 uv = ApplyUVAnimationVertex(v.uv, _UVAnimationMode, _UVAnimationParametersFrameLimit, _UVAnimationParameters);

				float3 precisionColor;
				float3 precisionColorInverse;
				float precisionColorIndexNormalized = _PrecisionColor.w;
				float precisionChromaBit = _PrecisionColorInverse.w;
				ApplyPrecisionColorOverride(precisionColor, precisionColorInverse, _PrecisionColor.rgb, _PrecisionColorInverse.rgb, precisionColorIndexNormalized, precisionChromaBit, _PrecisionColorOverrideMode, _PrecisionColorOverrideParameters);

				o.vertex = ApplyPrecisionGeometryToPositionCS(positionWS, positionVS, o.vertex, _PrecisionGeometryOverrideMode, _PrecisionGeometryOverrideParameters, _DrawDistanceOverrideMode, _DrawDistanceOverride);
				o.uvw = ApplyAffineTextureWarpingToUVW(uv, positionCS.w, _AffineTextureWarpingWeight);
				o.color = EvaluateVertexColorPerVertex(v.color, o.uvw.z);
				o.positionVS = positionVS; // TODO: Apply affine warping?
				o.positionWS = positionWS; // TODO: Apply affine warping?
				o.lightmapUV = v.lightmapUV.xy * unity_LightmapST.xy + unity_LightmapST.zw; // TODO: Apply affine warping?
				o.normalWS = TransformObjectToWorldNormal(v.normal);
				o.normalWS = EvaluateNormalDoubleSidedPerVertex(o.normalWS, o.positionWS, _WorldSpaceCameraPos);
				o.lighting = EvaluateLightingPerVertex(objectPositionWS, positionWS, o.normalWS, v.color, o.lightmapUV, o.uvw.z);
				o.fog = EvaluateFogPerVertex(objectPositionWS, objectPositionVS, positionWS, positionVS, o.uvw.z, _FogWeight, precisionColor, precisionColorInverse);
				o.posWorld = mul(unity_ObjectToWorld, v.vertex);

				return o;
			}

			half4 LitPassFragment(Varyings i, FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC) : SV_Target
			{

				float2 positionSS = i.vertex.xy;

				// Remember, we multipled all interpolated vertex values by positionCS.w in order to counter contemporary hardware's perspective correct interpolation.
				// We need to post multiply all interpolated vertex values by the interpolated positionCS.w (stored in uvw.z) component to "normalize" the interpolated values.
				float interpolatorNormalization = 1.0f / i.uvw.z;

				float3 normalWS = normalize(i.normalWS);
				normalWS = EvaluateNormalDoubleSidedPerPixel(normalWS, cullFace);

				float2 uv = i.uvw.xy * interpolatorNormalization;
				float2 uvColor = TRANSFORM_TEX(uv, _MainTex);

				float4 texelSizeLod;
				float lod;
				ComputeLODAndTexelSizeMaybeCallDDX(texelSizeLod, lod, uvColor, _MainTex_TexelSize);
				uvColor = ApplyUVAnimationPixel(texelSizeLod, lod, uvColor, _UVAnimationMode, _UVAnimationParametersFrameLimit, _UVAnimationParameters);

				float4 color = _MainColor * SampleTextureWithFilterMode(TEXTURE2D_ARGS(_MainTex, sampler_MainTex), uvColor, texelSizeLod, lod);

				color = ApplyVertexColorPerPixelColor(color, i.color, interpolatorNormalization, _VertexColorBlendMode);

			#if _ALPHATEST_ON
				// Perform alpha cutoff transparency (i.e: discard pixels in the holes of a chain link fence texture, or in the negative space of a leaf texture).
				// Any alpha value < alphaClippingDither will trigger the pixel to be discarded, any alpha value greater than or equal to alphaClippingDither will trigger the pixel to be preserved.
				float alphaClippingDither;
				float alphaForClipping;

				float cameraDist = length(i.posWorld.xyz - _WorldSpaceCameraPos.xyz);

				color.a = min(color.a,((cameraDist / _DistanceCut) - _AlphaCut) / (1-_AlphaCut)); // * (_AlphaCut)

				ComputeAndFetchAlphaClippingParameters(alphaClippingDither, alphaForClipping, color.a, positionSS, _AlphaClippingDitherIsEnabled, _AlphaClippingScaleBiasMinMax);
				clip((alphaForClipping > alphaClippingDither) ? 1.0f : -1.0f);
			#endif

			#if defined(SCENESELECTIONPASS)
				// We use depth prepass for scene selection in the editor, this code allow to output the outline correctly
				return float4(_ObjectId, _PassValue, 1.0, 1.0);
			#endif

				if (!_IsPSXQualityEnabled)
				{
					// TODO: Handle premultiply alpha case here?
					return color;
				}

				// Rather than paying the cost of interpolating our 6 floats for precision color per vertex, we simply recompute them per pixel here.
				float3 precisionColor;
				float3 precisionColorInverse;
				float precisionColorIndexNormalized = _PrecisionColor.w;
				float precisionChromaBit = _PrecisionColorInverse.w;
				ApplyPrecisionColorOverride(precisionColor, precisionColorInverse, _PrecisionColor.rgb, _PrecisionColorInverse.rgb, precisionColorIndexNormalized, precisionChromaBit, _PrecisionColorOverrideMode, _PrecisionColorOverrideParameters);

				color = ApplyPrecisionColorToColorSRGB(color, precisionColor, precisionColorInverse);
				color.rgb = SRGBToLinear(color.rgb);
				color = ApplyAlphaBlendTransformToColor(color);

				float3 lighting = EvaluateLightingPerPixel(i.positionWS, normalWS, i.lighting, i.lightmapUV, interpolatorNormalization);
				color = ApplyLightingToColor(lighting, color);

			#if defined(_EMISSION)
				// Convert to sRGB 5:6:5 color space, then from sRGB to Linear.
				float3 emission = _EmissionColor.rgb * SampleTextureWithFilterMode(TEXTURE2D_ARGS(_EmissionTexture, sampler_EmissionTexture), uvColor, texelSizeLod, lod).rgb;
				emission = ApplyPrecisionColorToColorSRGB(float4(emission, 0.0f), precisionColor, precisionColorInverse).rgb;
				emission = SRGBToLinear(emission);
				emission = ApplyVertexColorPerPixelEmission(emission, i.color, interpolatorNormalization);
				emission = ApplyAlphaBlendTransformToEmission(emission, color.a);

				color.rgb += emission;
			#endif

			#if defined(_REFLECTION_ON)
				float3 reflection = _ReflectionColor.rgb * SAMPLE_TEXTURE2D(_ReflectionTexture, sampler_ReflectionTexture, uvColor).rgb;
				reflection = ApplyPrecisionColorToColorSRGB(float4(reflection, 0.0f), precisionColor, precisionColorInverse).rgb;
				reflection = SRGBToLinear(reflection);
				reflection = ApplyAlphaBlendTransformToEmission(reflection, color.a);

				float3 V = normalize(i.positionWS - _WorldSpaceCameraPos);
				float3 R = reflect(V, normalWS);
				float3 reflectionDirection = EvaluateReflectionDirectionMode(_ReflectionDirectionMode, R, normalWS, V);
				float4 reflectionCubemap = SAMPLE_TEXTURECUBE(_ReflectionCubemap, sampler_ReflectionCubemap, reflectionDirection);
				reflectionCubemap.rgb *= reflectionCubemap.a;
				reflectionCubemap.rgb = ApplyPrecisionColorToColorSRGB(float4(reflectionCubemap.rgb, 0.0f), precisionColor, precisionColorInverse).rgb;
				// TODO: Convert reflectionCubemap from SRGB to linear space, but only if an LDR texture was supplied...
				// reflectionCubemap = SRGBToLinear(reflectionCubemap);
				reflection *= reflectionCubemap.rgb;

				if (_ReflectionBlendMode == 0)
				{
					// Additive
					color.rgb += reflection;
				}
				else if (_ReflectionBlendMode == 1)
				{
					// Subtractive
					color.rgb = max(0.0f, color.rgb - reflection);
				}
				else if (_ReflectionBlendMode == 2)
				{
					// Multiply
					color.rgb *= reflection;
				}

			#endif

				float4 fog = EvaluateFogPerPixel(i.positionWS, i.positionVS, positionSS, i.fog, interpolatorNormalization, _FogWeight, precisionColor, precisionColorInverse);
				fog.a = ApplyAlphaBlendTransformToFog(fog.a, color.a);
				color = ApplyFogToColor(fog, color);

			#if defined(_OUTPUT_LDR)

			#if !defined(_BLENDMODE_TONEMAPPER_OFF)
				// Apply tonemapping and gamma correction.
				// This is a departure from classic PS1 games, but it allows for greater flexibility, giving artists more controls for creating the final look and feel of their game.
				// Otherwise, they would need to spend a lot more time in the texturing phase, getting the textures alone to produce the mood they are aiming for.
				if (_TonemapperIsEnabled)
				{
					color.rgb = TonemapperGeneric(color.rgb);
				}
			#endif

				color.rgb = LinearToSRGB(color.rgb);

				// Convert the final color value to 5:6:5 color space (default) - this will actually be whatever color space the user specified in the Precision Volume Override.
				// This emulates a the limited bit-depth frame buffer.
				color.rgb = ComputeFramebufferDiscretization(color.rgb, positionSS, precisionColor, precisionColorInverse);
			#endif

				return (half4)color;
			}

			#endif

			ENDHLSL
		}

		// This pass it not used during regular rendering, only for lightmap baking.
		Pass
		{
			Name "Meta"
			Tags{"LightMode" = "Meta"}

			Cull Off

			HLSLPROGRAM
			// Required to compile gles 2.0 with standard srp library
			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x
			#pragma target 3.0

			// -------------------------------------
			// Material Keywords
			#pragma shader_feature _ _VERTEX_COLOR_MODE_COLOR _VERTEX_COLOR_MODE_LIGHTING _VERTEX_COLOR_MODE_COLOR_BACKGROUND _VERTEX_COLOR_MODE_ALPHA_ONLY _VERTEX_COLOR_MODE_SPLIT_COLOR_AND_LIGHTING
			#pragma shader_feature _EMISSION
			#pragma shader_feature _ALPHATEST_ON
			#pragma shader_feature _ _ALPHAPREMULTIPLY_ON _ALPHAMODULATE_ON

			#pragma vertex LitMetaPassVertex
			#pragma fragment LitMetaPassFragment

			#include "Packages/com.hauntedpsx.render-pipelines.psx/Runtime/Material/PSXLit/PSXLitInput.hlsl"
			#include "Packages/com.hauntedpsx.render-pipelines.psx/Runtime/Material/PSXLit/PSXLitMetaPass.hlsl"

			ENDHLSL
		}

		Pass
		{
		
			Name "SceneSelectionPass"
			Tags { "LightMode" = "SceneSelectionPass" }

			BlendOp[_BlendOp]
			Blend[_SrcBlend][_DstBlend]
			ZWrite[_ZWrite]
			Cull[_Cull]

			HLSLPROGRAM
			// Required to compile gles 2.0 with standard srp library
			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x
			#pragma target 3.0

			// -------------------------------------
			// Material Keywords
			#pragma shader_feature _ALPHATEST_ON

			//--------------------------------------
			// GPU Instancing
			#pragma multi_compile_instancing

			#pragma vertex LitPassVertex
			#pragma fragment LitPassFragment

			#define SCENESELECTIONPASS // This will drive the output of the scene selection shader
			#include "Packages/com.hauntedpsx.render-pipelines.psx/Runtime/Material/PSXLit/PSXLitInput.hlsl"
			#include "Packages/com.hauntedpsx.render-pipelines.psx/Runtime/Material/PSXLit/PSXLitPass.hlsl"            
			ENDHLSL
		}
	}

	CustomEditor "HauntedPSX.RenderPipelines.PSX.Editor.PSXLitShaderGUI"
}
