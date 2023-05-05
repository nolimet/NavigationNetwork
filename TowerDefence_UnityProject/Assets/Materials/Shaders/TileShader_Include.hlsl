//UNITY_SHADER_NO_UPGRADE
#ifndef TileShader_INCLUDED
#define TileShader_INCLUDED

float4 sample_texture(const UnityTexture2D tex, float uv)
{
    return SAMPLE_TEXTURE2D(tex, tex.samplerstate, uv);
}

void get_tile_data_half(const float2 uv, const UnityTexture2D tile_map, const float2 chunk_size, out float towerHeight, out bool supportsTower)
{
    half2 pixel = sample_texture(tile_map, uv);
    towerHeight = pixel.r;
    supportsTower = pixel.g > 0.5;
}

void draw_tile(const float4 supports_tower_color, const float4 supports_no_tower_color, const float2 chunk_count, UnityTexture2D mask, UnityTexture2D tile_background, UnityTexture2D tile_forground)


#endif
