#ifndef EVAN_SHAPES
#define EVAN_SHAPES

float OvalShape(float2 uv, float center, float radiusA, float radiusB, float size)
{
    float x = distance(uv.x, center);
    float y = distance(uv.y, center);
            
    return step(pow(x,2) / pow(radiusA, 2) + pow(y,2) / pow(radiusB, 2), size);
}

float OvalRingShape(float2 uv, float center, float radiusA, float radiusB)
{
    float x = distance(uv.x, center);
    float y = distance(uv.y, center);
            
    return pow(x,2) / pow(radiusA, 2) + pow(y,2) / pow(radiusB, 2);
}

#endif
