//UNITY_SHADER_NO_UPGRADE
#ifndef TileShader_INCLUDED
#define TileShader_INCLUDED

float4 sample_texture(const UnityTexture2D tex, float2 uv)
{
    return SAMPLE_TEXTURE2D(tex, tex.samplerstate, uv);
}

void get_tile_data_half(const float2 uv, const UnityTexture2D tile_map, out half towerHeight, out bool supportsTower)
{
    half2 pixel = sample_texture(tile_map, uv);
    towerHeight = pixel.r;
    supportsTower = pixel.g > 0.5;
}

void draw_tile_float(const float2 uv, half tower_height, bool supports_tower, const float4 supports_tower_color, const float4 supports_no_tower_color, const float4 background_tile_color, const float2 group_size,
                     const UnityTexture2D mask, const UnityTexture2D tile_background, const UnityTexture2D tile_foreground, out float4 color)
{
    const float2 repeat_uv = uv * group_size % 1;
    float mask_value = sample_texture(mask, repeat_uv);

    if (mask_value < .5)
    {
        color = sample_texture(tile_background, repeat_uv) * background_tile_color * tower_height;
    }
    else
    {
        float4 foreground_color = sample_texture(tile_foreground, repeat_uv);
        if (supports_tower)
        {
            foreground_color *= supports_tower_color;
        }
        else
        {
            foreground_color *= supports_no_tower_color;
        }
        color = foreground_color;
    }
}

#endif
