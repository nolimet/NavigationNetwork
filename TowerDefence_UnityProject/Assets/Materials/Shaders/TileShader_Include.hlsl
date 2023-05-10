//UNITY_SHADER_NO_UPGRADE
#ifndef TileShader_INCLUDED
#define TileShader_INCLUDED

float4 sample_texture(const UnityTexture2D tex, float2 uv)
{
    return SAMPLE_TEXTURE2D(tex, tex.samplerstate, uv);
}

void get_tile_data_half(const half2 uv, const UnityTexture2D tile_map, out float tower_height, out bool supports_tower)
{
    half4 pixel = sample_texture(tile_map, uv);
    tower_height = pixel.r;
    supports_tower = pixel.g > 0.5;
}

void draw_tile_half(const half2 uv, const half tower_height, const bool supports_tower, const half4 supports_tower_color, const half4 supports_no_tower_color, const half4 background_tile_color, const half2 group_size,
                    const UnityTexture2D mask, const UnityTexture2D tile_background, const UnityTexture2D tile_foreground, out half4 color)
{
    const half2 repeat_uv = uv * group_size % 1;
    half mask_value = sample_texture(mask, repeat_uv);

    if (mask_value < .5)
    {
        color = sample_texture(tile_background, repeat_uv) * background_tile_color * tower_height;
    }
    else
    {
        half4 foreground_color = sample_texture(tile_foreground, repeat_uv);
        if (supports_tower)
        {
            color = foreground_color * supports_tower_color;
        }
        else
        {
            color = foreground_color * supports_no_tower_color;
        }
    }
}

#endif
