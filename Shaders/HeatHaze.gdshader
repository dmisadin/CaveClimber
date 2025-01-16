shader_type canvas_item;

// Uniforms
uniform sampler2D noise_texture : source_color;
uniform float distortion_strength : hint_range(0.0, 10.0) = 2.0;
uniform float speed : hint_range(0.0, 5.0) = 1.0;
uniform float scale : hint_range(0.1, 5.0) = 1.0;
uniform vec2 screen_size; // Pass the screen size manually from the script

void fragment() {
    vec2 uv = FRAGCOORD.xy / screen_size;

    // Time-based distortion
    float time = TIME * speed;

    // Sample noise texture
    vec2 noise_uv = uv * scale + vec2(0.0, time);
    float noise = texture(noise_texture, noise_uv).r;

    // Offset UVs based on noise
    vec2 distortion = vec2(noise, noise) * distortion_strength / screen_size;
    vec2 distorted_uv = UV + distortion;

    // Sample the original texture with distorted UVs
    COLOR = texture(TEXTURE, distorted_uv);
}
