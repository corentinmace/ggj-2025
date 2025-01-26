shader_type canvas_item;

void fragment() {
	if (COLOR.a <= 0.0001)
		discard;

	COLOR = vec4(COLOR.rgb, (sin(TIME * 20.0) + 1.5) / 2.0);
}