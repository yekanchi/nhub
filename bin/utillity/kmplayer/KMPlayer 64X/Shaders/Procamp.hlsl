// procamp=ps_2_0
// Code from MPC

sampler s0 : register(s0);
float4 p0 : register(c0);
float4 p1 : register(c1);

#define width (p0[0])
#define height (p0[1])
#define counter (p0[2])
#define clock (p0[3])
#define one_over_width (p1[0])
#define one_over_height (p1[1])

#define PI acos(-1)

#ifdef GLSL

static float4x4 r2y = float4x4
(
	0.299, 0.587, 0.114, 0,
	-0.147, -0.289, 0.437, 0,
	0.615, -0.515, -0.100, 0,
	0, 0, 0, 0
);

static float4x4 y2r = float4x4
(
	1.0, 0.0, 1.140, 0, 
	1.0, -0.394, -0.581, 0,
	1.0, 2.028, 0.0, 0, 
	0, 0, 0, 0
);

#else

static float4x4 r2y =
{
	0.299, 0.587, 0.114, 0,
	-0.147, -0.289, 0.437, 0,
	0.615, -0.515, -0.100, 0,
	0, 0, 0, 0
};

static float4x4 y2r =
{
	1.0, 0.0, 1.140, 0, 
	1.0, -0.394, -0.581, 0,
	1.0, 2.028, 0.0, 0, 
	0, 0, 0, 0
};

#endif

#define ymin (16.0/255)
#define ymax (235.0/255)

// Brightness: -1.0 to 1.0, default 0.0
// Contrast: 0.0 to 10.0, default 1.0
// Hue: -180.0 to +180.0, default 0.0
// Saturation: 0.0 to 10.0, default 1.0

#define Brightness 0.0
#define Contrast 1.0
#define Hue 0.0
#define Saturation 1.0

// tv -> pc scale
// #define Brightness (-ymin)
// #define Contrast (1.0/(ymax-ymin))

#ifdef GLSL

static float2x2 HueMatrix = float2x2
(
	cos(Hue * PI / 180), sin(Hue * PI / 180),
	-sin(Hue * PI / 180), cos(Hue * PI / 180)
);

#else

static float2x2 HueMatrix =
{
	cos(Hue * PI / 180), sin(Hue * PI / 180),
	-sin(Hue * PI / 180), cos(Hue * PI / 180)
};

#endif

float4 main(float2 tex : TEXCOORD0) : COLOR
{
	float4 c0 = tex2D(s0, tex);
#ifdef GLSL
	c0 = mul(transpose(r2y), c0);
	c0.r = Contrast * (c0.r - ymin) + ymin + Brightness;
	c0.gb = mul(transpose(HueMatrix), c0.gb) * Saturation;
	c0 = mul(transpose(y2r), c0);
#else
	c0 = mul(r2y, c0);
	c0.r = Contrast * (c0.r - ymin) + ymin + Brightness;
	c0.gb = mul(HueMatrix, c0.gb) * Saturation;
	c0 = mul(y2r, c0);
#endif
	return c0; 
}