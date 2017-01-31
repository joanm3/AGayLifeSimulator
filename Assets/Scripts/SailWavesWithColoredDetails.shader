Shader "Custom/Boat/Sails/SailWavesWithColoredDetails"
{
	Properties
	{
		[MaterialToggle] _ShouldWave("Shoud it wave?", Float) = 0
		[MaterialToggle] _IsInverted("Is it inverted?", Float) = 0
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
		_DetailsTex("Details Texture", 2D) = "white" {}
		_Color("Details Color", Color) = (1, 1, 1, 1)
		_Shadows("Shadows", Range(0,1)) = 0.3
		_ShadowsColor("Shadows Color", Color) = (0, 0, 0, 0)
		_Amplitude("Amplitude", Range(0,1)) = 0.1
		_TimeFrequency("Time Frequency", Range(-20,20)) = 0
		_Frequency("Frequency", Float) = 3
		_ScaleX("Scale X", Float) = 1
	}

	SubShader
	{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Opaque" }
		LOD 100
		Lighting Off
		Cull back

	    Pass
	    {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD1;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float ratio : TEXCOORD1;
				half2 texcoord : TEXCOORD2;
			};

			float _ShouldWave;
			float _IsInverted;
			float4 _ShadowsColor;
			float _Amplitude;
			float _TimeFrequency;
			float _Frequency;
			float4 _Color;
			float _ScaleX;

			sampler2D _MainTex;
			sampler2D _DetailsTex;
			float4 _MainTex_ST;
			fixed _Shadows;

			v2f vert(appdata v)
			{
				float4 position = v.vertex;
				float s = 0;
				if ( _ShouldWave )
				{
					if ( _IsInverted )
					{
						s = sin(_Time * ( _TimeFrequency * 10 ) + position.z * _Frequency) * _Amplitude;
						if ( _ScaleX != 0 )
							s /= abs(_ScaleX);
						position.x += s;
					}
					else
					{
						s = sin(_Time * ( _TimeFrequency * 10 ) - position.y * _Frequency) * _Amplitude;
						if ( _ScaleX != 0 )
							s /= abs(_ScaleX);
						position.x += s;
					}
				}

				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, position);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.uv = v.uv;
				o.ratio = s;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{ 
				fixed4 cDetails = tex2D( _DetailsTex, i.texcoord );
		        if ( cDetails.a > 0 )
		      	 cDetails *= _Color;
		        fixed4 cRegular = tex2D( _MainTex, i.texcoord );
				fixed4 col = ( cDetails.a == 0 ? cRegular : ( ( 1 - cDetails.a ) * cRegular + cDetails.a * cDetails ) );
				if ( i.ratio != 0 )
				{
					float ratio = _Shadows * 8 * -i.ratio;
					col *= ( 1 - ratio );
					col += _ShadowsColor * ratio;
				}
				return col;
			}
			ENDCG
		}
	}
}
