shader_type canvas_item;

void fragment() {
	if (COLOR.a < 0.0001)
		discard;

	COLOR = mix(COLOR, vec4(0.0, 1.0, 0.0, 1.0), 0.4);
}

